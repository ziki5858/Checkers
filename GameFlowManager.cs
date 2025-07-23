using Checkers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Checkers
{
    /// <summary>
    /// Manages the game flow including player and computer moves, turn tracking, win detection, and database updates.
    /// </summary>
    internal class GameFlowManager
    {
        // ----- Board & UI -----
        private readonly int[,] boardLogic;
        private readonly Button[,] boardButtons;
        private readonly GameBoardManager boardManager;
        private readonly GameLogicManager gameLogicManager;

        // ----- Callbacks & Database -----
        private readonly Action updateTurnLabel;
        private readonly SqlConnection cnn;

        // ----- Player Providers -----
        private readonly Func<string> getPlayer1;
        private readonly Func<string> getPlayer2;

        // ----- Flags -----
        private readonly Func<bool> getAgainstComputer;
        private readonly Func<bool> getComputerMoveDone;
        private readonly Func<bool> getIsPlayer1Turn;
        private readonly Action<bool> setComputerMoveDone;
        private readonly Action<bool> setIsPlayer1Turn;

        // ----- Turn & Winner Tracking -----
        private readonly Func<int> getCurrentTurn;
        private readonly Func<int> getWinnerAnnounced;
        private readonly Action<int> setWinnerAnnounced;

        // ----- Game Symbols & AI Depth -----
        private readonly string PLAYER1_QUEEN;
        private readonly string PLAYER2_QUEEN;
        private readonly string EMPTY;
        private readonly int computerDifficultyDepth;

        // ----- Timer & End-of-Game Callback -----
        private readonly Timer timer;
        private readonly Action onGameEnded;

        // ----- Repository -----
        private readonly SqlGameResultRepository repo;

        /// <summary>
        /// Initializes a new instance of the GameFlowManager class with all required dependencies.
        /// </summary>
        public GameFlowManager(
            int[,] boardLogic,
            Button[,] boardButtons,
            GameBoardManager boardManager,
            GameLogicManager gameLogicManager,
            Action updateTurnLabel,
            SqlConnection cnn,
            Func<string> getPlayer1,
            Func<string> getPlayer2,
            Func<bool> getAgainstComputer,
            string player1QueenSymbol,
            string player2QueenSymbol,
            string emptySymbol,
            Timer timer,
            Func<int> getCurrentTurn,
            Func<int> getWinnerAnnounced,
            Action<int> setWinnerAnnounced,
            Func<bool> getComputerMoveDone,
            Action<bool> setComputerMoveDone,
            Func<bool> getIsPlayer1Turn,
            Action<bool> setIsPlayer1Turn,
            Action onGameEnded,
            SqlGameResultRepository repo,
            int computerDifficultyDepth = 6)
        {
            this.boardLogic = boardLogic;
            this.boardButtons = boardButtons;
            this.boardManager = boardManager;
            this.gameLogicManager = gameLogicManager;
            this.updateTurnLabel = updateTurnLabel;
            this.cnn = cnn;
            this.getPlayer1 = getPlayer1;
            this.getPlayer2 = getPlayer2;
            this.getAgainstComputer = getAgainstComputer;
            PLAYER1_QUEEN = player1QueenSymbol;
            PLAYER2_QUEEN = player2QueenSymbol;
            EMPTY = emptySymbol;
            this.timer = timer;
            this.getCurrentTurn = getCurrentTurn;
            this.getWinnerAnnounced = getWinnerAnnounced;
            this.setWinnerAnnounced = setWinnerAnnounced;
            this.getComputerMoveDone = getComputerMoveDone;
            this.setComputerMoveDone = setComputerMoveDone;
            this.getIsPlayer1Turn = getIsPlayer1Turn;
            this.setIsPlayer1Turn = setIsPlayer1Turn;
            this.onGameEnded = onGameEnded;
            this.repo = repo;
            this.computerDifficultyDepth = computerDifficultyDepth;
        }

        /// <summary>
        /// Executes a move initiated by the human player, including capture and promotion.
        /// </summary>
        public void ExecutePlayerMove(int fromY, int fromX, int toY, int toX)
        {
            int rows = boardButtons.GetLength(0);
            int cols = boardButtons.GetLength(1);
            // Ensure coordinates are valid
            if (fromY < 0 || fromX < 0 || toY < 0 || toX < 0 ||
                fromY >= rows || fromX >= cols || toY >= rows || toX >= cols)
            {
                MessageBox.Show("Invalid move: out of bounds.");
                return;
            }

            // If capturing a piece by jumping two squares
            if (Math.Abs(toX - fromX) == 2)
            {
                int midY = (fromY + toY) / 2;
                int midX = (fromX + toX) / 2;
                boardButtons[midY, midX].Text = EMPTY;
                boardLogic[midY, midX] = 0;
            }

            // Move piece visually and in logic
            boardButtons[toY, toX].Text = boardButtons[fromY, fromX].Text;
            boardLogic[toY, toX] = boardLogic[fromY, fromX];
            boardButtons[fromY, fromX].Text = EMPTY;
            boardLogic[fromY, fromX] = 0;

            // Check for promotion to queen
            if (toY == 0 && boardLogic[toY, toX] == 1)
            {
                boardButtons[toY, toX].Text = PLAYER1_QUEEN;
                boardLogic[toY, toX] = 2;
            }
            else if (toY == rows - 1 && boardLogic[toY, toX] == -1)
            {
                boardButtons[toY, toX].Text = PLAYER2_QUEEN;
                boardLogic[toY, toX] = -2;
            }

            CompleteTurn(isPlayerMove: true);
        }

        /// <summary>
        /// Determines and executes a move for the computer using the AI engine.
        /// </summary>
        public void ExecuteComputerMove()
        {
            var possibleMoves = AiEngine.CalculateComputerMoves(
                boardLogic, computerDifficultyDepth, gameLogicManager);
            if (!possibleMoves.Any()) return;

            // Select a random move from the best moves
            var move = possibleMoves[new Random().Next(possibleMoves.Count)];
            // Capture logic
            if (Math.Abs(move.XTo - move.XFrom) == 2)
            {
                int midY = (move.YFrom + move.YTo) / 2;
                int midX = (move.XFrom + move.XTo) / 2;
                boardButtons[midY, midX].Text = EMPTY;
                boardLogic[midY, midX] = 0;
            }

            // Execute the move
            boardButtons[move.YTo, move.XTo].Text = boardButtons[move.YFrom, move.XFrom].Text;
            boardLogic[move.YTo, move.XTo] = boardLogic[move.YFrom, move.XFrom];
            boardButtons[move.YFrom, move.XFrom].Text = EMPTY;
            boardLogic[move.YFrom, move.XFrom] = 0;

            // Promotion for computer
            if (boardLogic[move.YTo, move.XTo] == -1 && move.YTo == boardButtons.GetLength(0) - 1)
            {
                boardButtons[move.YTo, move.XTo].Text = PLAYER2_QUEEN;
                boardLogic[move.YTo, move.XTo] = -2;
            }

            CompleteTurn(isPlayerMove: false);
        }

        /// <summary>
        /// Completes a turn by checking for victory, resetting highlights, toggling turn, and updating UI.
        /// </summary>
        private void CompleteTurn(bool isPlayerMove)
        {
            bool playerTurn = getCurrentTurn() % 2 == 0;
            if (gameLogicManager.CheckWin(boardLogic, playerTurn))
            {
                HandleWin(isPlayerMove);
            }

            boardManager.ResetMoveHighlights();
            setWinnerAnnounced(getWinnerAnnounced());
            setIsPlayer1Turn(!getIsPlayer1Turn());
            updateTurnLabel();
        }

        /// <summary>
        /// Handles win scenario: updates database, shows message, and signals game end.
        /// </summary>
        private void HandleWin(bool isPlayerMove)
        {
            string winnerName = getCurrentTurn() % 2 == 0
                ? getPlayer1()
                : (getAgainstComputer() ? "Computer" : getPlayer2());

            // Start and stop timer for decoration
            timer.Start();

            // Award points in database
            repo.UpdateScore(getPlayer1(), getCurrentTurn() % 2 == 0 ? 5 : 0);
            if (!getAgainstComputer())
                repo.UpdateScore(getPlayer2(), getCurrentTurn() % 2 != 0 ? 5 : 0);

            MessageBox.Show($"{winnerName} wins!");

            // Record the game result
            repo.RecordWin(
                winnerName,
                getPlayer1(),
                getAgainstComputer() ? "Computer" : getPlayer2(),
                DateTime.Now
            );

            timer.Stop();
            setWinnerAnnounced(1);
            onGameEnded?.Invoke();
        }

        /// <summary>
        /// Executes an arbitrary SELECT SQL query and returns the results.
        /// </summary>
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

        /// <summary>
        /// Analyzes and classifies all possible computer moves into winning, threat, and risky categories.
        /// </summary>
        private (List<Move> winning, List<Move> threats, List<Move> risky) AnalyzeComputerMoves()
        {
            var winningMoves = new List<Move>();
            var enemyThreats = new List<Move>();
            var riskyMoves = new List<Move>();

            int rows = boardButtons.GetLength(0);
            int cols = boardButtons.GetLength(1);
            bool playerTurn = getCurrentTurn() % 2 == 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // Only evaluate computer's pieces (negative values)
                    if (boardLogic[r, c] >= 0) continue;

                    var moves = gameLogicManager.GetMoves(boardLogic, r, c, playerTurn);
                    foreach (var move in moves)
                    {
                        AiEngine.ClassifyMove(
                            move,
                            boardLogic,
                            gameLogicManager,
                            ref winningMoves,
                            ref enemyThreats,
                            ref riskyMoves,
                            depth: computerDifficultyDepth
                        );
                    }
                }
            }

            return (winningMoves, enemyThreats, riskyMoves);
        }
    }
}
