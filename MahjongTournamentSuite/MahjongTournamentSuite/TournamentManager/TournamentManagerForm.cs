using System;
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
        private const int TABLES_MARGIN_SIZE = 10;
        private const int TITLE_ROUNDS_HEIGHT = 23;
        private const int TITLE_TABLES_HEIGHT = 38;
        private const int NUM_TABLES_BUTTONS_HORIZONTAL = 10;
        private const int SEPARATOR_EXTRA_MARGIN_BOTTOM = 12; 

        private static readonly Color GREEN_MM = Color.FromArgb(0, 177, 106);
        private static readonly Color GREEN_MM_DARK = Color.FromArgb(0, 147, 76);

        private const string COLUMN_TEAMS_TOURNAMENT_ID = "TeamTournamentId";
        private const string COLUMN_TEAMS_ID = "TeamId";
        private const string COLUMN_TEAMS_NAME = "TeamName";

        private const string COLUMN_PLAYERS_TOURNAMENT_ID = "PlayerTournamentId";
        private const string COLUMN_PLAYERS_ID = "PlayerId";
        private const string COLUMN_PLAYERS_NAME = "PlayerName";
        private const string COLUMN_PLAYERS_TEAM = "PlayerTeamId";
        private const string COLUMN_PLAYERS_COUNTRY = "PlayerCountryId";
        private const string COLUMN_PLAYERS_TEAM_NAME = "PlayerTeamName";
        private const string COLUMN_PLAYERS_COUNTRY_NAME = "PlayerCountryName";

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

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_TEAMS_NAME)
                || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_NAME)))
                dgv.BeginEdit(true);
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_TEAMS_NAME))
                {
                    int tournamentId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_TEAMS_TOURNAMENT_ID].Value;
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        int teamId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_TEAMS_ID].Value;
                        _presenter.TeamNameChanged(tournamentId, teamId, newValue);
                    }
                    else
                        DGVCancelEdit();
                }
                else if(dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_NAME))
                {
                    int tournamentId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_TOURNAMENT_ID].Value;
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        int playerId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_ID].Value;
                        _presenter.PlayerNameChanged(tournamentId, playerId, newValue);
                    }
                    else
                        DGVCancelEdit();
                }
            }
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

        public void FillDGVWithPlayers(List<DGVPlayer> players, bool isTeams)
        {
            SortableBindingList<DGVPlayer> sortablePlayers =
                new SortableBindingList<DGVPlayer>(players);
            dgv.DataSource = sortablePlayers;

            //Visible
            dgv.Columns[COLUMN_PLAYERS_TOURNAMENT_ID].Visible = false;
            dgv.Columns[COLUMN_PLAYERS_TEAM].Visible = false;
            dgv.Columns[COLUMN_PLAYERS_COUNTRY].Visible = false;
            //ReadOnly
            dgv.Columns[COLUMN_PLAYERS_ID].ReadOnly = true;
            //HeaderText
            dgv.Columns[COLUMN_PLAYERS_ID].HeaderText = "Id";
            dgv.Columns[COLUMN_PLAYERS_NAME].HeaderText = "Name";
            dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].HeaderText = "Country";
            //AutoSizeMode
            dgv.Columns[COLUMN_PLAYERS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_PLAYERS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //DisplayIndex
            dgv.Columns[COLUMN_PLAYERS_ID].DisplayIndex = 0;
            dgv.Columns[COLUMN_PLAYERS_NAME].DisplayIndex = 1;
            if (isTeams)
            {
                dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].HeaderText = "Team";
                dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].DisplayIndex = 2;
                dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].DisplayIndex = 3;
            }
            else
            {
                dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].Visible = false;
                dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].DisplayIndex = 2;
            }
        }

        public void AddRoundsSubButtons(int numRounds)
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
            //Fijamos el tamaño vertical del contenedor para evitar el scroll
            splitContainer1.SplitterDistance = ((rowsCount + 1) * BUTTON_SIDE) + ((rowsCount + 2) * MARGIN_SIZE) 
                + SEPARATOR_EXTRA_MARGIN_BOTTOM + TITLE_ROUNDS_HEIGHT;
        }

        public void AddRoundTablesButtons(int roundId, int numTables)
        {
            //Calculamos el punto de comienzo para centrar los botones
            int neededWidth = ((NUM_TABLES_BUTTONS_HORIZONTAL * BUTTON_SIDE) + ((NUM_TABLES_BUTTONS_HORIZONTAL + 1) * TABLES_MARGIN_SIZE));
            int initPoint = (splitContainer1.Width - neededWidth) / 2;
            Point buttonStartPoint = new Point(initPoint, TITLE_TABLES_HEIGHT + TABLES_MARGIN_SIZE);

            int rowsCount = 0;
            for (int i = 1; i <= numTables; i++)
            {
                Button button = GetNewButton();
                button.Tag = i;
                button.Text = string.Format("\n{0}", i);
                button.Image = Properties.Resources.table;
                button.Width = BUTTON_SIDE;
                button.Height = BUTTON_SIDE;
                button.Location = buttonStartPoint;
                button.Click += delegate
                {
                    _presenter.ButtonRoundTableClicked((int)button.Tag);
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

        public void GoToTableManager(int tournamentId, int roundId, int tableId)
        {
            new TableManagerForm(tournamentId, roundId, tableId).ShowDialog();
        }

        public void SelectTeamsButton()
        {
            MakeButtonSelected(btnTeams, Properties.Resources.teams_white);
        }

        public void UnselectTeamsButton()
        {
            MakeButtonUnselected(btnTeams, Properties.Resources.teams);
        }

        public void SelectPlayersButton()
        {
            MakeButtonSelected(btnPlayers, Properties.Resources.players_white);
        }

        public void UnselectPlayersButton()
        {
            MakeButtonUnselected(btnPlayers, Properties.Resources.players);
        }

        public void SelectRoundsButton()
        {
            MakeButtonSelected(btnRounds, Properties.Resources.gong_big_white);
        }

        public void UnselectRoundsButton()
        {
            MakeButtonUnselected(btnRounds, Properties.Resources.gong_big);
        }

        public void SelectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                    MakeButtonSelected((Button)control, Properties.Resources.gong_white);
            }
        }

        public void UnselectRoundButton(int roundId)
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.Tag != null && (int)control.Tag == roundId)
                    MakeButtonUnselected((Button)control, Properties.Resources.gong);
            }
        }

        public void SelectRoundTableButton(int tableId)
        {
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                if (control.Tag != null && (int)control.Tag == tableId)
                    MakeButtonSelected((Button)control, Properties.Resources.table_white);
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

        public void ShowButtonPlayers()
        {
            btnPlayers.Visible = true;
        }

        public void HideButtonPlayers()
        {
            btnPlayers.Visible = false;
        }

        public void ShowButtonRounds()
        {
            btnRounds.Visible = true;
        }

        public void HideButtonRounds()
        {
            btnRounds.Visible = false;
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
            {
                if (control.GetType() == typeof(Button))
                    panelTournamentControls.Add(control);
            }
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

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        public void ShowMessageTeamNameInUse(string usedName, int ownerTeamId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the team {1}", usedName, ownerTeamId), "Name in use");
        }

        public void ShowMessagePlayerNameInUse(string usedName, int ownerPlayerId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the player {1}", usedName, ownerPlayerId), "Name in use");
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
            newButton.FlatAppearance.MouseDownBackColor = GREEN_MM_DARK;
            newButton.FlatAppearance.MouseOverBackColor = GREEN_MM;
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
            button.BackColor = GREEN_MM;
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
