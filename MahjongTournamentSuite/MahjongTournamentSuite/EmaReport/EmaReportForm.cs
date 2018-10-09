using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaReport
{
    public partial class EmaReportForm : Form, IEmaReportForm
    {
        private IEmaReportController _controller;
        private List<DGVEmaPlayer> _dgvEmaPlayers;

        public EmaReportForm(List<DGVEmaPlayer> dgvEmaPlayers)
        {
            InitializeComponent();
            _controller = Injector.provideEmaReportController(this);
            _dgvEmaPlayers = dgvEmaPlayers;
        }

        private void EmaReportForm_Load(object sender, System.EventArgs e)
        {
            _controller.LoadForm(_dgvEmaPlayers);
        }

        public void LoadDgv(List<DGVEmaPlayer> dgvEmaPlayers)
        {
            dgv.DataSource = dgvEmaPlayers;

            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].HeaderText = "EMA number";
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_NAME].HeaderText = "Name";
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME].HeaderText = "Last name";
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_NAME].HeaderText = "Country name";
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_TABLE_POINTS].HeaderText = "Table points";
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_SCORE].HeaderText = "Score";

            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_TABLE_POINTS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_EMA_NUMBER].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_LAST_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_COUNTRY_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_TABLE_POINTS].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVEmaPlayer.COLUMN_EMA_PLAYER_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
        }
    }
}
