using System.Windows.Forms;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using MahjongTournamentSuite.Model;
using System.Drawing;

namespace MahjongTournamentSuite.TeamsManager
{
    public partial class TeamsManagerForm : Form, ITeamsManagerForm
    {
        #region Fields

        private ITeamsManagerPresenter _presenter;
        private int _tournamentId;

        #endregion

        #region Constructor

        public TeamsManagerForm(int tournamentId)
        {
            InitializeComponent();
            _presenter = Injector.provideTeamsManagerPresenter(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void TeamsManagerForm_Load(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _presenter.LoadForm(_tournamentId);
            Cursor = Cursors.Default;
        }

        private void TeamsManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Para guardar los cambios que no se hayan guardado.
            lblStub.Focus();
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgv.CurrentCell.ReadOnly ?
                    MyConstants.GREEN_MM_DARKEST :
                    MyConstants.GREEN_MM_DARKER;
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(DBTeam.COLUMN_TEAMS_NAME))
                dgv.BeginEdit(true);
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(DBTeam.COLUMN_TEAMS_NAME))
            {
                Cursor = Cursors.WaitCursor;
                string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (newValue.Length > 0 && !newValue.Equals(previousValue))
                {
                    int teamId = (int)dgv.Rows[e.RowIndex].Cells[DBTeam.COLUMN_TEAMS_ID].Value;
                    _presenter.TeamNameChanged(teamId, newValue);
                }
                else
                    DGVCancelEdit();
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region ITeamsManagerForm implementation

        public void FillDGV(List<DBTeam> teams)
        {
            SortableBindingList<DBTeam> sortableTeams = new SortableBindingList<DBTeam>(teams);
            if (dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortableTeams;

            //Visible
            dgv.Columns[DBTeam.COLUMN_TEAMS_TOURNAMENT_ID].Visible = false;
            //ReadOnly
            dgv.Columns[DBTeam.COLUMN_TEAMS_ID].ReadOnly = true;
            //Readonly columns BackColor
            dgv.Columns[DBTeam.COLUMN_TEAMS_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgv.Columns[DBTeam.COLUMN_TEAMS_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[DBTeam.COLUMN_TEAMS_ID].HeaderText = "Team Id";
            dgv.Columns[DBTeam.COLUMN_TEAMS_NAME].HeaderText = "Team Name";
            //AutoSizeMode
            dgv.Columns[DBTeam.COLUMN_TEAMS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[DBTeam.COLUMN_TEAMS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void ShowMessageTeamNameInUse(string usedName, int ownerTeamId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the team {1}", usedName, ownerTeamId), "Name in use");
        }

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        #endregion

        #region Private



        #endregion
    }
}
