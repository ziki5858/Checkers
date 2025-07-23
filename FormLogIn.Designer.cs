namespace Checkers
{
    partial class FormLogIn
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label_Welcome;
        private System.Windows.Forms.Label label_User;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Button btn_LogIn;
        private System.Windows.Forms.Button btn_NewUser;
        private System.Windows.Forms.Button btn_Forgot;
        private System.Windows.Forms.ToolTip toolTip1;

        /// <summary>
        /// Sets up login form layout, initializes and styles all controls.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_Welcome = new System.Windows.Forms.Label();
            this.label_User = new System.Windows.Forms.Label();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.label_Password = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.btn_LogIn = new System.Windows.Forms.Button();
            this.btn_NewUser = new System.Windows.Forms.Button();
            this.btn_Forgot = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label_Welcome
            // 
            this.label_Welcome.AutoSize = true;
            this.label_Welcome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label_Welcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label_Welcome.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label_Welcome.Location = new System.Drawing.Point(127, 9);
            this.label_Welcome.Name = "label_Welcome";
            this.label_Welcome.Padding = new System.Windows.Forms.Padding(5);
            this.label_Welcome.Size = new System.Drawing.Size(213, 47);
            this.label_Welcome.TabIndex = 0;
            this.label_Welcome.Text = "Welcome Back";
            // 
            // label_User
            // 
            this.label_User.AutoSize = true;
            this.label_User.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_User.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label_User.Location = new System.Drawing.Point(50, 90);
            this.label_User.Name = "label_User";
            this.label_User.Size = new System.Drawing.Size(91, 23);
            this.label_User.TabIndex = 1;
            this.label_User.Text = "Username:";
            // 
            // textBox_User
            // 
            this.textBox_User.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox_User.Location = new System.Drawing.Point(160, 90);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(220, 27);
            this.textBox_User.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBox_User, "Enter your username");
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_Password.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label_Password.Location = new System.Drawing.Point(50, 140);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(84, 23);
            this.label_Password.TabIndex = 3;
            this.label_Password.Text = "Password:";
            // 
            // textBox_Password
            // 
            this.textBox_Password.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox_Password.Location = new System.Drawing.Point(160, 140);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(220, 27);
            this.textBox_Password.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBox_Password, "Enter your password");
            this.textBox_Password.UseSystemPasswordChar = true;
            // 
            // btn_LogIn
            // 
            this.btn_LogIn.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_LogIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LogIn.FlatAppearance.BorderSize = 0;
            this.btn_LogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LogIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_LogIn.ForeColor = System.Drawing.Color.White;
            this.btn_LogIn.Location = new System.Drawing.Point(300, 190);
            this.btn_LogIn.Name = "btn_LogIn";
            this.btn_LogIn.Size = new System.Drawing.Size(118, 35);
            this.btn_LogIn.TabIndex = 5;
            this.btn_LogIn.Text = "Log In";
            this.toolTip1.SetToolTip(this.btn_LogIn, "Log in with credentials");
            this.btn_LogIn.UseVisualStyleBackColor = false;
            this.btn_LogIn.Click += new System.EventHandler(this.btn_LogIn_Click);
            // 
            // btn_NewUser
            // 
            this.btn_NewUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_NewUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NewUser.FlatAppearance.BorderSize = 0;
            this.btn_NewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NewUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_NewUser.ForeColor = System.Drawing.Color.Black;
            this.btn_NewUser.Location = new System.Drawing.Point(60, 260);
            this.btn_NewUser.Name = "btn_NewUser";
            this.btn_NewUser.Size = new System.Drawing.Size(140, 30);
            this.btn_NewUser.TabIndex = 6;
            this.btn_NewUser.Text = "Create Account";
            this.toolTip1.SetToolTip(this.btn_NewUser, "Register as a new user");
            this.btn_NewUser.UseVisualStyleBackColor = false;
            this.btn_NewUser.Click += new System.EventHandler(this.btn_NewUser_Click);
            // 
            // btn_Forgot
            // 
            this.btn_Forgot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_Forgot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Forgot.FlatAppearance.BorderSize = 0;
            this.btn_Forgot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Forgot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Forgot.ForeColor = System.Drawing.Color.Black;
            this.btn_Forgot.Location = new System.Drawing.Point(260, 260);
            this.btn_Forgot.Name = "btn_Forgot";
            this.btn_Forgot.Size = new System.Drawing.Size(140, 30);
            this.btn_Forgot.TabIndex = 7;
            this.btn_Forgot.Text = "Forgot Password";
            this.toolTip1.SetToolTip(this.btn_Forgot, "Reset your password");
            this.btn_Forgot.UseVisualStyleBackColor = false;
            this.btn_Forgot.Click += new System.EventHandler(this.btn_Forgot_Click);
            // 
            // FormLogIn
            // 
            this.AcceptButton = this.btn_LogIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Checkers.Properties.Resources.images_ch;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(460, 360);
            this.Controls.Add(this.label_Welcome);
            this.Controls.Add(this.label_User);
            this.Controls.Add(this.textBox_User);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.btn_LogIn);
            this.Controls.Add(this.btn_NewUser);
            this.Controls.Add(this.btn_Forgot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login to Checkers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
