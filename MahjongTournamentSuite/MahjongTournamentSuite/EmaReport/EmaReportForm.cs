using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaReport
{
    public partial class EmaReportForm : Form, IEmaReportForm
    {
        private IEmaReportPresenter _presenter;
        private List<DGVPlayerEma> _dgvEmaPlayers;

        public EmaReportForm(List<DGVPlayerEma> dgvEmaPlayers)
        {
            InitializeComponent();
            _presenter = Injector.provideEmaReportPresenter(this);
            _dgvEmaPlayers = dgvEmaPlayers;
        }

        private void EmaReportForm_Load(object sender, System.EventArgs e)
        {
            _presenter.LoadForm(_dgvEmaPlayers);
        }

        public void LoadDgv(List<DGVPlayerEma> dgvEmaPlayers)
        {
            dgv.DataSource = dgvEmaPlayers;

            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_NAME].HeaderText = "Name";
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_LAST_NAME].HeaderText = "Last name";
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_EMA_NUMBER].HeaderText = "EMA number";
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_COUNTRY_NAME].HeaderText = "Country name";
            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_TABLE_POINTS].HeaderText = "Table points";
            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_SCORE].HeaderText = "Score";

            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_LAST_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_EMA_NUMBER].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_COUNTRY_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_TABLE_POINTS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_LAST_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_EMA_NUMBER].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_COUNTRY_NAME].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_TABLE_POINTS].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVPlayerEma.COLUMN_PLAYER_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
        }
    }
}
