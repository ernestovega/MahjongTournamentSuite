using MahjongTournamentSuite.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaReport
{
    public partial class EmaReportForm : Form, IEmaReportForm
    {
        private IEmaReportController _controller;
        private List<DGVEmaReportPlayer> _dgvEmaReportEmaPlayers;

        public EmaReportForm(List<DGVEmaReportPlayer> dgvEmaReportEmaPlayers)
        {
            InitializeComponent();
            _controller = Injector.provideEmaReportController(this);
            _dgvEmaReportEmaPlayers = dgvEmaReportEmaPlayers;
        }

        private void EmaReportForm_Load(object sender, System.EventArgs e)
        {
            _controller.LoadForm(_dgvEmaReportEmaPlayers);
        }

        public void LoadDgv(List<DGVEmaReportPlayer> dgvEmaReportPlayers)
        {
            dgv.DataSource = dgvEmaReportPlayers;

            //Visible
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_PLAYER_COUNTRY_NAME].Visible = false;
            //HeaderText
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_REPORT_PLAYER_PLACE].HeaderText = "Place";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_PLAYER_NAME].HeaderText = "First Name";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_PLAYER_LAST_NAME].HeaderText = "Last name";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].HeaderText = "EMA number";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_REPORT_PLAYER_TABLE_POINTS].HeaderText = "Table points";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_REPORT_PLAYER_SCORE].HeaderText = "Score";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_REPORT_PLAYER_COUNTRY_EMA_MEMBER].HeaderText = "Ema Member";
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_REPORT_PLAYER_COUNTRY_SHORT_NAME].HeaderText = "Country";
            //Visible
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_PLAYER_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[DGVEmaReportPlayer.COLUMN_EMA_PLAYER_LAST_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
