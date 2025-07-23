using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Checkers.Data;

namespace Checkers
{
    public partial class FormNewUser : Form
    {
        private readonly SqlGameResultRepository _repo;

        /// <summary>
        /// Ctor: Initializes form, repository, and validation state.
        /// </summary>
        public FormNewUser(SqlConnection cnn)
        {
            InitializeComponent();
            _repo = new SqlGameResultRepository(cnn);
            ValidateForm(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Add button click: validates input, checks username, creates player.
        /// </summary>
        private void button_Add_Click(object sender, EventArgs e)
        {
            string username = textBox_Name.Text.Trim();
            string password = textBox_pass.Text.Trim();
            string answer = textBox_ans.Text.Trim();
            string privateQuestion = radioButton_fathern.Checked
                                     ? radioButton_fathern.Text
                                     : radioButton_mathern.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(answer) ||
                (!radioButton_fathern.Checked && !radioButton_mathern.Checked))
            {
                GeneralHelper.ShowError("Please fill all fields");
                return;
            }

            if (_repo.UsernameExists(username))
            {
                GeneralHelper.ShowErrorConfirm("Username is already taken");
                return;
            }

            _repo.CreatePlayer(username, password, privateQuestion, answer);
            GeneralHelper.ShowInfo($"New player successfully created. Welcome: {username}");
            Close();
        }

        /// <summary>
        /// Toggle password visibility when checkbox is clicked.
        /// </summary>
        private void checkBox_ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            textBox_pass.UseSystemPasswordChar = !checkBox_ShowPass.Checked;
        }

        /// <summary>
        /// Enable the Add button only when all fields are filled, and set error if not.
        /// </summary>
        private void ValidateForm(object sender, EventArgs e)
        {
            bool allFilled =
                !string.IsNullOrWhiteSpace(textBox_Name.Text) &&
                !string.IsNullOrWhiteSpace(textBox_pass.Text) &&
                !string.IsNullOrWhiteSpace(textBox_ans.Text) &&
                (radioButton_fathern.Checked || radioButton_mathern.Checked);

            button_Add.Enabled = allFilled;
            errorProvider1.SetError(button_Add, allFilled ? "" : "Please fill all fields above");
        }
    }
}