using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    class DBTable
    {
        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int RoundId { get; set; }

        [Key, Column(Order = 2)]
        public int Id { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }

        public int Player3Id { get; set; }

        public int Player4Id { get; set; }

        public DBTable() {}

        public DBTable(int id, int tournamentId, int roundId, int player1Id, int player2Id, int player3Id, int player4Id)
        {
            Id = id;
            TournamentId = tournamentId;
            RoundId = roundId;
            Player1Id = player1Id;
            Player2Id = player2Id;
            Player3Id = player3Id;
            Player4Id = player4Id;
        }
    }
}
