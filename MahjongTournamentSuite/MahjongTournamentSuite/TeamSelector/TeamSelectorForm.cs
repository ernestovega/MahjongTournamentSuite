﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.TeamSelector
{
    public partial class TeamSelectorForm : Form, ITeamSelectorForm
    {
        #region Fields

        private ITeamSelectorController _controller;
        private int _tournamentId;

        public string ReturnValue { get; set; }

        #endregion

        #region Constructor

        public TeamSelectorForm(int tournamentId)
        {
            InitializeComponent();
            _controller = Injector.provideTeamSelectorController(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void TeamSelectorForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CancelButton = btnCancel;
            AcceptButton = btnOk;
            _controller.LoadForm(_tournamentId);
            Cursor = Cursors.Default;
        }

        private void lbTeams_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseReturningValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbTeams.SelectedIndex >= 0)
                CloseReturningValue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region ITeamSelectorForm implementation

        public void FillLbTeams(List<string> teams)
        {
            lbTeams.DataSource = teams;

            int heightIncrement = (teams.Count * lbTeams.ItemHeight) - lbTeams.Height;
            if (Height + heightIncrement > 600)
                Height = 600;
            else
                Height += heightIncrement;

            lbTeams.SelectedIndex = 0;
        }

        #endregion

        #region Private

        private void CloseReturningValue()
        {
            ReturnValue = (string)lbTeams.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
