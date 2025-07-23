using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Checkers.Data;

namespace Checkers
{
    public partial class FormGamesPlayed : Form
    {
        // ----- Dependencies -----
        private readonly SqlGameResultRepository _repository;
        private readonly string _currentUser;

        // ----- View State -----
        private bool _showAllGames = true;

        /// <summary>
        /// Initializes the form with repository and current user, applies styling.
        /// </summary>
        public FormGamesPlayed(SqlConnection connection, string currentUser)
        {
            InitializeComponent();
            _repository = new SqlGameResultRepository(connection);
            _currentUser = currentUser;

            ApplyStyling();
            ToggleDeleteButton();

            LoadGames();
        }

        #region Initialization & Styling

        /// <summary>
        /// Applies consistent header and grid styles.
        /// </summary>
        private void ApplyStyling()
        {
            // Header label
            lblHeader.BackColor = Color.FromArgb(44, 62, 80);
            lblHeader.ForeColor = Color.White;

            // DataGridView selection colors
            dataGridView_GamesPlayed.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dataGridView_GamesPlayed.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        /// <summary>
        /// Shows or hides the Delete button based on user role.
        /// </summary>
        private void ToggleDeleteButton()
        {
            btnDeleteGame.Visible = _currentUser.Equals("admin", StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Data Loading

        /// <summary>
        /// Loads either all games or details for the selected game.
        /// </summary>
        private void LoadGames()
        {
            DataTable table;
            if (_showAllGames)
            {
                table = _repository.GetAllGames();
            }
            else
            {
                table = LoadSelectedGame();
                if (table == null) return;
            }

            dataGridView_GamesPlayed.DataSource = table;
            _showAllGames = !_showAllGames; // toggle next view
        }

        /// <summary>
        /// Retrieves details for the currently selected game.
        /// </summary>
        private DataTable LoadSelectedGame()
        {
            if (dataGridView_GamesPlayed.CurrentRow == null) return null;

            var cell = dataGridView_GamesPlayed.CurrentRow.Cells["Game number"].Value;
            if (cell == null || !int.TryParse(cell.ToString(), out var gameNum))
            {
                GeneralHelper.ShowError("Invalid game number.");
                return null;
            }

            return _repository.GetGameByNumber(gameNum);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles click on grid rows to toggle between all and detail view.
        /// </summary>
        private void dataGridView_GamesPlayed_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LoadGames();
        }

        /// <summary>
        /// Deletes the currently selected game (admin only).
        /// </summary>
        private void btn_DeleteGame_click(object sender, EventArgs e)
        {
            if (dataGridView_GamesPlayed.CurrentRow == null) return;

            var cell = dataGridView_GamesPlayed.CurrentRow.Cells["Game number"].Value;
            if (cell == null || !int.TryParse(cell.ToString(), out var gameNum))
            {
                GeneralHelper.ShowError("Invalid game number.");
                return;
            }

            try
            {
                _repository.DeleteGame(gameNum, _currentUser);
                GeneralHelper.ShowInfo("Game deleted successfully.");
                _showAllGames = true; // reset to initial view
                LoadGames();
            }
            catch (InvalidOperationException ex)
            {
                GeneralHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                GeneralHelper.ShowError($"Error deleting game: {ex.Message}");
            }
        }

        /// <summary>
        /// Toggles between all games and detail view.
        /// </summary>
        private void btnToggleView_Click(object sender, EventArgs e)
        {
            LoadGames();
        }

        #endregion
    }
}
