using System;
using System.Windows.Forms;

namespace Checkers
{
    /// <summary>
    /// Dialog for entering and confirming a new password.
    /// </summary>
    public partial class FormResetPassword : Form
    {
        /// <summary>
        /// The new password entered by the user.
        /// </summary>
        public string NewPassword { get; private set; }

        /// <summary>
        /// Initializes the reset password form components.
        /// </summary>
        public FormResetPassword()
        {
            InitializeComponent();
            // Mask password entry
            txtNew.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;
        }

        #region Event Handlers

        /// <summary>
        /// Validates input and sets DialogResult to OK if validation passes.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            string newPwd = txtNew.Text.Trim();
            string confirmPwd = txtConfirm.Text.Trim();

            // Check for empty
            if (string.IsNullOrEmpty(newPwd))
            {
                GeneralHelper.ShowErrorConfirm("Password cannot be empty.");
                txtNew.Focus();
                return;
            }

            // Check minimum length
            if (newPwd.Length < 6)
            {
                GeneralHelper.ShowErrorConfirm("Password must be at least 6 characters long.");
                txtNew.Focus();
                return;
            }

            // Check match
            if (!newPwd.Equals(confirmPwd, StringComparison.Ordinal))
            {
                GeneralHelper.ShowErrorConfirm("Passwords do not match.");
                txtConfirm.Focus();
                return;
            }

            // All good
            NewPassword = newPwd;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancels the password reset operation.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}