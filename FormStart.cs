using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Checkers
{
    public partial class FormStart : Form
    {
        private SqlConnection cnn;
        private string _currentUser;

        /// <summary>
        /// Constructor: Initializes UI components and database connection.
        /// </summary>
        public FormStart()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            try
            {
                const string folder = "Data";
                const string dbFileName = "Checkers_DB - 2015.mdf";
           
                string dbPath = Path.Combine(Application.StartupPath, folder, dbFileName);

                if (!File.Exists(dbPath))
                {
                    GeneralHelper.ShowErrorConfirm($"Database file not found:\n{dbPath}");
                    Application.Exit();
                    return;
                }

                string connStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                ConnectUser();
            }
            catch (Exception ex)
            {
                GeneralHelper.ShowErrorConfirm("Error connecting to database:\n" + ex.Message);
                Application.Exit();
            }
        }


        /// <summary>
        /// Displays login form and captures authenticated user. Exits on cancel.
        /// </summary>
        private void ConnectUser()
        {
            using (var loginForm = new FormLogIn(cnn))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                    _currentUser = loginForm.Player;
                else
                    Application.Exit();
            }
        }

        /// <summary>
        /// Starts a new game session for the authenticated user.
        /// </summary>
        private void btn_Game_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentUser))
            {
                GeneralHelper.ShowError("Please log in first.");
                return;
            }

            using (var gameForm = new FormGame(cnn, _currentUser))
                gameForm.ShowDialog();
        }

        /// <summary>
        /// Opens the player statistics window.
        /// </summary>
        private void BtnStatics_Click(object sender, EventArgs e)
        {
            var staticsForm = new FormPlayer(cnn);
            staticsForm.Show();
        }

        /// <summary>
        /// Opens the games-played statistics window.
        /// </summary>
        private void btn_StaticsGP_Click(object sender, EventArgs e)
        {
            var statsGameForm = new FormGamesPlayed(cnn, _currentUser);
            statsGameForm.Show();
        }
    }
}