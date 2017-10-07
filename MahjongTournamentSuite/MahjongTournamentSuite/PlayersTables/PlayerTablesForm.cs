using MahjongTournamentSuite.ViewModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.PlayersTables
{
    public partial class PlayerTablesForm : Form
    {
        private PlayerTables _playerTables;

        public PlayerTablesForm(PlayerTables playerTables)
        {
            InitializeComponent();
            _playerTables = playerTables;
            CancelButton = btnClose;
            AcceptButton = btnClose;
        }

        private void PlayerTablesForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = string.Format("Tables for player {0} - {1}", _playerTables.PlayerId, _playerTables.PlayerName);
            dgv.DataSource = _playerTables.DgvPlayerTables;

            dgv.Columns[DGVPlayerTable.COLUMN_ROUNDID].HeaderText = "Round";
            dgv.Columns[DGVPlayerTable.COLUMN_TABLEID].HeaderText = "Table";
            dgv.Columns[DGVPlayerTable.COLUMN_ROUNDID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVPlayerTable.COLUMN_TABLEID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVPlayerTable.COLUMN_ROUNDID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVPlayerTable.COLUMN_TABLEID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
