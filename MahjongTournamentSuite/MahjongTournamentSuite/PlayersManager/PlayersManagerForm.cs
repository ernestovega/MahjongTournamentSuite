﻿using System.Windows.Forms;
using System.Collections.Generic;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.PlayersManager;
using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.ViewModel;
using System.Drawing;
using MahjongTournamentSuite.Resources.flags;
using System.Media;
using MahjongTournamentSuite.EmaPlayersSelector;
using System;

namespace MahjongTournamentSuite.ManagePlayers
{
    public partial class PlayersManagerForm : Form, IPlayersManagerForm
    {
        #region Fields

        private IPlayersManagerController _controller;
        private int _tournamentId;
        private int _selectedRowIndex = -1;
        private string _selectedColumnId = ""; 

        #endregion

        #region Constructor

        public PlayersManagerForm(int tournamentId)
        {
            InitializeComponent();
            _controller = Injector.providePlayersManagerController(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void PlayersManagerForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _controller.LoadForm(_tournamentId);
            if (dgv.Columns.Count > 0)
            {
                dgv.Columns[0].ReadOnly = true;
            }
            Cursor = Cursors.Default;
        }

        private void PlayersManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblStub.Focus();//Para guardar los cambios que no se hayan guardado.

            if (_controller.IsWrongPlayersTeams())
                DialogResult = DialogResult.Cancel;
            else
                DialogResult = DialogResult.OK;
        }
        
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgv.CurrentCell.ReadOnly ?
                    ColorConstants.GREEN_MM_DARKEST :
                    ColorConstants.GREEN_MM_DARKER;
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgv.CurrentCell != null && dgv.CurrentRow.Index >= 0)
            {
                if (dgv.CurrentCell.OwningColumn.Name.Equals(VPlayer.COLUMN_PLAYERS_NAME))
                {
                    ShowEmaPlayersSelector(dgv.CurrentRow.Index);
                    e.SuppressKeyPress = true;
                }
                else if (dgv.CurrentCell.OwningColumn.Name.Equals(DGVPlayer.COLUMN_PLAYERS_TEAM_NAME))
                {
                    ShowTeamsSelector(dgv.CurrentRow.Index);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(VPlayer.COLUMN_PLAYERS_NAME))
                    ShowEmaPlayersSelector(e.RowIndex);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(DGVPlayer.COLUMN_PLAYERS_TEAM_NAME))
                    ShowTeamsSelector(e.RowIndex);
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(DGVPlayer.COLUMN_PLAYERS_TEAM_NAME))
            {
                Cursor = Cursors.WaitCursor;
                _controller.CheckWrongPlayersTeams();
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
            dgv.Columns[VPlayer.COLUMN_PLAYERS_TOURNAMENT_ID].Visible = false;
            dgv.Columns[VPlayer.COLUMN_PLAYERS_TEAM].Visible = false;
            dgv.Columns[VPlayer.COLUMN_PLAYERS_COUNTRY_NAME].Visible = false;
            dgv.Columns[VPlayer.COLUMN_PLAYERS_EMA_NUMBER].Visible = false;
            //ReadOnly
            dgv.Columns[VPlayer.COLUMN_PLAYERS_ID].ReadOnly = true;
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].ReadOnly = true;
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].ReadOnly = true;
            //No editable Columns BackColor
            dgv.Columns[VPlayer.COLUMN_PLAYERS_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //No editable Columns ForeColor
            dgv.Columns[VPlayer.COLUMN_PLAYERS_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[VPlayer.COLUMN_PLAYERS_ID].HeaderText = "Player Id";
            dgv.Columns[VPlayer.COLUMN_PLAYERS_NAME].HeaderText = "Player Name";
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].HeaderText = "Player Country";
            //AutoSizeMode
            dgv.Columns[VPlayer.COLUMN_PLAYERS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[DGVPlayer.COLUMN_PLAYERS_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //DisplayIndex
            dgv.Columns[VPlayer.COLUMN_PLAYERS_ID].DisplayIndex = 0;
            dgv.Columns[VPlayer.COLUMN_PLAYERS_NAME].DisplayIndex = 1;
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
            if(_selectedRowIndex >= 0)
            {
                dgv.Rows[_selectedRowIndex].Selected = true;
                if (_selectedColumnId != "")
                {
                    dgv.Rows[_selectedRowIndex].Cells[_selectedColumnId].Selected = true;
                    _selectedColumnId = "";
                }
                _selectedRowIndex = -1;
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
                    int currentRowTeamId = (int)row.Cells[VPlayer.COLUMN_PLAYERS_TEAM].Value;
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

        #region Private

        private void ShowTeamsSelector(int rowIndex)
        {
            using (var teamSelectorForm = new TeamSelectorForm(_tournamentId))
            {
                if (teamSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    int playerId = (int)dgv.Rows[rowIndex].Cells[VPlayer.COLUMN_PLAYERS_ID].Value;
                    int teamId = _controller.SaveNewPlayerTeam(playerId, teamSelectorForm.ReturnValue);
                    if (teamId > 0)
                    {
                        dgv.Rows[rowIndex].Cells[VPlayer.COLUMN_PLAYERS_TEAM].Value = teamId;
                        dgv.Rows[rowIndex].Cells[DGVPlayer.COLUMN_PLAYERS_TEAM_NAME].Value = teamSelectorForm.ReturnValue;
                        _selectedRowIndex = rowIndex;
                        _selectedColumnId = DGVPlayer.COLUMN_PLAYERS_TEAM_NAME;
                    }
                }
            }
        }

        private void ShowEmaPlayersSelector(int rowIndex)
        {
            using (var emaPlayersSelectorForm = new EmaPlayersSelectorForm(_tournamentId))
            {
                if (emaPlayersSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    int playerId = (int)dgv.Rows[rowIndex].Cells[VPlayer.COLUMN_PLAYERS_ID].Value;
                    if (emaPlayersSelectorForm.ReturnValue != null)
                    {
                        if (emaPlayersSelectorForm.ReturnValue.Equals(string.Empty))
                            _controller.UnassignEmaPlayer(playerId);
                        else
                        {
                            string returnedItem = emaPlayersSelectorForm.ReturnValue;
                            string returnedEmaNumber = returnedItem.Substring(returnedItem.IndexOf(" - ")).Replace(" - ", string.Empty);
                            _controller.AssignNewEmaPlayer(playerId, returnedEmaNumber);
                        }
                        _selectedRowIndex = rowIndex;
                        _selectedColumnId = VPlayer.COLUMN_PLAYERS_NAME;
                        _controller.LoadForm(_tournamentId);
                    }
                }
            }
        }

        #endregion
    }
}
