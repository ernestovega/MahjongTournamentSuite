using System.Windows.Forms;
using System.Collections.Generic;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.PlayersManager;
using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.Model;
using System.Drawing;
using System;
using MahjongTournamentSuite.Resources.flags;
using System.Media;

namespace MahjongTournamentSuite.ManagePlayers
{
    public partial class PlayersManagerForm : Form, IPlayersManagerForm
    {
        #region Fields

        private IPlayersManagerPresenter _presenter;
        private int _tournamentId;

        #endregion

        #region Constructor

        public PlayersManagerForm(int tournamentId)
        {
            InitializeComponent();
            _presenter = Injector.providePlayersManagerPresenter(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void PlayersManagerForm_Load(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _presenter.LoadForm(_tournamentId);
            Cursor = Cursors.Default;
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
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(DBPlayer.COLUMN_PLAYERS_NAME))
                    dgv.BeginEdit(true);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(DGVPlayer.COLUMN_PLAYERS_TEAM_NAME))
                {
                    using (var teamSelectorForm = new TeamSelectorForm(_tournamentId))
                    {
                        if (teamSelectorForm.ShowDialog() == DialogResult.OK)
                        {
                            int playerId = (int)dgv.Rows[e.RowIndex].Cells[DBPlayer.COLUMN_PLAYERS_ID].Value;
                            int teamId = _presenter.SaveNewPlayerTeam(playerId, teamSelectorForm.ReturnValue);
                            if (teamId > 0)
                            {
                                dgv.Rows[e.RowIndex].Cells[DBPlayer.COLUMN_PLAYERS_TEAM].Value = teamId;
                                dgv.Rows[e.RowIndex].Cells[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].Value = teamSelectorForm.ReturnValue;
                            }
                        }
                    }
                }
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG))
                {
                    using (var countrySelectorForm = new CountrySelectorForm())
                    {
                        if (countrySelectorForm.ShowDialog() == DialogResult.OK)
                        {
                            int playerId = (int)dgv.Rows[e.RowIndex].Cells[DBPlayer.COLUMN_PLAYERS_ID].Value;
                            if (countrySelectorForm.ReturnValue != null && !countrySelectorForm.ReturnValue.Equals(string.Empty))
                            {
                                _presenter.SaveNewPlayerCountry(playerId, countrySelectorForm.ReturnValue);
                                dgv.Rows[e.RowIndex].Cells[DBPlayer.COLUMN_PLAYERS_COUNTRY_NAME].Value = countrySelectorForm.ReturnValue;
                                dgv.Rows[e.RowIndex].Cells[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].Value = 
                                    CountryFlags.GetFlagImage(countrySelectorForm.ReturnValue);
                            }
                        }
                    }
                }
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(DBPlayer.COLUMN_PLAYERS_NAME))
                {
                    Cursor = Cursors.WaitCursor;
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        int playerId = (int)dgv.Rows[e.RowIndex].Cells[DBPlayer.COLUMN_PLAYERS_ID].Value;
                        _presenter.PlayerNameChanged(playerId, newValue);
                    }
                    else
                        DGVCancelEdit();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(DGVPlayer.COLUMN_PLAYERS_TEAM_NAME))
            {
                Cursor = Cursors.WaitCursor;
                _presenter.CheckWrongPlayersTeams();
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region IPlayersManagerForm implementation

        public void FillDGV(List<DGVPlayer> players, bool isTeams)
        {
            SortableBindingList<DGVPlayer> sortablePlayers = new SortableBindingList<DGVPlayer>(players);
            if (dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortablePlayers;

            //Visible
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_TOURNAMENT_ID].Visible = false;
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_TEAM].Visible = false;
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_COUNTRY_NAME].Visible = false;
            //ReadOnly
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_ID].ReadOnly = true;
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].ReadOnly = true;
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].ReadOnly = true;
            //No editable Columns BackColor
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //No editable Columns ForeColor
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_ID].HeaderText = "Player Id";
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_NAME].HeaderText = "Player Name";
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].HeaderText = "Player Country";
            //AutoSizeMode
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //DisplayIndex
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_ID].DisplayIndex = 0;
            dgv.Columns[DBPlayer.COLUMN_PLAYERS_NAME].DisplayIndex = 1;
            if (isTeams)
            {
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].ReadOnly = true;
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].HeaderText = "Player Team";
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].DisplayIndex = 2;
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].DisplayIndex = 3;
            }
            else
            {
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].Visible = false;
                dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].DisplayIndex = 2;
            }
        }

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        public void ShowMessagePlayerNameInUse(string usedName, int ownerPlayerId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the player {1}", usedName, ownerPlayerId), "Name in use");
        }

        public void ShowMessageTeamError()
        {
            MessageBox.Show("Something went wrong with the team selection.", "Ups!");
        }

        public void MarkWrongTeamsPlayers(List<WrongTeam> wrongTeams)
        {
            if (wrongTeams.Count > 0)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    int currentRowTeamId = (int)row.Cells[DBPlayer.COLUMN_PLAYERS_TEAM].Value;
                    WrongTeam wrongTeam = wrongTeams.Find(x => x.TeamId == currentRowTeamId);
                    if (wrongTeam != null)
                    {
                        row.Cells[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].ErrorText =
                            string.Format("{0} players in this team", wrongTeam.NumPlayers);
                    }
                }
            }
        }

        public void ShowWrongNumberOfPlayersPerTeamMessage(List<WrongTeam> wrongTeams)
        {
            string message = string.Empty;
            foreach (WrongTeam wrongTeam in wrongTeams)
            {
                if (message != string.Empty)
                    message += "\n";
                message = string.Format("{0}The team \"{1}\" have {2} players.",
                    message, wrongTeam.TeamName, wrongTeam.NumPlayers);
            }
            string caption = "Wrong team number of players";
            if (wrongTeams.Count > 1)
                caption = "Wrong teams number of players";
            MessageBox.Show(message, caption);
        }

        public void CleanWrongTeamsPlayers()
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].ErrorText = string.Empty;
            }
        }

        public void PlayKoSound()
        {
            SystemSounds.Exclamation.Play();
        }

        #endregion

        private void PlayersManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_presenter.IsWrongPlayersTeams())
                DialogResult = DialogResult.Cancel;
            else
                DialogResult = DialogResult.OK;

        }
    }
}
