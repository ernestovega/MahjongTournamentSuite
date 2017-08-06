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

        private readonly Color GREEN_MM = Color.FromArgb(0 , 177, 106);
        private readonly Color GREEN_DARK_MM = Color.FromArgb(64, 64, 64);
        private readonly Color GRAY_MM = Color.FromArgb(64, 64, 64);
        private readonly Color GRAY_DARK_MM = Color.FromArgb(64, 64, 64);
        private readonly Color RED_MM = Color.FromArgb(64, 64, 64);

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
            //Borramos el panel que haya
            var controls = Controls.Find("panelButtons", true);
            if (controls != null && controls.Length > 0)
            {
                Controls.Remove(controls[0]);
            }

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
            int marginVertical = panelTables.Height / (numButtonsVertical * 10); //Un 10% del lado del botón
            int marginHorizontal = marginVertical;

            int horizontalMarginsSum = marginHorizontal * (numButtonsHorizontal - 1);
            int verticalMarginsSum = marginVertical * (numButtonsVertical - 1);

            //Obtenemos el tamaño de los lados de los botones teniendo en cuenta los márgenes entre cada uno.
            int buttonSideVertical = (panelTables.Height - verticalMarginsSum) / numButtonsVertical;
            int buttonSideHorizontal = buttonSideVertical;

            //Creamos un panel nuevo
            Panel panelButtons = new Panel();
            panelButtons.Name = "panelButtons";
            panelButtons.Width = (buttonSideHorizontal * numButtonsHorizontal) + (marginHorizontal * numButtonsHorizontal);
            panelButtons.Height = panelTables.Height;
            panelButtons.AutoSize = false;
            panelButtons.Location = new Point((panelTables.Width - panelButtons.Width) / 2, 0);

            //Generamos los botones
            string roundId = (string)comboRounds.SelectedValue;
            int count = 1;
            Point buttonStartPoint = new Point(0, 0);
            for (int i = 1; i <= numButtonsVertical; i++)
            {
                for (int j = 1; j <= numButtonsHorizontal; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSideHorizontal, buttonSideVertical);
                    button.Name = string.Format("btnRound{1}Table{0}", roundId, count);
                    button.Text = string.Format("TABLE {0}", count);
                    button.Tag = count;
                    button.Location = buttonStartPoint;
                    button.BackColor = GREEN_MM;
                    button.ForeColor = Color.White;
                    button.Font = new Font("Arial Black", 12);
                    //button.Cursor = ;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatAppearance.BorderSize = 0;
                    //button.FlatAppearance.CheckedBackColor =;
                    button.FlatAppearance.MouseDownBackColor = GREEN_DARK_MM;
                    //button.FlatAppearance.MouseOverBackColor = ;
                    button.Click += delegate
                    {
                        new TableManagerForm(_tournamentId, int.Parse((string)comboRounds.SelectedValue), (int)button.Tag).Show();
                    };

                    panelButtons.Controls.Add(button);

                    if (count < numTables)
                    {
                        count++;
                        buttonStartPoint.X += buttonSideHorizontal + marginHorizontal;
                    }
                    else
                    {
                        panelTables.Controls.Add(panelButtons);
                        return;
                    }
                }
                buttonStartPoint.X = 0;
                buttonStartPoint.Y += marginVertical + buttonSideVertical;
                panelTables.Controls.Add(panelButtons);
            }
        }

        #endregion

        #region Private

        #endregion
    }
}
