// FormPlayer.Designer.cs (modernized)
namespace Checkers
{
    partial class FormPlayer
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dg_Players;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dg_Players = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // mainLayout
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.mainLayout.RowCount = 2;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(26, 188, 156);
            this.lblTitle.Text = "Player Statistics";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // dg_Players
            this.dg_Players.AllowUserToAddRows = false;
            this.dg_Players.AllowUserToDeleteRows = false;
            this.dg_Players.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_Players.BackgroundColor = System.Drawing.Color.White;
            this.dg_Players.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dg_Players.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dg_Players.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Players.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Players.GridColor = System.Drawing.Color.LightGray;
            this.dg_Players.Location = new System.Drawing.Point(16, 58);
            this.dg_Players.Margin = new System.Windows.Forms.Padding(16, 8, 16, 16);
            this.dg_Players.MultiSelect = false;
            this.dg_Players.Name = "dg_Players";
            this.dg_Players.ReadOnly = true;
            this.dg_Players.RowHeadersVisible = false;
            this.dg_Players.RowTemplate.Height = 28;
            this.dg_Players.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Players.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dg_Players_CellMouseClick);
            // chart1
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(731, 58);
            this.chart1.Margin = new System.Windows.Forms.Padding(8, 8, 16, 16);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chart1.Size = new System.Drawing.Size(462, 513);
            var ca = new System.Windows.Forms.DataVisualization.Charting.ChartArea("Area"); ca.BackColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Clear(); this.chart1.ChartAreas.Add(ca);
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend("Legend"); legend.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            this.chart1.Legends.Clear(); this.chart1.Legends.Add(legend);
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("s1");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            this.chart1.Series.Clear(); this.chart1.Series.Add(series);
            this.chart1.Visible = false;

            // FormPlayer
            this.ClientSize = new System.Drawing.Size(1209, 587);
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormPlayer";
            this.Text = "Player Statistics";

            this.mainLayout.Controls.Add(this.lblTitle, 0, 0);
            this.mainLayout.SetColumnSpan(this.lblTitle, 2);
            this.mainLayout.Controls.Add(this.dg_Players, 0, 1);
            this.mainLayout.Controls.Add(this.chart1, 1, 1);

            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}