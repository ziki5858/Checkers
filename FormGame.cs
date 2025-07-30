using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Checkers.Data;

namespace Checkers
{
    public partial class FormGame : Form
    {
        // ----- Dependencies -----
        private readonly SqlConnection _connection;
        private readonly SqlGameResultRepository _repository;

        // ----- Player Info -----
        private readonly string _player1;
        private string _player2;

        // ----- Board Configuration -----
        private int _boardSize = 8;
        private int[,] _boardLogic;
        private Button[,] _boardButtons;
        private bool _isBoardBuilt;
        private Color _highlightColor;

        // ----- Game State -----
        private bool _isPlayer1Turn = true;
        private bool _againstComputer = false;
        private bool _computerMoveDone = false;
        private bool _modeChosen = false;
        private int _winnerAnnounced = 0;
        private int _currentScoreIndex = 0;

        // ----- Interaction Tracking -----
        private int _lastClickRow;
        private int _lastClickCol;

        // ----- Managers -----
        private GameBoardManager _boardManager;
        private GameLogicManager _logicManager;
        private GameFlowManager _flowManager;

        // ----- UI Symbols -----
        private const string Player1Symbol = "⬤";
        private const string Player2Symbol = "◯";
        private const string Player1Queen = "♛";
        private const string Player2Queen = "♕";
        private const string EmptySymbol = "";

        /// <summary>
        /// Initializes the form with database connection and first player name.
        /// </summary>
        public FormGame(SqlConnection connection, string player1)
        {
            InitializeComponent();
            _connection = connection;
            _player1 = player1;
            _repository = new SqlGameResultRepository(_connection);
            _highlightColor = btnColorPicker.BackColor;
            InitializeManagers();  // set up managers
        }

        #region Initialization Methods
        private void InitializeManagers()
        {
            _boardManager = new GameBoardManager(boardPanel, _boardSize, _highlightColor, UpdateTurnLabel);
            _boardButtons = _boardManager.GetBoardButtons();
            _boardLogic = _boardManager.GetBoardLogic();
            _logicManager = new GameLogicManager(_boardSize);
            _flowManager = CreateFlowManager();
        }

        private void EnsureBoardReady()
        {
            if (!_isBoardBuilt)
            {
                _boardManager.BuildBoard(OnCellClick);
                boardPanel.Resize += (s, e) => _boardManager.UpdateBoardLayout();
                _isBoardBuilt = true;
            }
            _boardManager.ResetBoard(Player1Symbol, Player2Symbol, EmptySymbol);
            _isPlayer1Turn = true;
            UpdateTurnLabel();
            // show controls
            lblBoardSize.Visible =
            lblPlayerColor.Visible =
            lblCurrentTurn.Visible =
            radioSize6.Visible =
            radioSize8.Visible =
            radioSize10.Visible =
            btnColorPicker.Visible = true;
        }

        private GameFlowManager CreateFlowManager()
        {
            // 1. Create game settings (use positional args or the actual parameter names)
            var settings = new GameSettings(
                Player1Queen,
                Player2Queen,
                EmptySymbol,
                6);

            // 2. Game state tracker
            var stateTracker = new FuncGameStateTracker(
                () => _winnerAnnounced,
                v => _winnerAnnounced = v,
                () => _isPlayer1Turn,
                v => _isPlayer1Turn = v
            );

            // 3. Computer controller
            var computerController = new FuncComputerController(
                () => _againstComputer,
                () => _computerMoveDone,
                v => _computerMoveDone = v
            );

            // 4. UI adapter
            var gameUI = new DelegateGameUI(
                UpdateTurnLabel,
                () => _modeChosen = false,
                turnTimer
            );

            // 5. Wrap your existing SqlGameResultRepository in the adapter
            IGameRepository repo = new SqlGameResultRepositoryAdapter(_connection);

            // 6. Finally create the flow manager (positional args)
             return new GameFlowManager(
                 _boardLogic,
                 _boardButtons,
                 _boardManager,
                 _logicManager,
                 settings,
                 stateTracker,
                 computerController,
                 gameUI,
                 repo,
                 getPlayer1: () => _player1,
                 getPlayer2: () => _player2
             );

        }

