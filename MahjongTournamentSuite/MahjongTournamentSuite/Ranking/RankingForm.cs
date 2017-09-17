using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MahjongTournamentSuite.Ranking
{
    public partial class RankingForm : Form, IRankingForm
    {
        #region Constants

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        #endregion

        #region Fields

        private IRankingPresenter _presenter;
        private Rankings _rankings;
        private int _numRowsPerScreen = RankingPresenter.DEFAULT_NUM_ROWS_PER_SCREEN;

        #endregion

        #region Constructor

        public RankingForm(Rankings rankings)
        {
            InitializeComponent();
            _presenter = Injector.provideRankingPresenter(this);
            _rankings = rankings;
        }

        #endregion

        #region Events

        private void RankingForm_Load(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.LoadData(_rankings);
            _presenter.StartShowRankingThread();
            ShowDefaultCursor();
        }

        private void btnSecondsUp_Click(object sender, EventArgs e)
        {
            _presenter.IncrementShowingTimeInOneSecond();
        }

        private void btnSecondsDown_Click(object sender, EventArgs e)
        {
            _presenter.DecrementShowingTimeInOneSecond();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _presenter.StopShowRankingThread();
        }

        private void RankingForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void RankingForm_Resize(object sender, EventArgs e)
        {
            if (dgv.DataSource != null)
                CalculateAndSetDefaultRowHeightToFillScreen();
        }

        private void RankingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShowWaitCursor();
            _presenter.AbortShowRankingThreadIfAlive();
            ShowDefaultCursor();
        }

        #endregion

        #region IMainForm implementation

        public void SetNumRowsPerScreen(int numRowsPerScreen)
        {
            ShowWaitCursor();
            _numRowsPerScreen = numRowsPerScreen;
            CalculateAndSetDefaultRowHeightToFillScreen();
            ShowDefaultCursor();
        }

        public void FillDGVPlayersFromThread(List<PlayerRanking> playersRankingsRange, bool isTeams)
        {
            Invoke(new MethodInvoker(() =>
            {
                ShowWaitCursor();
                FillDGVPlayers(playersRankingsRange, isTeams);
                ShowDefaultCursor();
            }));
        }

        public void FillDGVTeamsFromThread(List<TeamRanking> teamsRankingsRange)
        {
            Invoke(new MethodInvoker(() =>
            {
                ShowWaitCursor();
                FillDGVTeams(teamsRankingsRange);
                ShowDefaultCursor();
            }));
        }

        public void FillDGVPlayersChickenHandsFromThread(List<ChickenHandRanking> playersChickenHandsRankingsRange)
        {
            Invoke(new MethodInvoker(() => 
            {
                ShowWaitCursor();
                FillDGVPlayersChickenHands(playersChickenHandsRankingsRange);
                ShowDefaultCursor();
            }));
        }

        public void CloseFormFromThread()
        {
            Invoke(new MethodInvoker(() =>
            {
                Close();
                ShowDefaultCursor();
            }));
        }

        public void ShowButtonSecondsDown()
        {
            btnSecondsDown.Visible = true;
        }

        public void HideButtonSecondsDown()
        {
            btnSecondsDown.Visible = false;
        }

        public void SetSecondsLabel(string seconds)
        {
            lblSeconds.Text = seconds;
        }

        #endregion

        #region Private

        private void FillDGVPlayers(List<PlayerRanking> playersRankingsRange, bool isTeams)
        {
            pbIconTitle.Image = Properties.Resources.players_big;
            lblRankingTitle.Text = "PLAYERS RANKING";
            dgv.DataSource = playersRankingsRange;

            //Visible
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_ORDER].Visible = true;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_ID].Visible = false;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_NAME].Visible = true;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_POINTS].Visible = true;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_SCORE].Visible = true;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_TEAM_ID].Visible = false;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_TEAM_NAME].Visible = isTeams;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_NAME].Visible = false;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_HTML_FLAG_URL].Visible = false;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].Visible = true;
            //HeaderText
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_ORDER].HeaderText = "#";
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_NAME].HeaderText = "Player name";
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_POINTS].HeaderText = "Points";
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_SCORE].HeaderText = "Score";
            if(isTeams)
                dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_TEAM_NAME].HeaderText = "Team";
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].HeaderText = "Country";
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //Column Flags Header&Cell Content Alignment
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //DisplayIndex
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_ORDER].DisplayIndex = 0;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_NAME].DisplayIndex = 1;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_POINTS].DisplayIndex = 2;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_SCORE].DisplayIndex = 3;
            if (isTeams)
            {
                dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_TEAM_NAME].DisplayIndex = 4;
                dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_NAME].DisplayIndex = 5;
            }
            else
                dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].DisplayIndex = 4;
            //Sortable
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_ORDER].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_POINTS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_PLAYER_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_TEAM_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_TEAM_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].SortMode = DataGridViewColumnSortMode.NotSortable;

            CenterDGV();
        }

        private void FillDGVTeams(List<TeamRanking> teamsRankingsRange)
        {
            pbIconTitle.Image = Properties.Resources.teams_big;
            lblRankingTitle.Text = "TEAMS RANKING";
            dgv.DataSource = teamsRankingsRange;

            //Visible
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_ORDER].Visible = true;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_ID].Visible = false;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_NAME].Visible = true;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_POINTS].Visible = true;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_SCORE].Visible = true;
            //HeaderText
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_ORDER].HeaderText = "#";
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_NAME].HeaderText = "Team name";
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_POINTS].HeaderText = "Points";
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_SCORE].HeaderText = "Score";
            //DisplayIndex
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_ORDER].DisplayIndex = 0;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_NAME].DisplayIndex = 1;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_POINTS].DisplayIndex = 2;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_SCORE].DisplayIndex = 3;
            //Sortable
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_ORDER].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_POINTS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[TeamRanking.COLUMN_TEAM_RANKING_TEAM_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;

            CenterDGV();
        }

        private void FillDGVPlayersChickenHands(List<ChickenHandRanking> playersChickenHandsRankingsRange)
        {
            pbIconTitle.Image = Properties.Resources.chicken_big;
            lblRankingTitle.Text = "CHICKEN HAND RANKING";
            dgv.DataSource = playersChickenHandsRankingsRange;

            //Visible
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_ORDER].Visible = true;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_ID].Visible = false;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_NAME].Visible = true;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_NUM_CHICKEN_HANDS].Visible = true;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_POINTS].Visible = true;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_SCORE].Visible = true;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_NAME].Visible = false;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_HTML_FLAG_URL].Visible = false;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].Visible = true;
            //HeaderText             
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_ORDER].HeaderText = "#";
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_NAME].HeaderText = "Player name";
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_NUM_CHICKEN_HANDS].HeaderText = "Chicken hands";
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_POINTS].HeaderText = "Points";
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_SCORE].HeaderText = "Score";
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].HeaderText = "Country";
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //Column Flags Header&Cell Content Alignment
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //Padding
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_NUM_CHICKEN_HANDS].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_POINTS].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_SCORE].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            //DisplayIndex
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_ORDER].DisplayIndex = 0;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_NAME].DisplayIndex = 1;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_NUM_CHICKEN_HANDS].DisplayIndex = 2;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_POINTS].DisplayIndex = 3;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_SCORE].DisplayIndex = 4;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].DisplayIndex = 5;
            //Sortable
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_ORDER].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_NUM_CHICKEN_HANDS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_POINTS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].SortMode = DataGridViewColumnSortMode.NotSortable;

            CenterDGV();
        }

        private void CenterDGV()
        {
            var totalWidth = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, dgv.ClientSize.Height);
            dgv.Location = new Point((Width - totalWidth) / 2, dgv.Location.Y);
        }

        private void CalculateAndSetDefaultRowHeightToFillScreen()
        {
            int rowsTotalSpace = dgv.Height - dgv.ColumnHeadersHeight;
            int newRowHeight = rowsTotalSpace / _numRowsPerScreen;
            dgv.RowTemplate.Height = newRowHeight;
            dgv.Refresh();
        }

        private void ShowWaitCursor()
        {
            Cursor = Cursors.WaitCursor;
        }

        private void ShowDefaultCursor()
        {
            Cursor = Cursors.Default;
        }

        #endregion
    }
}
