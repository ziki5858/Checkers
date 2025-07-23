namespace Checkers
{
    partial class FormNewUser
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.TextBox textBox_ans;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.GroupBox groupBox_SecurityQuestion;
        private System.Windows.Forms.RadioButton radioButton_fathern;
        private System.Windows.Forms.RadioButton radioButton_mathern;
        private System.Windows.Forms.Label label_Answer;
        private System.Windows.Forms.CheckBox checkBox_ShowPass;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;

        /// <summary>
        /// Designer-generated code for layout and controls with enhanced styling using radio buttons.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.textBox_ans = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.groupBox_SecurityQuestion = new System.Windows.Forms.GroupBox();
            this.radioButton_fathern = new System.Windows.Forms.RadioButton();
            this.radioButton_mathern = new System.Windows.Forms.RadioButton();
            this.label_Answer = new System.Windows.Forms.Label();
            this.checkBox_ShowPass = new System.Windows.Forms.CheckBox();
            this.button_Add = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox_SecurityQuestion.SuspendLayout();
            this.SuspendLayout();

            // FormNewUser
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AcceptButton = this.button_Add;
            this.ClientSize = new System.Drawing.Size(460, 420);
            this.Text = "Create New User";
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // label_name
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label_name.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label_name.Location = new System.Drawing.Point(40, 40);
            this.label_name.Name = "label_name";
            this.label_name.Text = "Username:";

            // textBox_Name
            this.textBox_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Name.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox_Name.Location = new System.Drawing.Point(160, 38);
            this.textBox_Name.MaxLength = 10;
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(200, 23);
            this.textBox_Name.TabIndex = 0;
            this.textBox_Name.TextChanged += new System.EventHandler(this.ValidateForm);

            // label_password
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label_password.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label_password.Location = new System.Drawing.Point(40, 85);
            this.label_password.Name = "label_password";
            this.label_password.Text = "Password:";

            // textBox_pass
            this.textBox_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_pass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox_pass.Location = new System.Drawing.Point(160, 83);
            this.textBox_pass.MaxLength = 10;
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.Size = new System.Drawing.Size(200, 23);
            this.textBox_pass.TabIndex = 1;
            this.textBox_pass.UseSystemPasswordChar = true;
            this.textBox_pass.TextChanged += new System.EventHandler(this.ValidateForm);

            // checkBox_ShowPass
            this.checkBox_ShowPass.AutoSize = true;
            this.checkBox_ShowPass.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.checkBox_ShowPass.ForeColor = System.Drawing.Color.Gray;
            this.checkBox_ShowPass.Location = new System.Drawing.Point(370, 85);
            this.checkBox_ShowPass.Name = "checkBox_ShowPass";
            this.checkBox_ShowPass.Text = "Show";
            this.checkBox_ShowPass.UseVisualStyleBackColor = true;
            this.checkBox_ShowPass.CheckedChanged += new System.EventHandler(this.checkBox_ShowPass_CheckedChanged);

            // groupBox_SecurityQuestion
            this.groupBox_SecurityQuestion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox_SecurityQuestion.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox_SecurityQuestion.Location = new System.Drawing.Point(40, 130);
            this.groupBox_SecurityQuestion.Name = "groupBox_SecurityQuestion";
            this.groupBox_SecurityQuestion.Size = new System.Drawing.Size(380, 100);
            this.groupBox_SecurityQuestion.Text = "Security Question:";

            // radioButton_fathern
            this.radioButton_fathern.AutoSize = true;
            this.radioButton_fathern.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButton_fathern.ForeColor = System.Drawing.Color.Black;
            this.radioButton_fathern.Location = new System.Drawing.Point(20, 28);
            this.radioButton_fathern.Name = "radioButton_fathern";
            this.radioButton_fathern.Text = "Your father name";
            this.radioButton_fathern.UseVisualStyleBackColor = true;
            this.radioButton_fathern.CheckedChanged += new System.EventHandler(this.ValidateForm);

            // radioButton_mathern
            this.radioButton_mathern.AutoSize = true;
            this.radioButton_mathern.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButton_mathern.ForeColor = System.Drawing.Color.Black;
            this.radioButton_mathern.Location = new System.Drawing.Point(20, 60);
            this.radioButton_mathern.Name = "radioButton_mathern";
            this.radioButton_mathern.Text = "Your mother name";
            this.radioButton_mathern.UseVisualStyleBackColor = true;
            this.radioButton_mathern.CheckedChanged += new System.EventHandler(this.ValidateForm);

            this.groupBox_SecurityQuestion.Controls.Add(this.radioButton_fathern);
            this.groupBox_SecurityQuestion.Controls.Add(this.radioButton_mathern);

            // label_Answer
            this.label_Answer.AutoSize = true;
            this.label_Answer.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label_Answer.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label_Answer.Location = new System.Drawing.Point(40, 250);
            this.label_Answer.Name = "label_Answer";
            this.label_Answer.Text = "Answer:";

            // textBox_ans
            this.textBox_ans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ans.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox_ans.Location = new System.Drawing.Point(160, 248);
            this.textBox_ans.MaxLength = 50;
            this.textBox_ans.Name = "textBox_ans";
            this.textBox_ans.Size = new System.Drawing.Size(260, 23);
            this.textBox_ans.TabIndex = 3;
            this.textBox_ans.TextChanged += new System.EventHandler(this.ValidateForm);

            // button_Add
            this.button_Add.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.button_Add.ForeColor = System.Drawing.Color.White;
            this.button_Add.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.button_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Add.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.button_Add.FlatAppearance.BorderSize = 1;
            this.button_Add.Location = new System.Drawing.Point(180, 300);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(100, 30);
            this.button_Add.TabIndex = 4;
            this.button_Add.Text = "Create";
            this.button_Add.UseVisualStyleBackColor = false;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);

            // toolTip1
            this.toolTip1.BackColor = System.Drawing.Color.LightYellow;
            this.toolTip1.ForeColor = System.Drawing.Color.Black;
            this.toolTip1.SetToolTip(this.textBox_Name, "Enter username (max 10 chars)");
            this.toolTip1.SetToolTip(this.textBox_pass, "Enter a secure password");
            this.toolTip1.SetToolTip(this.groupBox_SecurityQuestion, "Select one question");
            this.toolTip1.SetToolTip(this.textBox_ans, "Answer to chosen question");

            // errorProvider1
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;

            // Add controls to form
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.checkBox_ShowPass);
            this.Controls.Add(this.groupBox_SecurityQuestion);
            this.Controls.Add(this.label_Answer);
            this.Controls.Add(this.textBox_ans);
            this.Controls.Add(this.button_Add);

            this.groupBox_SecurityQuestion.ResumeLayout(false);
            this.groupBox_SecurityQuestion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
