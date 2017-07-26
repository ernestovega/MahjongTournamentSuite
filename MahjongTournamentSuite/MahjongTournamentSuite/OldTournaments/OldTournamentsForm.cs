using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.OldTournaments
{
    public partial class OldTournamentsForm : Form, IOldTournamentsForm
    {
        #region Fields

        private IOldTournamentsPresenter _presenter;

        #endregion

        #region Constructor

        public OldTournamentsForm()
        {
            InitializeComponent();
            _presenter = new OldTournamentsPresenter(this);
        }

        #endregion

        #region Events

        private void OldTournamentsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.Tournaments' table. You can move, or remove it, as needed.
            this.tournamentsTableAdapter.Fill(this._MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.Tournaments);

        }

        private void dataGridTournaments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridTournaments.Rows[e.RowIndex];
            int tournamentId = (int)row.Cells[0].Value;
            string tournamentName = (string)row.Cells[e.ColumnIndex].Value;
            _presenter.UpdateName(tournamentId, tournamentName);
        }

        private void dataGridTournaments_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
        }

        #endregion

        #region IOldTournamentsForm implementation

        #endregion
    }
}