        #endregion

        #region Cell Click & Move Logic

        private void OnCellClick(object sender, EventArgs e)
        {
            if (!_isPlayer1Turn && _againstComputer) return;
            var btn = (Button)sender;
            int idx = (int)btn.Tag;
            int row = idx / _boardButtons.GetLength(1);
            int col = idx % _boardButtons.GetLength(1);
            if (IsSelectingPiece(row, col))
            {
                _boardManager.ResetMoveHighlights();
                var moves = _logicManager.GetMoves(_boardLogic, row, col, _isPlayer1Turn);
                _boardManager.HighlightAvailableMoves(moves);
                _lastClickRow = row;
                _lastClickCol = col;
            }
            else if (IsChoosingMove(row, col))
                _flowManager.ExecutePlayerMove(_lastClickRow, _lastClickCol, row, col);
            if (!_isPlayer1Turn && _againstComputer && !_computerMoveDone)
                _flowManager.ExecuteComputerMove();
        }

        private bool IsSelectingPiece(int r, int c)
            => (_isPlayer1Turn && _boardLogic[r, c] > 0) || (!_isPlayer1Turn && _boardLogic[r, c] < 0);

        private bool IsChoosingMove(int r, int c)
            => _boardButtons[r, c].BackColor.ToArgb() == _highlightColor.ToArgb();

        #endregion

        #region UI Event Handlers

        private void UpdateTurnLabel()
        {
            if (_winnerAnnounced != 0)
            {
                lblCurrentTurn.Text = string.Empty;
                return;
            }
            string current = _isPlayer1Turn ? _player1 : (_againstComputer ? "Computer" : _player2);
            lblCurrentTurn.Text = $"Turn of: {current}";
        }

        private void radioSize_CheckedChanged(object sender, EventArgs e)
        {
            _boardSize = int.Parse(((RadioButton)sender).Text);
            _isBoardBuilt = false;
            InitializeManagers();
            EnsureBoardReady();
        }
        private void radioButton1_CheckedChanged(object s, EventArgs e) => radioSize_CheckedChanged(s, e);

        private void btn_twoPlayers(object sender, EventArgs e) => StartGameMode(false);
        private void btn_againstCom(object sender, EventArgs e) => StartGameMode(true);
        private void btn_changeColor(object sender, EventArgs e)
        {
            _boardManager.PickHighlightColor(btnColorPicker);
            _highlightColor = btnColorPicker.BackColor;
        }
        private void btn_gameRoles(object sender, EventArgs e)
            => GeneralHelper.ShowInfo("Play vs computer or friend.\nNo double/backward jumps except queen.\nQueen moves one slot.\nPlayer 1 starts.");
        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{CAPSLOCK}"); SendKeys.Send("{NUMLOCK}"); SendKeys.Send("{SCROLLLOCK}");
        }

        #endregion

        #region Game Mode & Scoring

        private void StartGameMode(bool againstCom)
        {
            if (_modeChosen)
            {
                GeneralHelper.ShowError("Game mode already chosen."); return;
            }
            _againstComputer = againstCom;
            _currentScoreIndex = 0;
            ShowPlayerScore(_player1);
            if (!againstCom) LoginSecondPlayer();
            EnsureBoardReady();
            _modeChosen = true;
        }

        private void ShowPlayerScore(string player)
        {
            int score = _repository.GetScore(player);
            string text = $"Score of {player}: {score}";
            if (_currentScoreIndex == 0) lblPlayerOneScore.Text = text;
            else lblPlayerTwoScore.Text = text;
        }

        private void LoginSecondPlayer()
        {
            using (var loginForm = new FormLogIn(_connection))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    if (_player1 == loginForm.Player)
                    {
                        GeneralHelper.ShowErrorConfirm("Choose a different player.");
                        LoginSecondPlayer();
                        return;
                    }
                    _player2 = loginForm.Player;
                    _currentScoreIndex = 1;
                    ShowPlayerScore(_player2);
                }
            }
        }

        #endregion
    }
}
