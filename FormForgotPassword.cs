using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Checkers.Data;

namespace Checkers
{
    public partial class FormForgotPassword : Form
    {
        private readonly SqlGameResultRepository _repo;

        /// <summary>
        /// Initializes the 'Forgot Password' form and loads usernames into the combo box.
        /// </summary>
        /// <param name="cnn">An open SQL connection.</param>
        public FormForgotPassword(SqlConnection cnn)
        {
            InitializeComponent();
            _repo = new SqlGameResultRepository(cnn);

            try
            {
                // Load all usernames into the dropdown
                DataTable users = _repo.GetAllUsernames();
                cmbUsers.DataSource = users;
                cmbUsers.DisplayMember = "Username";
                cmbUsers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _repo.LogError(ex);
                GeneralHelper.ShowErrorConfirm("Error loading users:\n" + ex.Message);
            }
        }

        /// <summary>
        /// Handles user selection change by loading the security question.
        /// </summary>
        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear previous state
            textBoxAnswer.Clear();
            dgvQuestion.DataSource = null;
            dgvQuestion.Refresh();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string user = cmbUsers.Text;
                DataTable qTable = _repo.GetPrivateQuestionTable(user);
                dgvQuestion.DataSource = qTable;
            }
            catch (Exception ex)
            {
                _repo.LogError(ex);
                GeneralHelper.ShowErrorConfirm("Error loading security question:\n" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Verifies the provided answer and opens the reset-password form if correct.
        /// </summary>
        private void btnCheckAnswer_Click(object sender, EventArgs e)
        {
            string username = cmbUsers.Text;
            string answer = textBoxAnswer.Text.Trim();

            if (string.IsNullOrWhiteSpace(answer))
            {
                GeneralHelper.ShowErrorConfirm("Please enter your answer.");
                return;
            }

            try
            {
                btnCheckAnswer.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                if (!IsAnswerCorrect(username, answer))
                {
                    GeneralHelper.ShowErrorConfirm("Wrong answer, try again.");
                    return;
                }

                OpenResetForm(username);
            }
            catch (Exception ex)
            {
                _repo.LogError(ex);
                GeneralHelper.ShowErrorConfirm("An unexpected error occurred. Please contact support.");
            }
            finally
            {
                btnCheckAnswer.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Checks if the provided answer matches the stored one (case-insensitive).
        /// </summary>
        private bool IsAnswerCorrect(string user, string providedAnswer)
        {
            // Retrieve the correct answer from repository
            string correct = _repo.GetAnswer(user);

            if (string.IsNullOrEmpty(correct))
            {
                GeneralHelper.ShowErrorConfirm("User not found.");
                return false;
            }

            // Compare answers without case or culture effects
            return string.Equals(
                providedAnswer,
                correct.Trim(),
                StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Opens the password reset dialog and updates password on success.
        /// </summary>
        private void OpenResetForm(string user)
        {
            using (var resetForm = new FormResetPassword())
            {
                if (resetForm.ShowDialog(this) == DialogResult.OK)
                {
                    string newPass = resetForm.NewPassword;
                    _repo.ChangePassword(user, newPass);
                    GeneralHelper.ShowInfo("Your password has been reset.");
                    this.Close();
                }
            }
        }
    }
}