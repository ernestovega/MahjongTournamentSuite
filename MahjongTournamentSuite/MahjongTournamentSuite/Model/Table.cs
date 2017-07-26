using System.Collections.Generic;

namespace MahjongTournamentSuite.Model
{
    class Table
    {
        public int RoundId { get; set; }

        public int TableId { get; set; }

        public List<Player> Players { get; set; }

        public Table() { }

        public Table(int roundId, int tableId, List<Player> players)
        {
            RoundId = roundId;
            TableId = tableId;
            Players = players;
        }
    }
}
