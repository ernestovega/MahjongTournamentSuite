using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MahjongTournamentSuite.ViewModel;
using System.Drawing;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.Ranking;
using MahjongTournamentSuite.HTMLViewer;
using MahjongTournamentSuite.ManagePlayers;
using MahjongTournamentSuite.TeamsManager;
using MahjongTournamentSuite.EmaReport;
using System.Media;
using MahjongTournamentSuite.PlayersTables;

namespace MahjongTournamentSuite.TournamentManager
{
    public partial class TournamentManagerForm : Form, ITournamentManagerForm
    {
        #region Constants

        private const int BUTTON_SIDE = 64;
        private const int MARGIN_SIZE = 5;
        private const int TABLES_MARGIN_SIZE = 10;
        private const int TITLE_ROUNDS_HEIGHT = 23;
        private const int TITLE_TABLES_HEIGHT = 38;
        private const int NUM_TABLES_BUTTONS_HORIZONTAL = 10;
        private const int SEPARATOR_EXTRA_MARGIN_BOTTOM = 12;

        #endregion

        #region Fields

        private ITournamentManagerController _controller;
        private int _tournamentId;

        #endregion

        #region Constructor

        public TournamentManagerForm(int tournamentId)
        {
            InitializeComponent();
            _controller = Injector.provideTournamentManagerController(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Lifecycle

        private void TournamentManagerForm_Load(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.LoadTournament(_tournamentId);
            ShowDefaultCursor();
        }

        private void TournamentManagerForm_SizeChanged(object sender, EventArgs e)
        {
            ShowWaitCursor();
            if (_controller != null)
                _controller.OnFormResized(_tournamentId);
            ShowDefaultCursor();
        }

        private void TournamentManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            new HomeForm().Show();
        }

        #endregion

        #region Events

        private void btnTeams_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.ButtonTeamsClicked();
            ShowDefaultCursor();
        }

        private void btnPlayers_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.ButtonPlayersClicked();
            ShowDefaultCursor();
        }

