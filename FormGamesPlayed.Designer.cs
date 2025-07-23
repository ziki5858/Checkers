
namespace Checkers
{
    partial class FormGamesPlayed
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dataGridView_GamesPlayed;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button btnToggleView;
        private System.Windows.Forms.Button btnDeleteGame;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dataGridView_GamesPlayed = new System.Windows.Forms.DataGridView();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDeleteGame = new System.Windows.Forms.Button();
            this.btnToggleView = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GamesPlayed)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Controls.Add(this.lblHeader, 0, 0);
            this.mainLayout.Controls.Add(this.dataGridView_GamesPlayed, 0, 1);
            this.mainLayout.Controls.Add(this.buttonPanel, 0, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainLayout.Size = new System.Drawing.Size(900, 600);
            this.mainLayout.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(894, 50);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Games Played";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView_GamesPlayed
            // 
            this.dataGridView_GamesPlayed.AllowUserToAddRows = false;
            this.dataGridView_GamesPlayed.AllowUserToDeleteRows = false;
            this.dataGridView_GamesPlayed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_GamesPlayed.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_GamesPlayed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_GamesPlayed.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView_GamesPlayed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_GamesPlayed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_GamesPlayed.EnableHeadersVisualStyles = false;
            this.dataGridView_GamesPlayed.GridColor = System.Drawing.Color.LightGray;
            this.dataGridView_GamesPlayed.Location = new System.Drawing.Point(3, 53);
            this.dataGridView_GamesPlayed.Name = "dataGridView_GamesPlayed";
            this.dataGridView_GamesPlayed.ReadOnly = true;
            this.dataGridView_GamesPlayed.RowHeadersVisible = false;
            this.dataGridView_GamesPlayed.RowHeadersWidth = 51;
            this.dataGridView_GamesPlayed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_GamesPlayed.Size = new System.Drawing.Size(894, 504);
            this.dataGridView_GamesPlayed.TabIndex = 1;
            this.dataGridView_GamesPlayed.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_GamesPlayed_CellMouseClick);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.btnDeleteGame);
            this.buttonPanel.Controls.Add(this.btnToggleView);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(3, 563);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.buttonPanel.Size = new System.Drawing.Size(894, 34);
            this.buttonPanel.TabIndex = 2;
            this.buttonPanel.WrapContents = false;
            // 
            // btnDeleteGame
            // 
            this.btnDeleteGame.AutoSize = true;
            this.btnDeleteGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteGame.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDeleteGame.ForeColor = System.Drawing.Color.White;
            this.btnDeleteGame.Location = new System.Drawing.Point(722, 8);
            this.btnDeleteGame.Name = "btnDeleteGame";
            this.btnDeleteGame.Size = new System.Drawing.Size(149, 35);
            this.btnDeleteGame.TabIndex = 0;
            this.btnDeleteGame.Text = "🗑 Delete Game";
            this.btnDeleteGame.UseVisualStyleBackColor = false;
            this.btnDeleteGame.Click += new System.EventHandler(this.btn_DeleteGame_click);
            // 
            // btnToggleView
            // 
            this.btnToggleView.AutoSize = true;
            this.btnToggleView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnToggleView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleView.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleView.ForeColor = System.Drawing.Color.White;
            this.btnToggleView.Location = new System.Drawing.Point(541, 8);
            this.btnToggleView.Name = "btnToggleView";
            this.btnToggleView.Size = new System.Drawing.Size(175, 35);
            this.btnToggleView.TabIndex = 1;
            this.btnToggleView.Text = "🔄 Toggle All/Detail";
            this.btnToggleView.UseVisualStyleBackColor = false;
            this.btnToggleView.Click += new System.EventHandler(this.btnToggleView_Click);
            // 
            // FormGamesPlayed
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "FormGamesPlayed";
            this.Text = "Games Played";
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_GamesPlayed)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
