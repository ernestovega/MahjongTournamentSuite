using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MahjongTournamentSuite.Model;
using System.Drawing;

namespace MahjongTournamentSuite.TournamentManager
{
    public partial class TournamentManagerForm : Form, ITournamentManagerForm
    {
        #region Fields

        private ITournamentManagerPresenter _presenter;
        private bool isLoaded = false;
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

        #endregion

        #region Events

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
            List<ComboRoundsItem> comboRoundItems = new List<ComboRoundsItem>(numRounds);
            for (int round = 1; round <= numRounds; round++)
            {
                comboRoundItems.Add(new ComboRoundsItem(string.Format("Round {0}", round), round.ToString()));
            }
            comboRounds.DataSource = comboRoundItems;
            comboRounds.DisplayMember = "Text";
            comboRounds.ValueMember = "Value";
        }

        #endregion

        #region Private

        public void GenerateRoundTablesButtons(int numTables)
        {
            //Obtenemos el número de botones por lado.
            int numTablesHorizontal = 2;
            int numTablesVertical = 2;
            while(numTablesHorizontal * numTablesVertical < numTables)
            {
                if (numTablesHorizontal == numTablesVertical)
                    numTablesHorizontal++;
                else
                    numTablesVertical++;
            }

            InitTableLayoutPanel(panelTables, numTablesHorizontal, numTablesVertical);

            int buttonSideHorizontal = 20;
            int buttonSideVertical = 20;

            string roundId = (string)comboRounds.SelectedValue;
            int count = 1;
            for (int i = 0; i < numTablesVertical; i++)
            {
                for (int j = 0; j < numTablesHorizontal; j++)
                {
                    var button = new Button();
                    button.Size = new Size(buttonSideHorizontal, buttonSideVertical);
                    button.Name = string.Format("btnRound{0}Table{1}", count, roundId);
                    button.Text = string.Format("TABLE {0}", count);
                    panelTables.Controls.Add(button, i, j);
                    count++;
                }
            }



            //Obtenemos los lados de los botones en base al tamaño del contenedor
            //int panelWidth = panelTables.Size.Width;
            //int panelHeight = panelTables.Size.Height;
            //int buttonSideHorizontal = panelWidth / numTablesHorizontal;
            //int buttonSideVertical = panelHeight / numTablesVertical;

            //int initialOffset = 0;
            //int offsetHorizontal = initialOffset;
            //int offsetVertical = initialOffset;

            //string roundId = (string)comboRounds.SelectedValue;

            //for (int i = 1; i <= numTables; i++)
            //{
            //    if ((offsetHorizontal + buttonSideHorizontal) >= panelWidth)
            //    {
            //        offsetHorizontal = initialOffset;
            //        offsetVertical = offsetVertical + buttonSideVertical;
            //    }
            //    var button = new Button();
            //    button.Size = new Size(buttonSideHorizontal, buttonSideVertical);
            //    button.Name = string.Format("btnRound{0}Table{1}", i, roundId);
            //    button.Text = string.Format("TABLE {0}", i);
            //    button.Location = new Point(offsetHorizontal, offsetVertical);
            //    button.Click += delegate
            //    {
            //        MessageBox.Show(string.Format("TABLE {0}", i));
            //    };
            //    panelTables.Controls.Add(button);
            //    offsetHorizontal = offsetHorizontal + buttonSideHorizontal;
            //}
        }

        private void InitTableLayoutPanel(TableLayoutPanel TLP, int rows, int cols)
        {
            TLP.RowCount = rows;
            TLP.RowStyles.Clear();
            for (int i = 1; i <= rows; i++)
            {
                TLP.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
            }
            TLP.ColumnCount = cols;
            TLP.ColumnStyles.Clear();
            for (int i = 1; i <= cols; i++)
            {
                TLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            }
        }

        #endregion
    }
}
