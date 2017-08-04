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

        #endregion

        #region Events

        private void btnReturn_Click(object sender, EventArgs e)
        {
            new HomeForm().Show();
            Close();
        }

        private void btnTimer_Click(object sender, System.EventArgs e)
        {
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
        }

        private void btnRankingShower_Click(object sender, System.EventArgs e)
        {
            //var mahjongTournamentRankingShower = new MahjongTournamentRankingShower.Program();
            //Process.Start(mahjongTournamentRankingShower.returnExecutablePath());
        }

        #endregion

        #region ITournamentManagerForm

        public void FillComboRounds(int numRounds)
        {
            List<ComboItem> comboRoundItems = new List<ComboItem>(numRounds);
            for (int round = 1; round <= numRounds; round++)
            {
                comboRoundItems.Add(new ComboItem(string.Format("Round {0}", round), round.ToString()));
            }
            comboRounds.DataSource = comboRoundItems;
            comboRounds.DisplayMember = "Text";
            comboRounds.ValueMember = "Value";
        }

        public void GenerateRoundTablesButtons(int numTables)
        {
            //Obtenemos el número de botones por lado.
            int numButtonsHorizontal = 2;
            int numButtonsVertical = 2;
            while(numButtonsHorizontal * numButtonsVertical < numTables)
            {
                if (numButtonsHorizontal == numButtonsVertical)
                    numButtonsHorizontal++;
                else
                    numButtonsVertical++;
            }

            //Calculamos los márgenes
            int marginHorizontal = panelTables.Width / (numButtonsHorizontal * 10);
            int marginVertical = panelTables.Height / (numButtonsVertical * 10);

            int horizontalMarginsSum = marginHorizontal * (numButtonsHorizontal - 1);
            int verticalMarginsSum = marginVertical * (numButtonsVertical - 1);

            //Obtenemos el tamaño de los lados de los botones teniendo en cuenta los márgenes entre cada uno.
            int buttonSideHorizontal = (panelTables.Width - horizontalMarginsSum) / numButtonsHorizontal;
            int buttonSideVertical = (panelTables.Height - verticalMarginsSum) / numButtonsVertical;
            
            //Generamos los botones
            string roundId = (string)comboRounds.SelectedValue;
            int count = 1;
            Point buttonStartPoint = new Point(0, 0);
            for (int i = 0; i < numButtonsVertical; i++)
            {
                for (int j = 0; j < numButtonsHorizontal; j++)
                {
                    var button = new Button();
                    button.Size = new Size(buttonSideHorizontal, buttonSideVertical);
                    button.Name = string.Format("btnRound{0}Table{1}", count, roundId);
                    button.Text = string.Format("TABLE {0}", count);
                    button.Location = buttonStartPoint;
                    button.Click += delegate
                    {
                        new TableManagerForm(_tournamentId, int.Parse((string)comboRounds.SelectedValue), count).Show();
                    };

                    panelTables.Controls.Add(button);

                    if (count == numTables) return;
                    count++;
                    buttonStartPoint.X += buttonSideHorizontal + marginHorizontal;
                }
                buttonStartPoint.X = 0;
                buttonStartPoint.Y += marginVertical + buttonSideVertical;
            }
        }

        #endregion

        #region Private

        #endregion
    }
}
