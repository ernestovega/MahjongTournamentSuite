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

        private static readonly string COLUMN_TEAMS_TOURNAMENT_ID = "TournamentId";
        private static readonly string COLUMN_TEAMS_ID = "Id";
        private static readonly string COLUMN_TEAMS_NAME = "Name";
        private static readonly int COLUMN_TEAMS_NAME_INDEX = 2;

        private static readonly string COLUMN_PLAYERS_TOURNAMENT_ID = "TournamentId";
        private static readonly string COLUMN_PLAYERS_ID = "Id";
        private static readonly string COLUMN_PLAYERS_NAME = "Name";
        private static readonly string COLUMN_PLAYERS_TEAM = "TeamId";
        private static readonly string COLUMN_PLAYERS_COUNTRY = "CountryId";
        private static readonly int COLUMN_PLAYERS_NAME_INDEX = 2;
        private static readonly int COLUMN_PLAYERS_TEAM_INDEX = 3;
        private static readonly int COLUMN_PLAYERS_COUNTRY_INDEX = 4;

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

        public void AddButtonTeams()
        {
            Button btnTeams = new Button();
            btnTeams.Name = "Teams";
            btnTeams.Text = "Teams";
            btnTeams.AutoSize = true;
            btnTeams.Click += delegate
            {
                _presenter.ButtonTeamsClicked();
            };
            flowPanelRoundsButtons.Controls.Add(btnTeams);
        }

        public void AddPlayersButton()
        {
            Button btnPlayers = new Button();
            btnPlayers.Name = "Players";
            btnPlayers.Text = "Players";
            btnPlayers.AutoSize = true;
            btnPlayers.Click += delegate
            {
                _presenter.ButtonPlayersClicked();
            };
            flowPanelRoundsButtons.Controls.Add(btnPlayers);
        }

        public void AddRoundsButtons(int numRounds)
        {
            for (int i = 1; i <= numRounds; i++)
            {
                Button btnRound = new Button();
                btnRound.Name = string.Format("Round {0}", i);
                btnRound.Text = string.Format("Round {0}", i);
                btnRound.AutoSize = false;
                btnRound.Width = flowPanelRoundsButtons.Height;
                btnRound.Height = flowPanelRoundsButtons.Height;
                btnRound.Tag = i;
                btnRound.Anchor = AnchorStyles.None;
                btnRound.Click += delegate
                {
                    _presenter.ButtonRoundClicked((int)btnRound.Tag);
                };
                flowPanelRoundsButtons.Controls.Add(btnRound);
            }
        }

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
        }

        public void FillDGVWithPlayers(List<DBPlayer> players)
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
            dgv.Columns[COLUMN_PLAYERS_TEAM].HeaderText = "Team";
            dgv.Columns[COLUMN_PLAYERS_COUNTRY].HeaderText = "Country";
            //AutoSizeMode
            dgv.Columns[COLUMN_PLAYERS_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public void FillPanelTournamentWithRoundButtons(int roundId, int numTables)
        {
            //Obtenemos el número de botones por lado.
            int numButtonsHorizontal = 2;
            int numButtonsVertical = 2;
            while (numButtonsHorizontal * numButtonsVertical < numTables)
            {
                if (numButtonsHorizontal == numButtonsVertical)
                    numButtonsHorizontal++;
                else
                    numButtonsVertical++;
            }

            //Calculamos los márgenes
            int marginVertical = panelRoundButtons.Height / (numButtonsVertical * 10); //Un 10% del lado del botón
            int marginHorizontal = marginVertical;

            int horizontalMarginsSum = marginHorizontal * (numButtonsHorizontal - 1);
            int verticalMarginsSum = marginVertical * (numButtonsVertical - 1);

            //Obtenemos el tamaño de los panelTournament de los botones teniendo en cuenta los márgenes entre cada uno.
            int buttonSideVertical = (panelRoundButtons.Height - verticalMarginsSum) / numButtonsVertical;
            int buttonSideHorizontal = buttonSideVertical;

            //Creamos un panel nuevo
            Panel panelButtons = new Panel();
            panelButtons.Name = "panelButtons";
            panelButtons.Width = (buttonSideHorizontal * numButtonsHorizontal) + (marginHorizontal * numButtonsHorizontal);
            panelButtons.Height = panelRoundButtons.Height;
            panelButtons.AutoSize = false;
            panelButtons.Location = new Point((panelRoundButtons.Width - panelButtons.Width) / 2, 0);

            //Generamos los botones
            int tableId = 1;
            Point buttonStartPoint = new Point(0, 0);
            for (int i = 1; i <= numButtonsVertical; i++)
            {
                for (int j = 1; j <= numButtonsHorizontal; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSideHorizontal, buttonSideVertical);
                    button.Name = string.Format("btnRound{1}Table{0}", roundId, tableId);
                    button.Text = string.Format("TABLE {0}", tableId);
                    button.Tag = tableId;
                    button.Location = buttonStartPoint;
                    button.BackColor = Color.Green;
                    button.ForeColor = Color.White;
                    button.Font = new Font("Arial Black", 12);
                    //button.Cursor = ;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatAppearance.BorderSize = 0;
                    //button.FlatAppearance.CheckedBackColor =;
                    button.FlatAppearance.MouseDownBackColor = Color.ForestGreen;
                    //button.FlatAppearance.MouseOverBackColor = ;
                    button.Click += delegate
                    {
                        new TableManagerForm(_tournamentId, roundId, (int)button.Tag).ShowDialog();
                    };

                    panelButtons.Controls.Add(button);

                    if (tableId < numTables)
                    {
                        tableId++;
                        buttonStartPoint.X += buttonSideHorizontal + marginHorizontal;
                    }
                    else
                    {
                        panelRoundButtons.Controls.Add(panelButtons);
                        return;
                    }
                }
                buttonStartPoint.X = 0;
                buttonStartPoint.Y += marginVertical + buttonSideVertical;
                panelRoundButtons.Controls.Add(panelButtons);
            }
        }

        public void ShowDGV()
        {
            dgv.Visible = true;
        }

        public void HideDGV()
        {
            dgv.Visible = false;
        }

        public void EmptyPanelRoundButtons()
        {
            List<Control> panelTournamentControls = new List<Control>();
            foreach (Control control in panelRoundButtons.Controls)
            {
                if (control.GetType() != typeof(DataGridView))
                    panelRoundButtons.Controls.Remove(control);
            }
        }

        public void ShowRoundsButtonsAndPanel()
        {
            flowPanelRoundsButtons.Visible = true;
            panelRoundButtons.Visible = true;
        }

        public void HideRoundsButtonsAndPanel()
        {
            flowPanelRoundsButtons.Visible = false;
            panelRoundButtons.Visible = false;
        }

        #endregion
    }
}
