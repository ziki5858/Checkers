using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Checkers.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace Checkers
{
    public partial class FormPlayer : Form
    {
        // ----- Dependencies -----
        private readonly SqlConnection _connection;
        private readonly SqlGameResultRepository _repository;

        // ----- UI Components -----
        // chart1 and dg_Players defined in Designer

        /// <summary>
        /// Initializes the Player form and configures the chart.
        /// </summary>
        public FormPlayer(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            _repository = new SqlGameResultRepository(_connection);

            ConfigureChart();

            try
            {
                LoadPlayersIntoGrid();  // optional, loads usernames
                LoadPlayerStats();      // binds stats to grid
            }
            catch (Exception ex)
            {
                GeneralHelper.ShowErrorConfirm("Error loading players:\n" + ex.Message);
            }
        }

        #region Chart Configuration

        /// <summary>
        /// Sets up the pie chart style and hides it until needed.
        /// </summary>
        private void ConfigureChart()
        {
            var series = chart1.Series["s1"];
            series.ChartType = SeriesChartType.Pie;
            series.Label = "#VALX: #PERCENT{P0}";       // e.g. "Wins: 60%"
            series.IsValueShownAsLabel = true;
            series.LegendText = "#VALX";               // legend shows "Wins"/"Losses"
            series["PieLabelStyle"] = "Outside";      // labels outside slices
            chart1.Visible = false;                     // hidden until a player is selected
        }

        #endregion

        #region Data Loading

        /// <summary>
        /// Loads all player usernames into the DataGridView.
        /// </summary>
        private void LoadPlayersIntoGrid()
        {
            DataTable users = _repository.GetAllUsernames();
            // if you want only usernames, bind to a separate control; here it's optional
        }

        /// <summary>
        /// Loads full player stats ordered by score into the grid.
        /// </summary>
        private void LoadPlayerStats()
        {
            DataTable stats = _repository.GetAllPlayerStats();
            dg_Players.DataSource = stats;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Displays the win/loss pie chart for the selected player.
        /// </summary>
        private void dg_Players_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // show chart and clear previous data
            chart1.Visible = true;
            var series = chart1.Series["s1"];
            series.Points.Clear();

            // retrieve stats for selected player
            string player = dg_Players.CurrentRow.Cells[0].Value.ToString();
            DataTable table = _repository.GetPlayerStats(player);
            if (table.Rows.Count == 0) return;

            int wins = Convert.ToInt32(table.Rows[0]["winNumber"]);
            int losses = Convert.ToInt32(table.Rows[0]["loseNumber"]);
            int total = wins + losses;
            if (total == 0) return;

            double winPct = wins * 100.0 / total;
            double lossPct = 100.0 - winPct;

            // add slices with legend text
            series.Points.AddXY("Wins", Math.Round(winPct));
            series.Points.AddXY("Losses", Math.Round(lossPct));
            series.Points[0].LegendText = "Wins";
            series.Points[1].LegendText = "Losses";
        }

        #endregion
    }
}