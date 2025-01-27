﻿using MahjongTournamentSuite.TeamsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MahjongTournamentSuite.PlayersSelector
{
    public partial class PlayersSelectorForm : Form, IPlayersSelectorForm
    {
        #region Fields

        private IPlayersSelectorController _controller;
        private int _tournamentId;
        private int _teamId;
        public int ReturnValue { get; set; }
        List<string> allTeamPlayersNames = new List<string>();

        #endregion

        #region Constructor

        public PlayersSelectorForm(int tournamentId, int teamId)
        {
            InitializeComponent();
            _controller = Injector.providePlayersSelectorController(this);
            _tournamentId = tournamentId;
            _teamId = teamId;
        }

        #endregion

        #region Events

        private void PlayersSelectorForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CancelButton = btnCancel;
            AcceptButton = btnOk;
            _controller.LoadForm(_tournamentId, _teamId);
            Cursor = Cursors.Default;
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            var filter = sender.ToString();
            if (filter.Trim().Count() == 0)
            {
                FillLbPlayersNames(allTeamPlayersNames);
            } else
            {
                var filteredPlayers = from player in allTeamPlayersNames
                                      where player.Contains(filter)
                                      select player;
                FillLbPlayersNames(filteredPlayers.ToList());
            }
        }

        private void lbPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseReturningValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbPlayers.SelectedIndex >= 0)
                CloseReturningValue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region IPlayersSelectorForm implementation

        public void FillLbPlayersNames(List<string> dgvAvailableTeamPlayers)
        {
            if (allTeamPlayersNames.Count == 0)
            {
                allTeamPlayersNames = dgvAvailableTeamPlayers;
            }
            lbPlayers.DataSource = dgvAvailableTeamPlayers;
            lbPlayers.SelectedIndex = 0;
        }

        #endregion

        #region Private

        public void CloseReturningValue()
        {
            string selectedItem = (string)lbPlayers.SelectedItem;
            if(selectedItem.Equals(string.Empty))
            {
                ReturnValue = 0;
            }
            else
            {
                ReturnValue = int.Parse(selectedItem.Substring(selectedItem.IndexOf(" - ")).Replace(" - ", string.Empty));
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
