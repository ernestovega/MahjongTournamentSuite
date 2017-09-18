using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;
using System.Drawing;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.Ranking;
using MahjongTournamentSuite.HTMLViewer;
using MahjongTournamentSuite.ManagePlayers;
using MahjongTournamentSuite.TeamsManager;
using System.Media;

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

        private ITournamentManagerPresenter _presenter;
        private int _tournamentId;
        private PlayersManagerForm _playersManagerForm;

        #endregion

        #region Constructor

        public TournamentManagerForm(int tournamentId)
        {
            InitializeComponent();
            _presenter = Injector.provideTournamentManagerPresenter(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Lifecycle

        private void TournamentManagerForm_Load(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.LoadTournament(_tournamentId);
            ShowDefaultCursor();
        }

        private void TournamentManagerForm_Resize(object sender, EventArgs e)
        {
            ShowWaitCursor();
            if (_presenter != null)
                _presenter.OnFormResized();
            ShowDefaultCursor();
        }

        private void TournamentManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            new HomeForm().Show();
        }

        #endregion

        #region Events

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        private void btnExportHTML_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.ExportRankingsToHTMLClicked();
            ShowDefaultCursor();
        }

        private void btnTeams_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.ButtonTeamsClicked();
            ShowDefaultCursor();
        }

        private void btnPlayers_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.ButtonPlayersClicked();
            ShowDefaultCursor();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
            ShowDefaultCursor();
        }

        private void btnRankings_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.ShowRankingsClicked();
            ShowDefaultCursor();
        }

        #endregion

        #region ITournamentManagerForm

        public void AddRoundsButtons(int numRounds)
        {
            int numButtonsHorizontal = (splitContainer1.Width - (MARGIN_SIZE * 3)) / (BUTTON_SIDE + MARGIN_SIZE);

            Point buttonStartPoint = new Point(MARGIN_SIZE, TITLE_ROUNDS_HEIGHT + MARGIN_SIZE);
            if (numButtonsHorizontal >= numRounds)
            {
                int neededWidth = ((numRounds * BUTTON_SIDE) + ((numRounds + 1) * MARGIN_SIZE));
                buttonStartPoint.X = (splitContainer1.Width - neededWidth) / 2;
            }
            int rowsCount = 0;
            for (int i = 1; i <= numRounds; i++)
            {
                Button btnRound = GetNewButton();
                btnRound.Tag = i;
                btnRound.Text = string.Format("\n{0}", i);
                btnRound.Location = buttonStartPoint;
                btnRound.Image = _presenter.IsRoundCompleted(i) ?
                    Properties.Resources.gong_ok : Properties.Resources.gong;
                btnRound.Click += delegate
                {
                    ShowWaitCursor();
                    _presenter.ButtonRoundClicked((int)btnRound.Tag);
                    ShowDefaultCursor();
                };

                splitContainer1.Panel1.Controls.Add(btnRound);

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
            splitContainer1.SplitterDistance = ((rowsCount + 1) * BUTTON_SIDE) + ((rowsCount + 2) * MARGIN_SIZE) 
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

            int initPoint = (splitContainer1.Width - neededWidth) / 2;
            Point buttonStartPoint = new Point(initPoint, TITLE_TABLES_HEIGHT + TABLES_MARGIN_SIZE);

            int rowsCount = 0;
            for (int i = 1; i <= numTables; i++)
            {
                Button button = GetNewButton();
                button.Tag = i;
                button.Text = string.Format("\n{0}", i);
                button.Width = BUTTON_SIDE;
                button.Height = BUTTON_SIDE;
                button.Location = buttonStartPoint;
                button.Image = _presenter.IsTableCompleted(i) ? 
                    Properties.Resources.table_ok : Properties.Resources.table;
                button.Click += delegate
                {
                    ShowWaitCursor();
                    _presenter.ButtonTableClicked((int)button.Tag);
                    ShowDefaultCursor();
                };


                    splitContainer1.Panel2.Controls.Add(button);


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
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.GetType() == typeof(Button))
                    panelTournamentControls.Add(control);
            }
            foreach (Control control in panelTournamentControls)
                splitContainer1.Panel1.Controls.Remove(control);
        }

        public void RemoveTablesButtons()
        {
            List<Control> panelTournamentControls = new List<Control>();
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control.GetType() == typeof(Button))
                    panelTournamentControls.Add(control);
            }
            foreach (Control control in panelTournamentControls)
                splitContainer1.Panel2.Controls.Remove(control);
        }

        public void GoToTeamsManager()
        {
            TeamsManagerForm form = new TeamsManagerForm(_tournamentId);
            form.FormClosed += new FormClosedEventHandler(TeamsManagerForm_FormClosed);
            form.ShowDialog();
        }

        public void GoToPlayersManager()
        {
            using (var playersManagerForm = new PlayersManagerForm(_tournamentId))
            {
                if (playersManagerForm.ShowDialog() == DialogResult.OK)
                    _presenter.PlayersManagerFormClosed(true);
                else
                    _presenter.PlayersManagerFormClosed(false);
            }
        }

        public void GoToTableManager(int roundId, int tableId)
        {
            new TableManagerForm(_tournamentId, roundId, tableId).ShowDialog();
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
        
        public void SelectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                {
                    MakeButtonSelected((Button)control,
                        _presenter.IsRoundCompleted(roundId) ?
                        Properties.Resources.gong_ok_white : Properties.Resources.gong_white);
                }
            }
        }

        public void UnselectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                    MakeButtonUnselected((Button)control,
                        _presenter.IsRoundCompleted(roundId) ?
                        Properties.Resources.gong_ok : Properties.Resources.gong);
            }
        }

        public void SelectTableButton(int tableId)
        {
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                    MakeButtonSelected((Button)control, 
                        _presenter.IsTableCompleted(tableId) ?
                        Properties.Resources.table_ok : Properties.Resources.table);
            }
        }

        public void UnselectTableButton(int tableId)
        {
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                    MakeButtonUnselected((Button)control, Properties.Resources.table);
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
            newButton.FlatAppearance.MouseDownBackColor = MyConstants.GREEN_MM_DARK;
            newButton.FlatAppearance.MouseOverBackColor = MyConstants.GREEN_MM;
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

        private void TeamsManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowWaitCursor();
            _presenter.TeamsManagerFormClosed();
            ShowDefaultCursor();
        }

        private static void MakeButtonSelected(Button button, Image image)
        {
            button.BackColor = MyConstants.GREEN_MM;
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
