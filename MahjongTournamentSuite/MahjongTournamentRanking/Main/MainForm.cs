using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MahjongTournamentRanking.Model;

namespace MahjongTournamentRanking.Main
{
    public partial class MainForm : Form, IMainForm
    {
        #region Fields

        private IMainPresenter _presenter;

        #endregion

        #region Constructor

        public MainForm(int tournamentId)
        {
            InitializeComponent();
            _presenter = new MainPresenter(this);
            _presenter.LoadRanking(tournamentId);
        }

        #endregion

        #region IMainForm implementation

        public void FillDGVPlayersFromThread(List<PlayerRanking> playersRankingsRange)
        {
            dgv.Invoke(new MethodInvoker(() => { FillDGVPlayers(playersRankingsRange); }));
        }

        public void FillDGVTeamsFromThread(List<TeamRanking> teamsRankingsRange)
        {
            dgv.Invoke(new MethodInvoker(() => { FillDGVTeams(teamsRankingsRange); }));
        }

        #endregion

        #region Private

        private void FillDGVPlayers(List<PlayerRanking> playersRankingsRange)
        {
            dgv.DataSource = playersRankingsRange;
        }

        private void FillDGVTeams(List<TeamRanking> teamsRankingsRange)
        {
            dgv.DataSource = teamsRankingsRange;
        }

        #endregion
    }
}
