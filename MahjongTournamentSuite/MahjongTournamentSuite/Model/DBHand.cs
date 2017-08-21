using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    public class DBHand
    {
        [Key, Column(Order = 0)]
        public int HandTournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int HandRoundId { get; set; }

        [Key, Column(Order = 2)]
        public int HandTableId { get; set; }

        [Key, Column(Order = 3)]
        public int HandId { get; set; }
        
        public int PlayerWinnerId { get; set; }

        public int PlayerLooserId { get; set; }

        public int HandScore { get; set; }

        public bool IsChickenHand { get; set; }

        public DBHand() {}

        public DBHand(int tournamentId, int roundId, int tableId, int id)
        {
            HandTournamentId = tournamentId;
            HandRoundId = roundId;
            HandTableId = tableId;
            HandId = id;
        }
    }
}
