namespace Checkers
{
    partial class FormStart
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btn_Game;
        private System.Windows.Forms.Button btn_StasticP;
        private System.Windows.Forms.Button btn_StaticsGP;
        private System.Windows.Forms.ToolTip toolTip1;

        /// <summary>
        /// Designer-generated code for layout and enhanced styling.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Game = new System.Windows.Forms.Button();
            this.btn_StasticP = new System.Windows.Forms.Button();
            this.btn_StaticsGP = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();

            // FormStart
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(560, 460);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Checkers";

            // btn_Game
            this.btn_Game.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_Game.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateBlue;
            this.btn_Game.FlatAppearance.BorderSize = 2;
            this.btn_Game.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Game.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Game.ForeColor = System.Drawing.Color.White;
            this.btn_Game.Location = new System.Drawing.Point(170, 60);
            this.btn_Game.Name = "btn_Game";
            this.btn_Game.Size = new System.Drawing.Size(220, 110);
            this.btn_Game.TabIndex = 0;
            this.btn_Game.Text = "New Game";
            this.toolTip1.SetToolTip(this.btn_Game, "Start a brand new match");
            this.btn_Game.UseVisualStyleBackColor = false;
            this.btn_Game.Click += new System.EventHandler(this.btn_Game_Click);

            // btn_StasticP
            this.btn_StasticP.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_StasticP.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btn_StasticP.FlatAppearance.BorderSize = 2;
            this.btn_StasticP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StasticP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btn_StasticP.ForeColor = System.Drawing.Color.White;
            this.btn_StasticP.Location = new System.Drawing.Point(170, 200);
            this.btn_StasticP.Name = "btn_StasticP";
            this.btn_StasticP.Size = new System.Drawing.Size(220, 50);
            this.btn_StasticP.TabIndex = 1;
            this.btn_StasticP.Text = "Player Statistics";
            this.toolTip1.SetToolTip(this.btn_StasticP, "View each player's win/loss record");
            this.btn_StasticP.UseVisualStyleBackColor = false;
            this.btn_StasticP.Click += new System.EventHandler(this.BtnStatics_Click);

            // btn_StaticsGP
            this.btn_StaticsGP.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_StaticsGP.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btn_StaticsGP.FlatAppearance.BorderSize = 2;
            this.btn_StaticsGP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StaticsGP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btn_StaticsGP.ForeColor = System.Drawing.Color.White;
            this.btn_StaticsGP.Location = new System.Drawing.Point(170, 270);
            this.btn_StaticsGP.Name = "btn_StaticsGP";
            this.btn_StaticsGP.Size = new System.Drawing.Size(220, 50);
            this.btn_StaticsGP.TabIndex = 2;
            this.btn_StaticsGP.Text = "Games Played Stats";
            this.toolTip1.SetToolTip(this.btn_StaticsGP, "See overall match history");
            this.btn_StaticsGP.UseVisualStyleBackColor = false;
            this.btn_StaticsGP.Click += new System.EventHandler(this.btn_StaticsGP_Click);

            // Finalize layout
            this.Controls.Add(this.btn_Game);
            this.Controls.Add(this.btn_StasticP);
            this.Controls.Add(this.btn_StaticsGP);
            this.ResumeLayout(false);
        }
    }
}
