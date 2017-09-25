using System.Windows.Forms;

namespace MahjongTournamentSuite.EmaReport
{
    public partial class EmaReportForm : Form, IEmaReportForm
    {
        private int _tournamentId;
        private IEmaReportPresenter _presenter;

        public EmaReportForm(int tournamentId)
        {
            InitializeComponent();
            _tournamentId = tournamentId;
            _presenter = Injector.provideEmaReportPresenter(this);
        }

        private void EmaReportForm_Load(object sender, System.EventArgs e)
        {
            _presenter.LoadForm(_tournamentId);
            //dgv.Columns[DGVPlayerTable.COLUMN_ROUNDID].HeaderText = "Round";
            //dgv.Columns[DGVPlayerTable.COLUMN_TABLEID].HeaderText = "Table";
            //dgv.Columns[DGVPlayerTable.COLUMN_ROUNDID].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgv.Columns[DGVPlayerTable.COLUMN_TABLEID].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgv.Columns[DGVPlayerTable.COLUMN_ROUNDID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //dgv.Columns[DGVPlayerTable.COLUMN_TABLEID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
        }
    }
}
