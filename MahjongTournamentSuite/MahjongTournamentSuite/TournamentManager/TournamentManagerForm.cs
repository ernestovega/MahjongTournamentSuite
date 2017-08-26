using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;
using System.Drawing;
using MahjongTournamentSuite.Home;
using MahjongTournamentSuite.TableManager;
using MahjongTournamentSuite.CountrySelector;
using MahjongTournamentSuite.TeamSelector;
using MahjongTournamentSuite.Resources;

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
            ShowWaitCursor();
            _tournamentId = tournamentId;
            _presenter = Injector.provideTournamentManagerPresenter(this);
            _presenter.LoadTournament(_tournamentId);
            ShowDefaultCursor();
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

        private void btnTimer_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
            ShowDefaultCursor();
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            //var mahjongTournamentRankingShower = new MahjongTournamentRankingShower.Program();
            //Process.Start(mahjongTournamentRankingShower.returnExecutablePath());
            ShowDefaultCursor();
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgv.CurrentCell.ReadOnly ?
                    CustomColors.GREEN_MM_DARKEST :
                    CustomColors.GREEN_MM_DARKER;
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_NAME)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_TEAMS_NAME))
                    dgv.BeginEdit(true);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_TEAM_NAME))
                {
                    using (var teamSelectorForm = new TeamSelectorForm(_tournamentId))
                    {
                        if (teamSelectorForm.ShowDialog() == DialogResult.OK)
                        {
                            int playerId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_ID].Value;
                            int teamId = _presenter.SaveNewPlayerTeam(playerId, teamSelectorForm.ReturnValue);
                            if (teamId > 0)
                            {
                                dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_TEAM].Value = teamId;
                                dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_TEAM_NAME].Value = teamSelectorForm.ReturnValue;
                            }
                        }
                    }
                }
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_COUNTRY_NAME))
                {
                    using (var countrySelectorForm = new CountrySelectorForm())
                    {
                        if (countrySelectorForm.ShowDialog() == DialogResult.OK)
                        {
                            int playerId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_ID].Value;
                            int countryId = _presenter.SaveNewPlayerCountry(playerId, countrySelectorForm.ReturnValue);
                            if (countryId > 0)
                            {
                                dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_COUNTRY].Value = countryId;
                                dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_COUNTRY_NAME].Value = countrySelectorForm.ReturnValue;
                            }
                        }
                    }
                }
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_TEAMS_NAME))
                {
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        int teamId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_TEAMS_ID].Value;
                        _presenter.TeamNameChanged(teamId, newValue);
                    }
                    else
                        DGVCancelEdit();
                }
                else if(dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_NAME))
                {
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    {
                        int playerId = (int)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYERS_ID].Value;
                        _presenter.PlayerNameChanged(playerId, newValue);
                    }
                    else
                        DGVCancelEdit();
                }
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYERS_TEAM_NAME))
                _presenter.PlayerTeamChanged();
        }

        #endregion

        #region ITournamentManagerForm

        public void FillDGVWithTeams(List<DBTeam> teams)
        {
            SortableBindingList<DBTeam> sortableTeams = new SortableBindingList<DBTeam>(teams);
            if(dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortableTeams;

            //Visible
            dgv.Columns[COLUMN_TEAMS_TOURNAMENT_ID].Visible = false;
            //ReadOnly
            dgv.Columns[COLUMN_TEAMS_ID].ReadOnly = true;
            //Readonly columns BackColor
            dgv.Columns[COLUMN_TEAMS_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgv.Columns[COLUMN_TEAMS_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[COLUMN_TEAMS_ID].HeaderText = "Team Id";
            dgv.Columns[COLUMN_TEAMS_NAME].HeaderText = "Team Name";
            //AutoSizeMode
            dgv.Columns[COLUMN_TEAMS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_TEAMS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void FillDGVWithPlayers(List<DGVPlayer> players, bool isTeams)
        {
            SortableBindingList<DGVPlayer> sortablePlayers = new SortableBindingList<DGVPlayer>(players);
            if(dgv.DataSource != null)
                dgv.DataSource = null;
            dgv.DataSource = sortablePlayers;

            //Visible
            dgv.Columns[COLUMN_PLAYERS_TOURNAMENT_ID].Visible = false;
            dgv.Columns[COLUMN_PLAYERS_TEAM].Visible = false;
            dgv.Columns[COLUMN_PLAYERS_COUNTRY].Visible = false;
            //ReadOnly
            dgv.Columns[COLUMN_PLAYERS_ID].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].ReadOnly = true;
            //Readonly Columns BackColor
            dgv.Columns[COLUMN_PLAYERS_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly Columns ForeColor
            dgv.Columns[COLUMN_PLAYERS_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[COLUMN_PLAYERS_ID].HeaderText = "Player Id";
            dgv.Columns[COLUMN_PLAYERS_NAME].HeaderText = "Player Name";
            dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].HeaderText = "Player Country";
            //AutoSizeMode
            dgv.Columns[COLUMN_PLAYERS_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_PLAYERS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[COLUMN_PLAYERS_COUNTRY_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //DisplayIndex
            dgv.Columns[COLUMN_PLAYERS_ID].DisplayIndex = 0;
            dgv.Columns[COLUMN_PLAYERS_NAME].DisplayIndex = 1;
            if (isTeams)
            {
                dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].ReadOnly = true;
                dgv.Columns[COLUMN_PLAYERS_TEAM_NAME].HeaderText = "Player Team";
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

        public void MarkWrongTeamsPlayers(List<WrongTeam> wrongTeams)
        {
            if (wrongTeams.Count > 0)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    int currentRowTeamId = (int)row.Cells[COLUMN_PLAYERS_TEAM].Value;
                    WrongTeam wrongTeam = wrongTeams.Find(x => x.TeamId == currentRowTeamId);
                    if(wrongTeam != null)
                    {
                        row.Cells[COLUMN_PLAYERS_TEAM_NAME].ErrorText =
                            string.Format("{0} players in this team", wrongTeam.NumPlayers);
                    }
                }
            }
        }

        public void CleanWrongTeamsPlayers()
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[COLUMN_PLAYERS_TEAM_NAME].ErrorText = string.Empty;
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

        public void AddRoundTablesButtons(int roundId, int numTables)
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
                button.Image = Properties.Resources.table;
                button.Width = BUTTON_SIDE;
                button.Height = BUTTON_SIDE;
                button.Location = buttonStartPoint;
                button.Click += delegate
                {
                    ShowWaitCursor();
                    _presenter.ButtonRoundTableClicked((int)button.Tag);
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

        public void GoToTableManager(int roundId, int tableId)
        {
            new TableManagerForm(_tournamentId, roundId, tableId).ShowDialog();
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

        public void ShowWaitCursor()
        {
            Cursor = Cursors.WaitCursor;
        }

        public void ShowDefaultCursor()
        {
            Cursor = Cursors.Default;
        }

        public void ShowDGV()
        {
            dgv.Visible = true;
        }

        public void HideDGV()
        {
            dgv.Visible = false;
        }

        public void ShowButtonPlayersBorder()
        {
            btnPlayers.FlatAppearance.BorderSize = 1;
        }

        public void HideButtonPlayersBorder()
        {
            btnPlayers.FlatAppearance.BorderSize = 0;
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

        public void ShowWrongNumberOfPlayersPerTeamMessage(List<WrongTeam> wrongTeams)
        {
            string message = string.Empty;
            foreach (WrongTeam wrongTeam in wrongTeams)
            {
                if (message != string.Empty)
                    message += "\n";
                message = string.Format("{0}The team \"{1}\" have {2} players.",
                    message, wrongTeam.TeamName, wrongTeam.NumPlayers);
            }
            string caption = "Wrong team number of players";
            if(wrongTeams.Count > 1)
                caption = "Wrong teams number of players";
            MessageBox.Show(message, caption);
        }

        public void ShowMessageTeamNameInUse(string usedName, int ownerTeamId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the team {1}", usedName, ownerTeamId), "Name in use");
        }

        public void ShowMessagePlayerNameInUse(string usedName, int ownerPlayerId)
        {
            MessageBox.Show(string.Format("\"{0}\" is in use by the player {1}", usedName, ownerPlayerId), "Name in use");
        }

        public void ShowMessageCountryError()
        {
            MessageBox.Show("Something went wrong with the country selection.", "Ups!");
        }

        public void ShowMessageTeamError()
        {
            MessageBox.Show("Something went wrong with the team selection.", "Ups!");
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
            newButton.FlatAppearance.MouseDownBackColor = CustomColors.GREEN_MM_DARK;
            newButton.FlatAppearance.MouseOverBackColor = CustomColors.GREEN_MM;
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
            button.BackColor = CustomColors.GREEN_MM;
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
