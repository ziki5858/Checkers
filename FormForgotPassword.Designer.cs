// FormForgotPassword.Designer.cs (modernized)
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    partial class FormForgotPassword
    {
        private IContainer components = null;
        private TableLayoutPanel tableLayoutPanel;
        private ToolTip toolTip;
        private ComboBox cmbUsers;
        private Label lblSelectUser;
        private DataGridView dgvQuestion;
        private Label lblAnswer;
        private TextBox textBoxAnswer;
        private Button btnCheckAnswer;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.btnCheckAnswer = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectUser = new System.Windows.Forms.Label();
            this.dgvQuestion = new System.Windows.Forms.DataGridView();
            this.lblAnswer = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestion)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUsers
            // 
            this.cmbUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.Location = new System.Drawing.Point(119, 13);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(388, 31);
            this.cmbUsers.TabIndex = 1;
            this.toolTip.SetToolTip(this.cmbUsers, "Select your username");
            this.cmbUsers.SelectedIndexChanged += new System.EventHandler(this.cmbUsers_SelectedIndexChanged);
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAnswer.Location = new System.Drawing.Point(119, 253);
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(388, 30);
            this.textBoxAnswer.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxAnswer, "Enter the answer to your security question");
            // 
            // btnCheckAnswer
            // 
            this.btnCheckAnswer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCheckAnswer.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnCheckAnswer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckAnswer.Location = new System.Drawing.Point(387, 290);
            this.btnCheckAnswer.Name = "btnCheckAnswer";
            this.btnCheckAnswer.Size = new System.Drawing.Size(120, 35);
            this.btnCheckAnswer.TabIndex = 5;
            this.btnCheckAnswer.Text = "Verify and Reset";
            this.toolTip.SetToolTip(this.btnCheckAnswer, "Click to verify your answer");
            this.btnCheckAnswer.UseVisualStyleBackColor = false;
            this.btnCheckAnswer.Click += new System.EventHandler(this.btnCheckAnswer_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lblSelectUser, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.cmbUsers, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.dgvQuestion, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblAnswer, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.textBoxAnswer, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.btnCheckAnswer, 1, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(520, 340);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectUser.Location = new System.Drawing.Point(13, 10);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(100, 30);
            this.lblSelectUser.TabIndex = 0;
            this.lblSelectUser.Text = "Select User:";
            this.lblSelectUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvQuestion
            // 
            this.dgvQuestion.AllowUserToAddRows = false;
            this.dgvQuestion.AllowUserToDeleteRows = false;
            this.dgvQuestion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuestion.ColumnHeadersHeight = 29;
            this.dgvQuestion.ColumnHeadersVisible = false;
            this.tableLayoutPanel.SetColumnSpan(this.dgvQuestion, 2);
            this.dgvQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuestion.Location = new System.Drawing.Point(13, 43);
            this.dgvQuestion.Name = "dgvQuestion";
            this.dgvQuestion.ReadOnly = true;
            this.dgvQuestion.RowHeadersVisible = false;
            this.dgvQuestion.RowHeadersWidth = 51;
            this.dgvQuestion.Size = new System.Drawing.Size(494, 204);
            this.dgvQuestion.TabIndex = 2;
            // 
            // lblAnswer
            // 
            this.lblAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnswer.Location = new System.Drawing.Point(13, 250);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(100, 35);
            this.lblAnswer.TabIndex = 3;
            this.lblAnswer.Text = "Answer:";
            this.lblAnswer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormForgotPassword
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(520, 340);
            this.Controls.Add(this.tableLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Forgot Password";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestion)).EndInit();
            this.ResumeLayout(false);

        }
    }
}