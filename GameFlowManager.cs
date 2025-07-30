using Checkers.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Checkers
{
    // 1. GameSettings
    public class GameSettings
    {
        public string Player1Queen { get; }
        public string Player2Queen { get; }
        public string EmptySymbol { get; }
        public int AIDepth { get; }

        public GameSettings(string p1Queen, string p2Queen, string empty, int aiDepth)
        {
            Player1Queen = p1Queen;
            Player2Queen = p2Queen;
            EmptySymbol = empty;
            AIDepth = aiDepth;
        }
    }

    // 2. IGameStateTracker
    public interface IGameStateTracker
    {
        int WinnerAnnounced { get; set; }
        bool IsPlayer1Turn { get; set; }
    }

    // 3. IComputerController
    public interface IComputerController
    {
        bool AgainstComputer { get; }
        bool ComputerMoveDone { get; set; }
    }

    // 4. IGameUI
    public interface IGameUI
    {
        void UpdateTurnLabel();
        void OnGameEnded();
        Timer GameTimer { get; }
    }

    // 5. IGameRepository (wraps your existing SqlGameResultRepository)
    public interface IGameRepository
    {
        void UpdateScore(string player, int points);
        void RecordWin(string winnerName, string player1, string player2, DateTime timestamp);
    }

    // Adapter: delegates calls to SqlGameResultRepository
    public class SqlGameResultRepositoryAdapter : IGameRepository
    {
        private readonly SqlGameResultRepository _inner;
        public SqlGameResultRepositoryAdapter(SqlConnection cnn)
        {
            _inner = new SqlGameResultRepository(cnn);
        }
        public void UpdateScore(string player, int points)
            => _inner.UpdateScore(player, points);

        public void RecordWin(string winnerName, string player1, string player2, DateTime timestamp)
            => _inner.RecordWin(winnerName, player1, player2, timestamp);
    }

    // Adapter for IGameStateTracker
    public class FuncGameStateTracker : IGameStateTracker
    {
        private readonly Func<int> _getWinnerAnnounced;
        private readonly Action<int> _setWinnerAnnounced;
        private readonly Func<bool> _getIsPlayer1Turn;
        private readonly Action<bool> _setIsPlayer1Turn;
        private readonly Func<string> _getPlayer1;
        private readonly Func<string> _getPlayer2;


        public FuncGameStateTracker(
            Func<int> getWinnerAnnounced,
            Action<int> setWinnerAnnounced,
            Func<bool> getIsPlayer1Turn,
            Action<bool> setIsPlayer1Turn)
        {
            _getWinnerAnnounced = getWinnerAnnounced;
            _setWinnerAnnounced = setWinnerAnnounced;
            _getIsPlayer1Turn = getIsPlayer1Turn;
            _setIsPlayer1Turn = setIsPlayer1Turn;
        }

        public int WinnerAnnounced { get => _getWinnerAnnounced(); set => _setWinnerAnnounced(value); }
        public bool IsPlayer1Turn { get => _getIsPlayer1Turn(); set => _setIsPlayer1Turn(value); }
    }

    // Adapter for IComputerController
    public class FuncComputerController : IComputerController
    {
        private readonly Func<bool> _getAgainstComputer;
        private readonly Func<bool> _getMoveDone;
        private readonly Action<bool> _setMoveDone;

        public FuncComputerController(
            Func<bool> getAgainstComputer,
            Func<bool> getMoveDone,
            Action<bool> setMoveDone)
        {
            _getAgainstComputer = getAgainstComputer;
            _getMoveDone = getMoveDone;
            _setMoveDone = setMoveDone;
        }

        public bool AgainstComputer => _getAgainstComputer();
        public bool ComputerMoveDone { get => _getMoveDone(); set => _setMoveDone(value); }
    }

    // Adapter for IGameUI
    public class DelegateGameUI : IGameUI
    {
        private readonly Action _updateTurnLabel;
        private readonly Action _onGameEnded;
        public Timer GameTimer { get; }

        public DelegateGameUI(Action updateTurnLabel, Action onGameEnded, Timer gameTimer)
        {
            _updateTurnLabel = updateTurnLabel;
            _onGameEnded = onGameEnded;
            GameTimer = gameTimer;
        }

        public void UpdateTurnLabel() => _updateTurnLabel();
        public void OnGameEnded() => _onGameEnded();
    }

    /// <summary>
    /// Handles full game flow: moves, captures, promotions, win logic & database.
    /// </summary>
    internal class GameFlowManager
    {
        // Board & logic
        private readonly int[,] _boardLogic;
        private readonly Button[,] _boardButtons;
        private readonly GameBoardManager _boardManager;
        private readonly GameLogicManager _logicManager;
        private readonly GameSettings _settings;
        // State & AI
        private readonly IGameStateTracker _state;
        private readonly IComputerController _computer;
        // UI & repo
        private readonly IGameUI _ui;
        private readonly IGameRepository _repo;
        // Player providers
        private readonly Func<string> _getPlayer1;
        private readonly Func<string> _getPlayer2;

        // קבוע לשם המחשב
        private const string ComputerUser = "Computer";

        public GameFlowManager(
            int[,] boardLogic,
            Button[,] boardButtons,
            GameBoardManager boardManager,
            GameLogicManager logicManager,
            GameSettings settings,
            IGameStateTracker state,
            IComputerController computer,
            IGameUI ui,
            IGameRepository repo,
            Func<string> getPlayer1,
            Func<string> getPlayer2)
        {
            _boardLogic = boardLogic;
            _boardButtons = boardButtons;
            _boardManager = boardManager;
            _logicManager = logicManager;
            _settings = settings;
            _state = state;
            _computer = computer;
            _ui = ui;
            _repo = repo;
            _getPlayer1 = getPlayer1;
            _getPlayer2 = getPlayer2;
        }

        public void ExecutePlayerMove(int fromY, int fromX, int toY, int toX)
        {
            int rows = _boardButtons.GetLength(0);
            int cols = _boardButtons.GetLength(1);
            if (fromY < 0 || fromX < 0 || toY < 0 || toX < 0 ||
                fromY >= rows || fromX >= cols || toY >= rows || toX >= cols)
            {
                MessageBox.Show("Invalid move: out of bounds.");
                return;
            }

            // capture
            if (Math.Abs(toX - fromX) == 2)
            {
                int midY = (fromY + toY) / 2;
                int midX = (fromX + toX) / 2;
                _boardButtons[midY, midX].Text = _settings.EmptySymbol;
                _boardLogic[midY, midX] = 0;
            }

            // move
            _boardButtons[toY, toX].Text = _boardButtons[fromY, fromX].Text;
            _boardLogic[toY, toX] = _boardLogic[fromY, fromX];
            _boardButtons[fromY, fromX].Text = _settings.EmptySymbol;
            _boardLogic[fromY, fromX] = 0;

            // promotion
            if (toY == 0 && _boardLogic[toY, toX] == 1)
            {
                _boardButtons[toY, toX].Text = _settings.Player1Queen;
                _boardLogic[toY, toX] = 2;
            }
            else if (toY == rows - 1 && _boardLogic[toY, toX] == -1)
            {
                _boardButtons[toY, toX].Text = _settings.Player2Queen;
                _boardLogic[toY, toX] = -2;
            }

            CompleteTurn(isPlayerMove: true);
        }

        public void ExecuteComputerMove()
        {
            var bestMoves = AiEngine.CalculateComputerMoves(
                _boardLogic,
                _settings.AIDepth,
                _logicManager);

            if (!bestMoves.Any()) return;

            var move = bestMoves[new Random().Next(bestMoves.Count)];

            // capture
            if (Math.Abs(move.XTo - move.XFrom) == 2)
            {
                int midY = (move.YFrom + move.YTo) / 2;
                int midX = (move.XFrom + move.XTo) / 2;
                _boardButtons[midY, midX].Text = _settings.EmptySymbol;
                _boardLogic[midY, midX] = 0;
            }

            // move
            _boardButtons[move.YTo, move.XTo].Text = _boardButtons[move.YFrom, move.XFrom].Text;
            _boardLogic[move.YTo, move.XTo] = _boardLogic[move.YFrom, move.XFrom];
            _boardButtons[move.YFrom, move.XFrom].Text = _settings.EmptySymbol;
            _boardLogic[move.YFrom, move.XFrom] = 0;

            // promotion
            if (_boardLogic[move.YTo, move.XTo] == -1 &&
                move.YTo == _boardButtons.GetLength(0) - 1)
            {
                _boardButtons[move.YTo, move.XTo].Text = _settings.Player2Queen;
                _boardLogic[move.YTo, move.XTo] = -2;
            }

            CompleteTurn(isPlayerMove: false);
        }

        private void CompleteTurn(bool isPlayerMove)
        {
            bool playerTurn = _state.IsPlayer1Turn;
            if (_logicManager.CheckWin(_boardLogic, playerTurn))
                HandleWin(isPlayerMove);

            _boardManager.ResetMoveHighlights();
            _state.IsPlayer1Turn = !_state.IsPlayer1Turn;
            _ui.UpdateTurnLabel();
        }

        private void HandleWin(bool isPlayerMove)
        {
            bool player1Turn = _state.IsPlayer1Turn;

            // use the real names
            string winnerName = player1Turn
                ? _getPlayer1()
                : (_computer.AgainstComputer ? ComputerUser : _getPlayer2());

            _ui.GameTimer.Start();

            // award points
            _repo.UpdateScore(winnerName, 5);

            // record the game with real player names
            _repo.RecordWin(
                winnerName,
                _getPlayer1(),
                (_computer.AgainstComputer ? ComputerUser : _getPlayer2()),
                DateTime.Now);

            MessageBox.Show($"{winnerName} wins!");

            _ui.GameTimer.Stop();
            _state.WinnerAnnounced = 1;
            _ui.OnGameEnded();
        }

        public static DataTable ExecuteSelectQuery(string query, SqlConnection cnn)
        {
            using (var cmd = new SqlCommand(query, cnn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}