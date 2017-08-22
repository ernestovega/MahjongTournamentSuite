using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.TeamSelector
{
    public partial class TeamSelectorForm : Form, ITeamSelectorForm
    {
        #region Fields

        private ITeamSelectorPresenter _presenter;
        public string ReturnValue { get; set; }

        #endregion

        #region Constructor

        public TeamSelectorForm(int tournamentId)
        {
            InitializeComponent();
            _presenter = Injector.provideTeamSelectorPresenter(this);
            _presenter.LoadTeams(tournamentId);
            CancelButton = btnCancel;
            AcceptButton = btnOk;
        }

        #endregion

        #region Events

        private void lbTeams_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseReturningValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbTeams.SelectedIndex > 0)
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
