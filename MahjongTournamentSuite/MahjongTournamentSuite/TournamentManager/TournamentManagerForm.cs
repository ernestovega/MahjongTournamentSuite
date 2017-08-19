﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;
using System.Drawing;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.TableManager;

namespace MahjongTournamentSuite.TournamentManager
{
    public partial class TournamentManagerForm : Form, ITournamentManagerForm
    {
        #region Constants

        private const int BUTTON_SIDE = 64;
        private const int MARGIN_SIZE = 5;
        private const int TABLE_BUTTON_SIDE = 96;
        private const int TABLE_MARGIN_SIZE = 10;
        private static readonly Color GREEN_MM = Color.FromArgb(0, 177, 106);

        private const string COLUMN_TEAMS_TOURNAMENT_ID = "TournamentId";
        private const string COLUMN_TEAMS_ID = "Id";
        private const string COLUMN_TEAMS_NAME = "Name";
        private const int COLUMN_TEAMS_NAME_INDEX = 2;

        private const string COLUMN_PLAYERS_TOURNAMENT_ID = "TournamentId";
        private const string COLUMN_PLAYERS_ID = "Id";
        private const string COLUMN_PLAYERS_NAME = "Name";
        private const string COLUMN_PLAYERS_TEAM = "TeamId";
        private const string COLUMN_PLAYERS_COUNTRY = "CountryId";
        private const int COLUMN_PLAYERS_NAME_INDEX = 2;
        private const int COLUMN_PLAYERS_TEAM_INDEX = 3;
        private const int COLUMN_PLAYERS_COUNTRY_INDEX = 4;

        #endregion

        #region Fields

        private ITournamentManagerPresenter _presenter;
        private int _tournamentId;

        #endregion

        #region Constructor

        public TournamentManagerForm(int tournamentId)
        {
            InitializeComponent();
            _tournamentId = tournamentId;
            _presenter = Injector.provideTournamentManagerPresenter(this);
            _presenter.LoadTournament(_tournamentId);
        }

        #endregion

        #region Lifecycle

        private void TournamentManagerForm_Resize(object sender, EventArgs e)
        {
            _presenter.OnFormResized();
        }

        private void TournamentManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            new HomeForm().Show();
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

        private void btnTeams_Click(object sender, EventArgs e)
        {
            _presenter.ButtonTeamsClicked();
        }

        private void btnPlayers_Click(object sender, EventArgs e)
        {
            _presenter.ButtonPlayersClicked();
        }

        private void btnRounds_Click(object sender, EventArgs e)
        {
            _presenter.ButtonRoundsClicked();
        }

        private void btnTimer_Click(object sender, System.EventArgs e)
        {
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
        }

        private void btnRanking_Click(object sender, System.EventArgs e)
        {
            //var mahjongTournamentRankingShower = new MahjongTournamentRankingShower.Program();
            //Process.Start(mahjongTournamentRankingShower.returnExecutablePath());
        }

        #endregion

        #region ITournamentManagerForm

