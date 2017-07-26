using System.Collections.Generic;

namespace MahjongTournamentSuite.Model
{
    class Table
    {
        public int TournamentId { get; set; }

        public int TableId { get; set; }

        public List<Player> Players { get; set; }

        public Table() { }

        public Table(int tournamentId, int tableId, List<Player> players)
        {
            TournamentId = tournamentId;
            TableId = tableId;
            Players = players;
        }
    }
}
