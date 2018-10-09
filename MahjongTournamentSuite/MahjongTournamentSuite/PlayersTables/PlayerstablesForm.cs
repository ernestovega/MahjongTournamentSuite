using MahjongTournamentSuite.Resources;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.PlayersTables
{
    public partial class PlayersTablesForm : Form, IPlayersTablesForm
    {
        #region Constants

        private const int BUTTON_SIDE = 64;
        private const int BUTTONS_MARGIN_SIZE = 10;
        private const int NUM_BUTTONS_HORIZONTAL = 10;

        #endregion

        #region Fields

        private IPlayersTablesController _controller;
        private int _tournamentId;

        #endregion
        
        #region Contructor

        public PlayersTablesForm(int tournamentId)
        {
            InitializeComponent();
            _controller = Injector.providePlayersTablesController(this);
            _tournamentId = tournamentId;
        }

        #endregion

        #region Events

        private void PlayerstablesForm_Load(object sender, System.EventArgs e)
        {
            ShowWaitCursor();
            _controller.LoadForm(_tournamentId);
            ShowDefaultCursor();
        }

        #endregion

        #region IPlayersTablesForm implementation

        public void GeneratePlayersButtons(int numPlayers)
        {
            //Calculamos el punto de comienzo para centrar los botones
            int neededWidth;
            if (numPlayers < NUM_BUTTONS_HORIZONTAL)
                neededWidth = ((numPlayers * BUTTON_SIDE) + ((numPlayers + 1) * BUTTONS_MARGIN_SIZE));
            else
                neededWidth = ((NUM_BUTTONS_HORIZONTAL * BUTTON_SIDE) + ((NUM_BUTTONS_HORIZONTAL + 1) * BUTTONS_MARGIN_SIZE));

            int initPoint = (panel.Width - neededWidth) / 2;
            Point buttonStartPoint = new Point(initPoint, BUTTONS_MARGIN_SIZE);

            int rowsCount = 0;
            for (int i = 1; i <= numPlayers; i++)
            {
                Button button = GetNewButton();
                button.Tag = i;
                button.Text = string.Format("\n{0}", i);
                button.Width = BUTTON_SIDE;
                button.Height = BUTTON_SIDE;
                button.Location = buttonStartPoint;
                button.Image = Properties.Resources.players;
                button.Click += delegate
                {
                    ShowWaitCursor();
                    _controller.ButtonPlayerClicked((int)button.Tag);
                    ShowDefaultCursor();
                };

                panel.Controls.Add(button);

                if (i < numPlayers)
                {
                    int numButtonsInActualRow = i - (rowsCount * NUM_BUTTONS_HORIZONTAL);
                    if (numButtonsInActualRow == NUM_BUTTONS_HORIZONTAL)
                    {
                        buttonStartPoint.Y += BUTTON_SIDE + BUTTONS_MARGIN_SIZE;
                        buttonStartPoint.X = initPoint;
                        rowsCount++;
                    }
                    else
                        buttonStartPoint.X += BUTTON_SIDE + BUTTONS_MARGIN_SIZE;
                }
                else
                    return;
            }
        }

        public void ShowPlayerTables(PlayerTables playerTables)
        {
            new PlayerTablesForm(playerTables).ShowDialog();
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
