using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MahjongTournamentSuite.ViewModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

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

        private IRankingController _controller;
        private Rankings _rankings;
        private int _numRowsPerScreen = RankingController.DEFAULT_NUM_ROWS_PER_SCREEN;

        #endregion

        #region Constructor

        public RankingForm(Rankings rankings)
        {
            InitializeComponent();
            _controller = Injector.provideRankingController(this);
            _rankings = rankings;
        }

        #endregion

        #region Events

        private void RankingForm_Load(object sender, EventArgs e)
        {
            ShowWaitCursor();
            CenterPanelTitle();
            CenterLabelUrlLiveRanking();
            _controller.LoadData(_rankings);
            ShowDefaultCursor();
        }

        private void RankingForm_Resize(object sender, EventArgs e)
        {
            if (dgv.DataSource != null)
            {
                CalculateAndSetDefaultRowHeightToFillScreen();
                CenterPanelTitle();
                CenterDGV();
                CenterLabelUrlLiveRanking();
            }
        }

        private void RankingForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            Padding newPadding;

            if (WindowState != FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Maximized;
                newPadding = new Padding(8, 0, 8, 0);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                newPadding = new Padding(32, 0, 32, 0);
            }

            dgv.DefaultCellStyle.Padding = newPadding;

            //foreach (DataGridViewRow row in dgv.Rows)
            //{
            //    if (row.Index >= 0)
            //    {
            //        foreach (DataGridViewCell cell in row.Cells)
            //            cell.Style.Padding = newPadding;
            //    }
            //}
        }

        private void btnSecondsUp_Click(object sender, EventArgs e)
        {
            _controller.IncrementShowingTime();
        }

        private void btnSecondsDown_Click(object sender, EventArgs e)
        {
            _controller.DecrementShowingTime();
        }

        private void btnRowsUp_Click(object sender, EventArgs e)
        {
            _controller.IncrementShowingRows();
        }

        private void btnRowsDown_Click(object sender, EventArgs e)
        {
            _controller.DecrementShowingRows();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _controller.PlayClicked();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _controller.PauseClicked();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _controller.StopShowRankingThread();
        }

        private void lblLiveRankingUrl_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/9thsomcliveranking");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        #endregion

        #region IMainForm implementation

        public void SetNumRowsPerScreen(int numRowsPerScreen)
        {
            ShowWaitCursor();
            _numRowsPerScreen = numRowsPerScreen;
            CalculateAndSetDefaultRowHeightToFillScreen();
            lblNumRows.Text = _numRowsPerScreen.ToString();
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

        public void FillDGVPlayersBestHandsFromThread(List<BestHandRanking> playersBestHandsRankingsRange)
        {
            Invoke(new MethodInvoker(() =>
            {
                ShowWaitCursor();
                FillDGVPlayersBestHands(playersBestHandsRankingsRange);
                ShowDefaultCursor();
            }));
        }

        public void CloseForm()
        {
            Close();
            ShowDefaultCursor();
        }

        public void SetNumSecondsLabel(string numSeconds)
        {
            lblNumSeconds.Text = numSeconds;
        }

        public void SetNumRowsLabel(string numRows)
        {
            lblNumRows.Text = numRows;
        }

        public void UpdateProgressFromThread(string leftTime)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblProgress.Text = leftTime;
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

        public void ShowButtonRowsDown()
        {
            btnRowsDown.Visible = true;
        }

        public void HideButtonRowsDown()
        {
            btnRowsDown.Visible = false;
        }

        public void ShowButtonRowsUp()
        {
            btnRowsUp.Visible = true;
        }

        public void HideButtonRowsUp()
        {
            btnRowsUp.Visible = false;
        }

        public void ShowButtonPlay()
        {
            btnPlay.Visible = true;
        }

        public void HideButtonPlay()
        {
            btnPlay.Visible = false;
        }

        public void ShowButtonPause()
        {
            btnPause.Visible = true;
        }

        public void HideButtonPause()
        {
            btnPause.Visible = false;
        }

        #endregion

        #region Private

        private void FillDGVPlayers(List<PlayerRanking> playersRankingsRange, bool isTeams)
        {
            pbIconTitle.Image = Properties.Resources.players_big;
            lblRankingTitle.Text = "PLAYERS";
            CenterPanelTitle();
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
            dgv.Columns[PlayerRanking.COLUMN_PLAYER_RANKING_COUNTRY_FLAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            CalculateAndSetDefaultRowHeightToFillScreen();
        }

        private void FillDGVTeams(List<TeamRanking> teamsRankingsRange)
        {
            pbIconTitle.Image = Properties.Resources.teams_big;
            lblRankingTitle.Text = "TEAMS";
            CenterPanelTitle();
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
            CalculateAndSetDefaultRowHeightToFillScreen();
        }

        private void FillDGVPlayersChickenHands(List<ChickenHandRanking> playersChickenHandsRankingsRange)
        {
            pbIconTitle.Image = Properties.Resources.chicken_big;
            lblRankingTitle.Text = "CHICKEN HANDS";
            CenterPanelTitle();
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
            dgv.Columns[ChickenHandRanking.COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            CalculateAndSetDefaultRowHeightToFillScreen();
        }

        public void FillDGVPlayersBestHands(List<BestHandRanking> playersBestHandsRankingsRange)
        {
            pbIconTitle.Image = Properties.Resources.best_hand_big;
            lblRankingTitle.Text = "BEST HANDS";
            CenterPanelTitle();
            dgv.DataSource = playersBestHandsRankingsRange;

            //Visible
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_ORDER].Visible = true;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_ID].Visible = false;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_NAME].Visible = true;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_BEST_HAND_VALUE].Visible = true;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_POINTS].Visible = true;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_SCORE].Visible = true;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_NAME].Visible = false;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_HTML_FLAG_URL].Visible = false;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].Visible = true;
            //HeaderText             
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_ORDER].HeaderText = "#";
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_NAME].HeaderText = "Player name";
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_BEST_HAND_VALUE].HeaderText = "Best hand";
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_POINTS].HeaderText = "Points";
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_SCORE].HeaderText = "Score";
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].HeaderText = "Country";
            //Column Flags Image Layout
            ((DataGridViewImageColumn)dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            //Column Flags Header&Cell Content Alignment
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Padding
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_BEST_HAND_VALUE].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_POINTS].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_SCORE].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].DefaultCellStyle.Padding = new Padding(25, 0, 25, 0);
            //DisplayIndex
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_ORDER].DisplayIndex = 0;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_NAME].DisplayIndex = 1;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_BEST_HAND_VALUE].DisplayIndex = 2;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_POINTS].DisplayIndex = 3;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_SCORE].DisplayIndex = 4;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].DisplayIndex = 5;
            //Sortable
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_ORDER].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_BEST_HAND_VALUE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_POINTS].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[BestHandRanking.COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG].SortMode = DataGridViewColumnSortMode.NotSortable;

            CenterDGV();
            CalculateAndSetDefaultRowHeightToFillScreen();
        }

        private void CenterPanelTitle()
        {
            flowLayoutPanelTitle.Location = 
                new Point((Width - flowLayoutPanelTitle.Width) / 2, flowLayoutPanelTitle.Location.Y);
        }

        private void CenterLabelUrlLiveRanking()
        {
            lblLiveRankingUrl.Location =
                new Point((Width - lblLiveRankingUrl.Width) / 2, lblLiveRankingUrl.Location.Y);
        }

        private void CenterDGV()
        {
            var totalWidth = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, dgv.ClientSize.Height);
            dgv.Location = new Point((Width - totalWidth) / 2, dgv.Location.Y);
        }

        private void CalculateAndSetDefaultRowHeightToFillScreen()
        {
            float fontSize = (dgv.ClientSize.Height / _numRowsPerScreen) / 1.4f;
            dgv.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            int rowsTotalSpace = dgv.ClientSize.Height - dgv.ColumnHeadersHeight;
            int newRowHeight = rowsTotalSpace / _numRowsPerScreen;
            dgv.RowTemplate.Height = newRowHeight;
            foreach (DataGridViewRow row in dgv.Rows)
                row.Height = newRowHeight;

            dgv.ScrollBars = ScrollBars.None;
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
