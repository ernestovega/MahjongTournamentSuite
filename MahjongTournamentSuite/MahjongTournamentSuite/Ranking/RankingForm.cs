using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;
using System.Runtime.InteropServices;

namespace MahjongTournamentSuite.Ranking
{
    public partial class RankingForm : Form, IRankingForm
    {
        #region Constants

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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
        private int _actualNumRows = RankingPresenter.DEFAULT_NUM_ROWS_PER_SCREEN;

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

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void RankingForm_Resize(object sender, EventArgs e)
        {
            CalculateAndSetRowHeightToFillScreen();
        }

        private void RankingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _presenter.StopShowRankingThread();
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
            lblRankingTitle.Text = "PLAYERS RANKING";
            _actualNumRows = playersRankingsRange.Count;
            dgv.DataSource = playersRankingsRange;

            //Visible
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_ID].Visible = true;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_NAME].Visible = true;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_POINTS].Visible = true;
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_SCORE].Visible = true;
            dgv.Columns[COLUMN_PLAYER_RANKING_TEAM_ID].Visible = false;
            dgv.Columns[COLUMN_PLAYER_RANKING_TEAM_NAME].Visible = true;
            dgv.Columns[COLUMN_PLAYER_RANKING_COUNTRY_ID].Visible = false;
            dgv.Columns[COLUMN_PLAYER_RANKING_COUNTRY_NAME].Visible = true;
            //HeaderText
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_ID].HeaderText = "#";
            dgv.Columns[COLUMN_PLAYER_RANKING_PLAYER_NAME].HeaderText = "PLAYER NAME";
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
            lblRankingTitle.Text = "TEAMS RANKING";
            _actualNumRows = teamsRankingsRange.Count;
            dgv.DataSource = teamsRankingsRange;

            //Visible
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_ID].Visible = true;
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_NAME].Visible = true;
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_POINTS].Visible = true;
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_SCORE].Visible = true;
            //HeaderText
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_ID].HeaderText = "#";
            dgv.Columns[COLUMN_TEAM_RANKING_TEAM_NAME].HeaderText = "TEAM NAME";
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

        private void CalculateAndSetRowHeightToFillScreen()
        {
            int rowsTotalSpace = dgv.Height - dgv.ColumnHeadersHeight;
            int newRowHeight = rowsTotalSpace / _actualNumRows;
            dgv.RowTemplate.Height = newRowHeight;
        }

        #endregion

        private void RankingForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Maximized)
                    WindowState = FormWindowState.Normal;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
