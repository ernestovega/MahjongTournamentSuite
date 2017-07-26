namespace MahjongTournamentSuite.Model
{
    class TablePlayer
    {
        public int Round { get; set; }

        public int Table { get; set; }

        public int Player { get; set; }

        public int PlayerId { get; set; }

        public TablePlayer(int round, int table, int player, int playerId)
        {
            Round = round;
            Table = table;
            Player = player;
            PlayerId = playerId;
        }
    }
}
