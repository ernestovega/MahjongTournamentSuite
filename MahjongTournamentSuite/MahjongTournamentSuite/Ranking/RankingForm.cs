using System.Collections.Generic;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.Ranking
{
    public partial class RankingForm : Form, IRankingForm
    {
        #region Constants

        private const string COLUMN_PLAYER_RANKING_PLAYER_ID = "PlayerId";
        private const string COLUMN_PLAYER_RANKING_PLAYER_NAME = "PlayerName";
        private const string COLUMN_PLAYER_RANKING_PLAYER_POINTS = "PlayerPoints";
        private const string COLUMN_PLAYER_RANKING_PLAYER_SCORE = "PlayerScore";
        private const string COLUMN_PLAYER_RANKING_TEAM_ID = "TeamId";
        private const string COLUMN_PLAYER_RANKING_TEAM_NAME = "TeamName";
        private const string COLUMN_PLAYER_RANKING_COUNTRY_ID = "CountryId";
        private const string COLUMN_PLAYER_RANKING_COUNTRY_NAME = "CountryName";

        private const string COLUMN_TEAM_RANKING_TEAM_ID = "TeamId";
        private const string COLUMN_TEAM_RANKING_TEAM_NAME = "TeamName";
        private const string COLUMN_TEAM_RANKING_TEAM_POINTS = "TeamPoints";
        private const string COLUMN_TEAM_RANKING_TEAM_SCORE = "TeamScore";

        #endregion

        #region Fields

        private IRankingPresenter _presenter;

        #endregion

        #region Constructor

        public RankingForm(int tournamentId)
        {
            InitializeComponent();
            _presenter = new RankingPresenter(this);
            _presenter.LoadDataAndStartShowRankingThread(tournamentId);
        }

        #endregion

        #region Events

        private void RankingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _presenter.StopShowRankingThread();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
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

            //Visible
            dgv.Columns[COLUMN_PLAYER_RANKING_TEAM_ID].Visible = false;
            dgv.Columns[COLUMN_PLAYER_RANKING_COUNTRY_ID].Visible = false;
            //HeaderText
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_ID].HeaderText = "#";
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_NAME].HeaderText = "NAME";
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_POINTS].HeaderText = "POINTS";
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_SCORE].HeaderText = "SCORE";
            dgv.Columns[COLUMN_PLAYER_RANKING_TEAM_NAME].HeaderText = "TEAM";
            dgv.Columns[COLUMN_PLAYER_RANKING_COUNTRY_NAME].HeaderText = "COUNTRY";
            //AutoSizeMode
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_POINTS].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_RANKING_TEAM_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_PLAYER_RANKING_COUNTRY_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //DisplayIndex
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_ID].DisplayIndex = 0;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_NAME].DisplayIndex = 1;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_POINTS].DisplayIndex = 2;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_SCORE].DisplayIndex = 3;
            dgv.Columns[COLUMN_PLAYER_RANKING_TEAM_NAME].DisplayIndex = 4;
            dgv.Columns[COLUMN_PLAYER_RANKING_COUNTRY_NAME].DisplayIndex = 5;
        }

        private void FillDGVTeams(List<TeamRanking> teamsRankingsRange)
        {
            dgv.DataSource = teamsRankingsRange;
            
            //HeaderText
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_ID].HeaderText = "#";
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_NAME].HeaderText = "NAME";
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_POINTS].HeaderText = "POINTS";
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_SCORE].HeaderText = "SCORE";
            ////AutoSizeMode
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //DisplayIndex
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_ID].DisplayIndex = 0;
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_NAME].DisplayIndex = 1;
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_POINTS].DisplayIndex = 2;
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_SCORE].DisplayIndex = 3;
        }

        #endregion
    }
}
