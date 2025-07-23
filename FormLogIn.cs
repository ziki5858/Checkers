using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Checkers.Data;

namespace Checkers
{
    public partial class FormLogIn : Form
    {
        // ----- Public Properties -----
        /// <summary>Username of the successfully logged-in player.</summary>
        public string Player { get; private set; }

        // ----- Dependencies -----
        private readonly SqlConnection _connection;
        private readonly SqlGameResultRepository _repository;

        /// <summary>
        /// Initializes the login form with a SQL connection.
        /// </summary>
        public FormLogIn(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            _repository = new SqlGameResultRepository(_connection);
        }

        #region Event Handlers

        /// <summary>
        /// Attempts to log in the user with provided credentials.
        /// </summary>
        private void btn_LogIn_Click(object sender, EventArgs e)
        {
            string username = textBox_User.Text.Trim();
            string password = textBox_Password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                GeneralHelper.ShowError("Please enter both username and password.");
                return;
            }

            // Validate username existence
            if (!_repository.UsernameExists(username))
            {
                GeneralHelper.ShowError("Username not found.");
                return;
            }

            // Validate password
            if (!_repository.ValidatePassword(username, password))
            {
                GeneralHelper.ShowError("Wrong password.");
                return;
            }

            // Success
            Player = username;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Opens the Forgot Password dialog.
        /// </summary>
        private void btn_Forgot_Click(object sender, EventArgs e)
        {
            using (var forgotForm = new FormForgotPassword(_connection))
            {
                forgotForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Opens the New User registration form.
        /// </summary>
        private void btn_NewUser_Click(object sender, EventArgs e)
        {
            using (var newUserForm = new FormNewUser(_connection))
            {
                newUserForm.ShowDialog(this);
            }
        }

        #endregion
    }
}
