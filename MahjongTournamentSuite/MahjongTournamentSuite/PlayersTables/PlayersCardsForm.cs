using MahjongTournamentSuite.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.PlayersTables
{
    public partial class PlayersCardsForm : Form
    {
        private List<DGVPlayerCard> _playersCards;

        public PlayersCardsForm(List<DGVPlayerCard> playersCards)
        {
            InitializeComponent();
            _playersCards = playersCards;
        }

        private void PlayersCardsForm_Load(object sender, EventArgs e)
        {
            dgvPlayersCards.DataSource = _playersCards;

            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_NUMBER].HeaderText = "Number";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_NAME].HeaderText = "Name";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_NAME].HeaderText = "Last name";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_NAME].HeaderText = "Team";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_NAME].HeaderText = "Country";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_1].HeaderText = "Table 1";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_2].HeaderText = "Table 2";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_3].HeaderText = "Table 3";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_4].HeaderText = "Table 4";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_5].HeaderText = "Table 5";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_6].HeaderText = "Table 6";
            dgvPlayersCards.Columns[DGVPlayerCard.COLUMN_PLAYER_TABLE_7].HeaderText = "Table 7";
        }
    }
}
