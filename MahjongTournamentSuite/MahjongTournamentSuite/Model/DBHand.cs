using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    public class DBHand
    {
        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int RoundId { get; set; }

        [Key, Column(Order = 2)]
        public int TableId { get; set; }

        [Key, Column(Order = 3)]
        public int Id { get; set; }
        
        public int PlayerWinnerId { get; set; }

        public int PlayerLooserId { get; set; }

        public int Score { get; set; }

        public bool IsChickenHand { get; set; }

        public DBHand() {}

        public DBHand(int tournamentId, int roundId, int tableId, int id)
        {
            TournamentId = tournamentId;
            RoundId = roundId;
            TableId = tableId;
            Id = id;
        }
    }
}
