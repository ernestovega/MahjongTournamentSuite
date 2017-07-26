using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.OldTournaments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    public partial class HomeForm : Form, IHomeForm
    {
        #region Fields

        private IHomePresenter _presenter;

        #endregion

        #region Constructor

        public HomeForm()
        {
            InitializeComponent();
            _presenter = new HomePresenter(this);
        }

        #endregion

        #region Events

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

        public void btnTimer_Click(object sender, EventArgs e)
        {
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            new NewTournamentForm().Show();
        }

        public void btnResume_Click(object sender, EventArgs e)
        {
            new OldTournamentsForm().Show();
        }

        #endregion

        #region IHomeForm implementation

        #endregion
    }
}
