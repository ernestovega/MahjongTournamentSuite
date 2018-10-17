using System.Windows.Forms;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;
using System.Drawing;
using MahjongTournamentSuite.PlayersSelector;

namespace MahjongTournamentSuite.TeamsManager
{
    public partial class TeamsManagerForm : Form, ITeamsManagerForm
    {
        #region Fields

        private ITeamsManagerController _controller;
        private int _tournamentId;

        #endregion

        #region Constructor

        public TeamsManagerForm(int tournamentId)
        {
            InitializeComponent();
            _controller = Injector.provideTeamsManagerController(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void TeamsManagerForm_Load(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _controller.LoadForm(_tournamentId);
            Cursor = Cursors.Default;
        }

        private void TeamsManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Para guardar los cambios que no se hayan guardado.
            lblStub.Focus();
        }

        private void dgvTeams_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTeams.CurrentCell != null &&
                dgvTeams.CurrentCell.RowIndex == e.RowIndex &&
                dgvTeams.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgvTeams.CurrentCell.ReadOnly ?
                    ColorConstants.GREEN_MM_DARKEST :
                    ColorConstants.GREEN_MM_DARKER;
            }
        }

        private void dgvTeams_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _controller.LoadTeamPlayers((int)dgvTeams.Rows[e.RowIndex].Cells[VTeam.COLUMN_TEAMS_ID].Value);
                if (dgvTeams.Columns[e.ColumnIndex].Name.Equals(VTeam.COLUMN_TEAMS_NAME))
                    dgvTeams.BeginEdit(true);
            }
        }

        private void dgvTeamPlayers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                ShowPlayersSelector(e.RowIndex);
            }
        }

        private void ShowPlayersSelector(int rowIndex)
        {
            int selectedTeamId = (int)dgvTeams.Rows[rowIndex].Cells[VTeam.COLUMN_TEAMS_ID].Value;
            int selectedTeamPlayerId = (int)dgvTeamPlayers.Rows[rowIndex].Cells[DGVTeamPlayer.COLUMN_TEAMPLAYER_ID].Value;

            //using (var playersSelectorForm = new PlayersSelectorForm(_tournamentId, selectedTeamId, ))
            //{
            //    if (playersSelectorForm.ShowDialog() == DialogResult.OK)
            //    {
            //        if (playersSelectorForm.ReturnValue == 0)
            //            _controller.UnassignTeamPlayer(_tournamentId, selectedTeamPlayerId);
            //        else
            //        {
            //            _controller.AssignTeamPlayer(_tournamentId, playersSelectorForm.ReturnValue, selectedTeamId);
            //        }
            //        //_controller.LoadForm(_tournamentId);
            //    }
            //}
        }

        private void dgvTeams_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1 && dgvTeams.Columns[e.ColumnIndex].Name.Equals(VTeam.COLUMN_TEAMS_NAME))
            {
                Cursor = Cursors.WaitCursor;
                string previousValue = (string)dgvTeams.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (newValue.Length > 0 && !newValue.Equals(previousValue))
                {
                    int teamId = (int)dgvTeams.Rows[e.RowIndex].Cells[VTeam.COLUMN_TEAMS_ID].Value;
                    _controller.TeamNameChanged(teamId, newValue);
                }
                else
                    DGVCancelEdit();
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region ITeamsManagerForm implementation

        public void FillDGVTeams(List<VTeam> teams)
        {
            SortableBindingList<VTeam> sortableTeams = new SortableBindingList<VTeam>(teams);
            if (dgvTeams.DataSource != null)
                dgvTeams.DataSource = null;
            dgvTeams.DataSource = sortableTeams;

            //Visible
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_TOURNAMENT_ID].Visible = false;
            //ReadOnly
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_ID].ReadOnly = true;
            //Readonly columns BackColor
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_ID].HeaderText = "Team Id";
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_NAME].HeaderText = "Team Name";
            //AutoSizeMode
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvTeams.Columns[VTeam.COLUMN_TEAMS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _controller.LoadTeamPlayers((int)dgvTeams.Rows[0].Cells[VTeam.COLUMN_TEAMS_ID].Value);
        }

        public void FillDGVTeamPlayers(List<DGVTeamPlayer> dgvTeamPlayers)
        {
            SortableBindingList<DGVTeamPlayer> sortableTeamPlayersNames = new SortableBindingList<DGVTeamPlayer>(dgvTeamPlayers);
            if (this.dgvTeamPlayers.DataSource != null)
                this.dgvTeamPlayers.DataSource = null;
            this.dgvTeamPlayers.DataSource = sortableTeamPlayersNames;

            //HeaderText
            this.dgvTeamPlayers.Columns[DGVTeamPlayer.COLUMN_TEAMPLAYER_ID].HeaderText = "Player Id";
            this.dgvTeamPlayers.Columns[DGVTeamPlayer.COLUMN_TEAMPLAYER_NAME].HeaderText = "Player Name";
            //AutoSizeMode
            this.dgvTeamPlayers.Columns[DGVTeamPlayer.COLUMN_TEAMPLAYER_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvTeamPlayers.Columns[DGVTeamPlayer.COLUMN_TEAMPLAYER_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void ShowMessageTeamNameInUse(string usedName, int ownerTeamId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the team {1}", usedName, ownerTeamId), "Name in use");
        }

        public void DGVCancelEdit()
        {
            dgvTeams.CancelEdit();
        }

        #endregion
    }
}