        public void FillDGVWithTeams(List<DBTeam> teams)
        {
            SortableBindingList<DBTeam> sortableTeams =
                new SortableBindingList<DBTeam>(teams);
            dgv.DataSource = sortableTeams;

            //Visible
            dgv.Columns[COLUMN_TEAMS_TOURNAMENT_ID].Visible = false;
            //ReadOnly
            dgv.Columns[COLUMN_TEAMS_ID].ReadOnly = true;
            //HeaderText
            dgv.Columns[COLUMN_TEAMS_ID].HeaderText = "Id";
            dgv.Columns[COLUMN_TEAMS_NAME].HeaderText = "Name";
            //AutoSizeMode
            dgv.Columns[COLUMN_TEAMS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_TEAMS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void FillDGVWithPlayers(List<DBPlayer> players, bool isTeams)
        {
            SortableBindingList<DBPlayer> sortablePlayers =
                new SortableBindingList<DBPlayer>(players);
            dgv.DataSource = sortablePlayers;

            //Visible
            dgv.Columns[COLUMN_PLAYERS_TOURNAMENT_ID].Visible = false;
            //ReadOnly
            dgv.Columns[COLUMN_PLAYERS_ID].ReadOnly = true;
            //HeaderText
            dgv.Columns[COLUMN_PLAYERS_ID].HeaderText = "Id";
            dgv.Columns[COLUMN_PLAYERS_NAME].HeaderText = "Name";
            dgv.Columns[COLUMN_PLAYERS_COUNTRY].HeaderText = "Country";
            //AutoSizeMode
            dgv.Columns[COLUMN_PLAYERS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_PLAYERS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[COLUMN_PLAYERS_COUNTRY].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (isTeams)
            {                
                dgv.Columns[COLUMN_PLAYERS_TEAM].HeaderText = "Team";
                dgv.Columns[COLUMN_PLAYERS_TEAM].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
                dgv.Columns[COLUMN_PLAYERS_TEAM].Visible = false;
        }

        public void AddRoundsSubButtons(int numRounds)
        {
            //Número de botones en horizontal
            int numButtonsHorizontal = (splitContainer1.Width - (MARGIN_SIZE * 3)) / (BUTTON_SIDE + MARGIN_SIZE);

            Point buttonStartPoint = new Point(MARGIN_SIZE, MARGIN_SIZE);
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
                btnRound.Image = Properties.Resources.gong;
                btnRound.Location = buttonStartPoint;
                btnRound.Click += delegate
                {
                    _presenter.ButtonRoundClicked((int)btnRound.Tag);
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
            splitContainer1.SplitterDistance = ((rowsCount + 1) * BUTTON_SIDE) + ((rowsCount + 2) * MARGIN_SIZE) + 2;
        }

        public void AddRoundTablesButtons(int roundId, int numTables)
        {
            //Número de botones en horizontal
            int numButtonsHorizontal = splitContainer1.Width / (TABLE_BUTTON_SIDE + TABLE_MARGIN_SIZE);

            //Generamos los botones
            int rowsCount = 0;
            Point buttonStartPoint = new Point(TABLE_MARGIN_SIZE, TABLE_MARGIN_SIZE);
            for (int i = 1; i <= numTables; i++)
            {
                Button button = GetNewButton();
                button.Tag = i;
                button.Text = string.Format("\n{0}", i);
                button.Image = Properties.Resources.table32;
                button.Width = TABLE_BUTTON_SIDE;
                button.Height = TABLE_BUTTON_SIDE;
                button.Location = buttonStartPoint;
                button.Click += delegate
                {
                    _presenter.ButtonRoundTableClicked((int)button.Tag);
                };

                splitContainer1.Panel2.Controls.Add(button);

                if (i < numTables)
                {
                    int numButtonsInActualRow = i - (rowsCount * numButtonsHorizontal);
                    if (numButtonsInActualRow == numButtonsHorizontal)
                    {
                        buttonStartPoint.Y += TABLE_BUTTON_SIDE + TABLE_MARGIN_SIZE;
                        buttonStartPoint.X = TABLE_MARGIN_SIZE;
                        rowsCount++;
                    }
                    else
                        buttonStartPoint.X += TABLE_BUTTON_SIDE + TABLE_MARGIN_SIZE;
                }
                else
                    return;
            }
        }

        public void GoToTableManager(int tournamentId, int roundId, int tableId)
        {
            new TableManagerForm(tournamentId, roundId, tableId).ShowDialog();
        }

        public void SelectTeamsButton()
        {
            btnTeams.BackColor = GREEN_MM;
            btnTeams.FlatAppearance.BorderSize = 1;
        }

        public void UnselectTeamsButton()
        {
            btnTeams.FlatAppearance.BorderSize = 0;
            btnTeams.BackColor = SystemColors.Control;
        }

        public void SelectPlayersButton()
        {
            btnPlayers.BackColor = GREEN_MM;
            btnPlayers.FlatAppearance.BorderSize = 1;
        }

        public void UnselectPlayersButton()
        {
            btnPlayers.FlatAppearance.BorderSize = 0;
            btnPlayers.BackColor = SystemColors.Control;
        }

        public void SelectRoundsButton()
        {
            btnRounds.BackColor = GREEN_MM;
            btnRounds.FlatAppearance.BorderSize = 1;
        }

        public void UnselectRoundsButton()
        {
            btnRounds.FlatAppearance.BorderSize = 0;
            btnRounds.BackColor = SystemColors.Control;
        }

        public void SelectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                {
                    control.BackColor = GREEN_MM;
                    ((Button)control).FlatAppearance.BorderSize = 1;
                }
            }
        }

        public void UnselectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                {
                    ((Button)control).FlatAppearance.BorderSize = 0;
                    control.BackColor = SystemColors.Control;
                }
            }
        }

        public void SelectRoundTableButton(int tableId)
        {
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                {
                    control.BackColor = GREEN_MM;
                    ((Button)control).FlatAppearance.BorderSize = 1;
                }
            }
        }

        public void UnselectTableButton(int tableId)
        {
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                {
                    ((Button)control).FlatAppearance.BorderSize = 0;
                    control.BackColor = SystemColors.Control;
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

        public void ShowDGV()
        {
            dgv.Visible = true;
        }

        public void HideDGV()
        {
            dgv.Visible = false;
        }

        public void EmptyPanelRoundsButtons()
        {
            List<Control> panelTournamentControls = new List<Control>();
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if(control.GetType() == typeof(Button))
                    panelTournamentControls.Add(control);
            }
            foreach (Control control in panelTournamentControls)
                splitContainer1.Panel1.Controls.Remove(control);
        }

        public void EmptyPanelRoundTablesButtons()
        {
            List<Control> panelTournamentControls = new List<Control>();
            foreach (Control control in splitContainer1.Panel2.Controls)
                panelTournamentControls.Add(control);
            foreach (Control control in panelTournamentControls)
                splitContainer1.Panel2.Controls.Remove(control);
        }

        public void ShowRoundsButtonsAndTablesPanel()
        {
            splitContainer1.Visible = true;
        }

        public void HideRoundsButtonsAndTablesPanel()
        {
            splitContainer1.Visible = false;
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
            newButton.FlatAppearance.BorderColor = SystemColors.ControlText;
            newButton.FlatAppearance.MouseDownBackColor = Color.LightGray;
            newButton.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            newButton.BackColor = Color.Transparent;
            newButton.BackgroundImageLayout = ImageLayout.None;
            newButton.Cursor = Cursors.Hand;
            newButton.ForeColor = SystemColors.ControlText;
            newButton.Margin = new Padding(5, 0, 5, 0);
            newButton.Padding = new Padding(0, 3, 0, 0);
            newButton.ImageAlign = ContentAlignment.TopCenter;
            newButton.TextImageRelation = TextImageRelation.ImageAboveText;
            newButton.UseVisualStyleBackColor = false;
            return newButton;
        }

        #endregion
    }
}
