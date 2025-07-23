namespace Checkers
{
    partial class FormGame
    {
        // --------------------------------------------------------------------
        // designer fields
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Button btnTwoPlayers;
        private System.Windows.Forms.Button btnSinglePlayer;
        private System.Windows.Forms.Button btnColorPicker;
        private System.Windows.Forms.Button btnRules;
        private System.Windows.Forms.Label lblBoardSize;
        private System.Windows.Forms.Label lblPlayerColor;
        private System.Windows.Forms.RadioButton radioSize6;
        private System.Windows.Forms.RadioButton radioSize8;
        private System.Windows.Forms.RadioButton radioSize10;
        private System.Windows.Forms.Label lblCurrentTurn;
        private System.Windows.Forms.Label lblPlayerOneScore;
        private System.Windows.Forms.Label lblPlayerTwoScore;
        private System.Windows.Forms.Timer turnTimer;

        // --------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        // --------------------------------------------------------------------
        /// <summary>Initialize form controls (auto-generated + facelift).</summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.boardPanel = new System.Windows.Forms.Panel();
            this.btnTwoPlayers = new System.Windows.Forms.Button();
            this.btnSinglePlayer = new System.Windows.Forms.Button();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.lblBoardSize = new System.Windows.Forms.Label();
            this.lblPlayerColor = new System.Windows.Forms.Label();
            this.lblCurrentTurn = new System.Windows.Forms.Label();
            this.lblPlayerOneScore = new System.Windows.Forms.Label();
            this.lblPlayerTwoScore = new System.Windows.Forms.Label();
            this.radioSize6 = new System.Windows.Forms.RadioButton();
            this.radioSize8 = new System.Windows.Forms.RadioButton();
            this.radioSize10 = new System.Windows.Forms.RadioButton();
            this.turnTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boardPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.boardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardPanel.Location = new System.Drawing.Point(16, 15);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(1228, 751);
            this.boardPanel.TabIndex = 0;
            // 
            // btnTwoPlayers
            // 
            this.btnTwoPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTwoPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnTwoPlayers.FlatAppearance.BorderSize = 0;
            this.btnTwoPlayers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnTwoPlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTwoPlayers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTwoPlayers.ForeColor = System.Drawing.Color.White;
            this.btnTwoPlayers.Location = new System.Drawing.Point(1252, 25);
            this.btnTwoPlayers.Name = "btnTwoPlayers";
            this.btnTwoPlayers.Size = new System.Drawing.Size(180, 45);
            this.btnTwoPlayers.TabIndex = 1;
            this.btnTwoPlayers.Text = "👥  Two Players";
            this.btnTwoPlayers.UseVisualStyleBackColor = false;
            this.btnTwoPlayers.Click += new System.EventHandler(this.btn_twoPlayers);
            // 
            // btnSinglePlayer
            // 
            this.btnSinglePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSinglePlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSinglePlayer.FlatAppearance.BorderSize = 0;
            this.btnSinglePlayer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btnSinglePlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSinglePlayer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSinglePlayer.ForeColor = System.Drawing.Color.White;
            this.btnSinglePlayer.Location = new System.Drawing.Point(1252, 80);
            this.btnSinglePlayer.Name = "btnSinglePlayer";
            this.btnSinglePlayer.Size = new System.Drawing.Size(180, 45);
            this.btnSinglePlayer.TabIndex = 2;
            this.btnSinglePlayer.Text = "🤖  Single Player";
            this.btnSinglePlayer.UseVisualStyleBackColor = false;
            this.btnSinglePlayer.Click += new System.EventHandler(this.btn_againstCom);
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorPicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnColorPicker.FlatAppearance.BorderSize = 0;
            this.btnColorPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorPicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnColorPicker.Location = new System.Drawing.Point(1260, 642);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(155, 41);
            this.btnColorPicker.TabIndex = 3;
            this.btnColorPicker.UseVisualStyleBackColor = false;
            this.btnColorPicker.Visible = false;
            this.btnColorPicker.Click += new System.EventHandler(this.btn_changeColor);
            // 
            // btnRules
            // 
            this.btnRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnRules.FlatAppearance.BorderSize = 0;
            this.btnRules.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRules.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRules.ForeColor = System.Drawing.Color.Black;
            this.btnRules.Location = new System.Drawing.Point(1260, 689);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(155, 45);
            this.btnRules.TabIndex = 4;
            this.btnRules.Text = "📜  Game Rules";
            this.btnRules.UseVisualStyleBackColor = false;
            this.btnRules.Click += new System.EventHandler(this.btn_gameRoles);
            // 
            // lblBoardSize
            // 
            this.lblBoardSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBoardSize.AutoSize = true;
            this.lblBoardSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBoardSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.lblBoardSize.Location = new System.Drawing.Point(1256, 201);
            this.lblBoardSize.Name = "lblBoardSize";
            this.lblBoardSize.Size = new System.Drawing.Size(94, 23);
            this.lblBoardSize.TabIndex = 5;
            this.lblBoardSize.Text = "Board Size:";
            this.lblBoardSize.Visible = false;
            // 
            // lblPlayerColor
            // 
            this.lblPlayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerColor.AutoSize = true;
            this.lblPlayerColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPlayerColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.lblPlayerColor.Location = new System.Drawing.Point(1268, 616);
            this.lblPlayerColor.Name = "lblPlayerColor";
            this.lblPlayerColor.Size = new System.Drawing.Size(130, 23);
            this.lblPlayerColor.TabIndex = 6;
            this.lblPlayerColor.Text = "Highlight Color:";
            this.lblPlayerColor.Visible = false;
            // 
            // lblCurrentTurn
            // 
            this.lblCurrentTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentTurn.AutoSize = true;
            this.lblCurrentTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentTurn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentTurn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.lblCurrentTurn.Location = new System.Drawing.Point(1249, 555);
            this.lblCurrentTurn.Name = "lblCurrentTurn";
            this.lblCurrentTurn.Size = new System.Drawing.Size(45, 20);
            this.lblCurrentTurn.TabIndex = 10;
            this.lblCurrentTurn.Text = "Turn:";
            this.lblCurrentTurn.Visible = false;
            // 
            // lblPlayerOneScore
            // 
            this.lblPlayerOneScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerOneScore.AutoSize = true;
            this.lblPlayerOneScore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPlayerOneScore.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPlayerOneScore.Location = new System.Drawing.Point(1268, 335);
            this.lblPlayerOneScore.Name = "lblPlayerOneScore";
            this.lblPlayerOneScore.Size = new System.Drawing.Size(0, 23);
            this.lblPlayerOneScore.TabIndex = 11;
            // 
            // lblPlayerTwoScore
            // 
            this.lblPlayerTwoScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerTwoScore.AutoSize = true;
            this.lblPlayerTwoScore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPlayerTwoScore.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPlayerTwoScore.Location = new System.Drawing.Point(1268, 465);
            this.lblPlayerTwoScore.Name = "lblPlayerTwoScore";
            this.lblPlayerTwoScore.Size = new System.Drawing.Size(0, 23);
            this.lblPlayerTwoScore.TabIndex = 12;
            // 
            // radioSize6
            // 
            this.radioSize6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioSize6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radioSize6.ForeColor = System.Drawing.Color.Black;
            this.radioSize6.Location = new System.Drawing.Point(1260, 227);
            this.radioSize6.Name = "radioSize6";
            this.radioSize6.Size = new System.Drawing.Size(80, 30);
            this.radioSize6.TabIndex = 7;
            this.radioSize6.Text = "6";
            this.radioSize6.Visible = false;
            this.radioSize6.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioSize8
            // 
            this.radioSize8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioSize8.Checked = true;
            this.radioSize8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radioSize8.ForeColor = System.Drawing.Color.Black;
            this.radioSize8.Location = new System.Drawing.Point(1260, 257);
            this.radioSize8.Name = "radioSize8";
            this.radioSize8.Size = new System.Drawing.Size(80, 30);
            this.radioSize8.TabIndex = 8;
            this.radioSize8.TabStop = true;
            this.radioSize8.Text = "8";
            this.radioSize8.Visible = false;
            this.radioSize8.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioSize10
            // 
            this.radioSize10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioSize10.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radioSize10.ForeColor = System.Drawing.Color.Black;
            this.radioSize10.Location = new System.Drawing.Point(1260, 287);
            this.radioSize10.Name = "radioSize10";
            this.radioSize10.Size = new System.Drawing.Size(80, 30);
            this.radioSize10.TabIndex = 9;
            this.radioSize10.Text = "10";
            this.radioSize10.Visible = false;
            this.radioSize10.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // turnTimer
            // 
            this.turnTimer.Interval = 1000;
            this.turnTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1449, 791);
            this.Controls.Add(this.boardPanel);
            this.Controls.Add(this.btnTwoPlayers);
            this.Controls.Add(this.btnSinglePlayer);
            this.Controls.Add(this.btnColorPicker);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.lblBoardSize);
            this.Controls.Add(this.lblPlayerColor);
            this.Controls.Add(this.radioSize6);
            this.Controls.Add(this.radioSize8);
            this.Controls.Add(this.radioSize10);
            this.Controls.Add(this.lblCurrentTurn);
            this.Controls.Add(this.lblPlayerOneScore);
            this.Controls.Add(this.lblPlayerTwoScore);
            this.MinimumSize = new System.Drawing.Size(1373, 838);
            this.Name = "FormGame";
            this.Text = "Checkers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
