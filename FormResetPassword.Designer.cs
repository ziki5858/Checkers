using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    partial class FormResetPassword
    {
        private Label lblNew;
        private TextBox txtNew;
        private Label lblConfirm;
        private TextBox txtConfirm;
        private Button btnOk;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.lblNew = new Label() { Text = "New Password:", AutoSize = true, Left = 10, Top = 20 };
            this.txtNew = new TextBox() { Left = 120, Top = 18, Width = 150, UseSystemPasswordChar = true };
            this.lblConfirm = new Label() { Text = "Confirm Password:", AutoSize = true, Left = 10, Top = 60 };
            this.txtConfirm = new TextBox() { Left = 120, Top = 58, Width = 150, UseSystemPasswordChar = true };
            this.btnOk = new Button() { Text = "OK", Left = 120, Width = 70, Top = 100, DialogResult = DialogResult.OK };
            this.btnCancel = new Button() { Text = "Cancel", Left = 200, Width = 70, Top = 100, DialogResult = DialogResult.Cancel };

            this.btnOk.Click += btnOk_Click;
            this.btnCancel.Click += btnCancel_Click;

            this.ClientSize = new Size(300, 150);
            this.Controls.AddRange(new Control[] { lblNew, txtNew, lblConfirm, txtConfirm, btnOk, btnCancel });
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}