        private void btnPlayersTables_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.PlayersTablesClicked();
            ShowDefaultCursor();
        }

        private void btnEmaReport_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.EmaReportClicked();
            ShowDefaultCursor();
        }

        private void btnRankings_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.ShowRankingsClicked();
            ShowDefaultCursor();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
            ShowDefaultCursor();
        }

        #endregion

        #region ITournamentManagerForm

        public void AddRoundsButtons(int numRounds)
        {
            int numButtonsHorizontal = (splitContainer.Width - (MARGIN_SIZE * 3)) / (BUTTON_SIDE + MARGIN_SIZE);

            Point buttonStartPoint = new Point(MARGIN_SIZE, TITLE_ROUNDS_HEIGHT + TABLES_MARGIN_SIZE);
            if (numButtonsHorizontal >= numRounds)
            {
                int neededWidth = ((numRounds * BUTTON_SIDE) + ((numRounds + 1) * MARGIN_SIZE));
                buttonStartPoint.X = (splitContainer.Width - neededWidth) / 2;
            }
            int rowsCount = 0;
            for (int i = 1; i <= numRounds; i++)
            {
                Button btnRound = GetNewButton();
                btnRound.Tag = i;
                btnRound.TabIndex = i + 7;
                btnRound.Text = string.Format("\n{0}", i);
                btnRound.Location = buttonStartPoint;
                btnRound.Image = _controller.IsRoundCompleted(i) ?
                    Properties.Resources.gong_ok : Properties.Resources.gong;
                btnRound.Click += delegate
                {
                    ShowWaitCursor();
                    _controller.ButtonRoundClicked((int)btnRound.Tag);
                    ShowDefaultCursor();
                };

                splitContainer.Panel1.Controls.Add(btnRound);

                int numButtonsInActualRow = i - (rowsCount * numButtonsHorizontal);
                if (numButtonsInActualRow == numButtonsHorizontal)
                {
                    buttonStartPoint.Y += BUTTON_SIDE + MARGIN_SIZE;
                    buttonStartPoint.X = MARGIN_SIZE;
                    rowsCount++;
                }
                else
                    buttonStartPoint.X += BUTTON_SIDE + MARGIN_SIZE;
            }
            //Fijamos el tamaño vertical del contenedor para evitar el scroll
            splitContainer.SplitterDistance = ((rowsCount + 1) * BUTTON_SIDE) + ((rowsCount + 2) * MARGIN_SIZE) 
                + SEPARATOR_EXTRA_MARGIN_BOTTOM + TITLE_ROUNDS_HEIGHT;
        }

        public void AddTablesButtons(int numTables)
        {
            //Calculamos el punto de comienzo para centrar los botones
            int neededWidth;
            if(numTables < NUM_TABLES_BUTTONS_HORIZONTAL)
                neededWidth = ((numTables * BUTTON_SIDE) + ((numTables + 1) * TABLES_MARGIN_SIZE));
            else
                neededWidth = ((NUM_TABLES_BUTTONS_HORIZONTAL * BUTTON_SIDE) + ((NUM_TABLES_BUTTONS_HORIZONTAL + 1) * TABLES_MARGIN_SIZE));

            int initPoint = (splitContainer.Width - neededWidth) / 2;
            Point buttonStartPoint = new Point(initPoint, TITLE_TABLES_HEIGHT + TABLES_MARGIN_SIZE);

            int rowsCount = 0;
            for (int i = 1; i <= numTables; i++)
            {
                Button button = GetNewButton();
                button.Tag = i;
                button.TabIndex = i + 7 + _controller.GetNumRounds();
                button.Text = string.Format("\n{0}", i);
                button.Width = BUTTON_SIDE;
                button.Height = BUTTON_SIDE;
                button.Location = buttonStartPoint;
                Image icon;
                if (_controller.IsTableCompleted(i))
                    icon = Properties.Resources.table_ok;
                else
                {
                    if (_controller.IsTableUsingTotalsOnly(i))
                        icon = Properties.Resources.table_totalsonly;
                    else
                        icon = Properties.Resources.table;
                }
                button.Image = icon;

                button.Click += delegate
                {
                    ShowWaitCursor();
                    _controller.ButtonTableClicked((int)button.Tag);
                    ShowDefaultCursor();
                };

                splitContainer.Panel2.Controls.Add(button);
                
                if (i < numTables)
                {
                    int numButtonsInActualRow = i - (rowsCount * NUM_TABLES_BUTTONS_HORIZONTAL);
                    if (numButtonsInActualRow == NUM_TABLES_BUTTONS_HORIZONTAL)
                    {
                        buttonStartPoint.Y += BUTTON_SIDE + TABLES_MARGIN_SIZE;
                        buttonStartPoint.X = initPoint;
                        rowsCount++;
                    }
                    else
                        buttonStartPoint.X += BUTTON_SIDE + TABLES_MARGIN_SIZE;
                }
                else
                    return;
            }
        }

        public void RemoveRoundsButtons()
        {
            List<Control> panelTournamentControls = new List<Control>();
            foreach (Control control in splitContainer.Panel1.Controls)
            {
                if (control.GetType() == typeof(Button))
                    panelTournamentControls.Add(control);
            }
            foreach (Control control in panelTournamentControls)
                splitContainer.Panel1.Controls.Remove(control);
        }

        public void RemoveTablesButtons()
        {
            List<Control> panelTournamentControls = new List<Control>();
            foreach (Control control in splitContainer.Panel2.Controls)
            {
                if (control.GetType() == typeof(Button))
                    panelTournamentControls.Add(control);
            }
            foreach (Control control in panelTournamentControls)
                splitContainer.Panel2.Controls.Remove(control);
        }

        public void GoToTeamsManager()
        {
            TeamsManagerForm form = new TeamsManagerForm(_tournamentId);
            form.FormClosed += new FormClosedEventHandler(TeamsManagerForm_FormClosed);
            form.ShowDialog();
        }

        private void TeamsManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowWaitCursor();
            _controller.TeamsManagerFormClosed();
            ShowDefaultCursor();
        }

        public void GoToPlayersManager()
        {
            using (var playersManagerForm = new PlayersManagerForm(_tournamentId))
            {
                if (playersManagerForm.ShowDialog() == DialogResult.OK)
                    _controller.PlayersManagerFormClosed(false);
                else
                    _controller.PlayersManagerFormClosed(true);
            }
        }

        public void GoToTableManager(int roundId, int tableId)
        {
            TableManagerForm form = new TableManagerForm(_tournamentId, roundId, tableId);
            form.FormClosed += new FormClosedEventHandler(TableManagerForm_FormClosed);
            form.ShowDialog();
        }

        private void TableManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowWaitCursor();
            _controller.TableManagerFormClosed();
            ShowDefaultCursor();
        }

        public void GoToRankings(Rankings rankings)
        {
            try
            {
                new RankingForm(rankings).Show();
            }
            catch { }
        }

        public void GoToHTMLViewer(HTMLRankings htmlRankings)
        {
            new HTMLViewerForm(htmlRankings).ShowDialog();
        }

        public void GoToPlayersTables(int tournamentId)
        {
            new PlayersTablesForm(tournamentId).ShowDialog();
        }

        public void GoToEmaReport(List<DGVEmaReportPlayer> dgvEmaReportPlayers)
        {
            new EmaReportForm(dgvEmaReportPlayers).ShowDialog();
        }

        public void SelectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                {
                    Image icon;
                    if (_controller.IsRoundCompleted(roundId))
                        icon = Properties.Resources.gong_ok_white;
                    else
                        icon = Properties.Resources.gong_white;

                    MakeButtonSelected((Button)control, icon);
                }
            }
        }

        public void UnselectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                {
                    Image icon;
                    if (_controller.IsRoundCompleted(roundId))
                        icon = Properties.Resources.gong_ok;
                    else
                        icon = Properties.Resources.gong;

                    MakeButtonUnselected((Button)control, icon);
                }
            }
        }

        public void SelectTableButton(int tableId)
        {
            foreach (Control control in splitContainer.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                {
                    Image icon;
                    if (_controller.IsTableCompleted(tableId))
                        icon = Properties.Resources.table_ok_white;
                    else
                    {
                        if (_controller.IsTableUsingTotalsOnly(tableId))
                            icon = Properties.Resources.table_totalsonly_white;
                        else
                            icon = Properties.Resources.table_white;
                    }

                    MakeButtonSelected((Button)control, icon);
                }
            }
        }

        public void UnselectTableButton(int tableId)
        {
            foreach (Control control in splitContainer.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                {
                    Image icon;
                    if (_controller.IsTableCompleted(tableId))
                        icon = Properties.Resources.table_ok;
                    else
                    {
                        if (_controller.IsTableUsingTotalsOnly(tableId))
                            icon = Properties.Resources.table_totalsonly;
                        else
                            icon = Properties.Resources.table;
                    }

                    MakeButtonUnselected((Button)control, icon);
                }
            }
        }

        public void ShowButtonTeams()
        {
            btnTeams.Visible = true;
        }

        public void HideButtonTeams()
        {
            btnTeams.Visible = false;
        }
        
        public void ShowWaitCursor()
        {
            Cursor = Cursors.WaitCursor;
        }

        public void ShowDefaultCursor()
        {
            Cursor = Cursors.Default;
        }

        public void PlayKoSound()
        {
            SystemSounds.Exclamation.Play();
        }

        public void SetTournamentName(string tournamentName)
        {
            lblTournamentName.Text = tournamentName;
        }

        public void CenterMainButtons()
        {
            int newX = (Size.Width - flowLayoutPanelButtons.Size.Width) / 2;
            flowLayoutPanelButtons.Location = new Point(newX, flowLayoutPanelButtons.Location.Y);
        }

        #endregion

        #region Private

        private static Button GetNewButton()
        {
            Button newButton = new Button();
            newButton.AutoSize = false;
            newButton.Width = BUTTON_SIDE;
            newButton.Height = BUTTON_SIDE;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderSize = 0;
            newButton.FlatAppearance.MouseDownBackColor = ColorConstants.GREEN_MM_DARK;
            newButton.FlatAppearance.MouseOverBackColor = ColorConstants.GREEN_MM;
            newButton.BackgroundImageLayout = ImageLayout.None;
            newButton.Cursor = Cursors.Hand;
            newButton.Font = new Font(newButton.Font.Name, newButton.Font.Size, FontStyle.Bold);
            newButton.Margin = new Padding(5, 0, 5, 0);
            newButton.Padding = new Padding(0, 3, 0, 0);
            newButton.ImageAlign = ContentAlignment.TopCenter;
            newButton.TextImageRelation = TextImageRelation.ImageAboveText;
            newButton.UseVisualStyleBackColor = false;
            return newButton;
        }

        private static void MakeButtonSelected(Button button, Image image)
        {
            button.BackColor = ColorConstants.GREEN_MM;
            button.ForeColor = Color.White;
            button.Image = image;
        }

        private static void MakeButtonUnselected(Button button, Image image)
        {
            button.BackColor = SystemColors.Control;
            button.ForeColor = SystemColors.ControlText;
            button.Image = image;
        }

        #endregion
    }
}
