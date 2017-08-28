using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    public class DBHand
    {
        public enum HandType
        {
            NONE = 0,
            WASHOUT = 1,
            TSUMO = 2,
            RON = 3
        }

        [Key, Column(Order = 0)]
        public int HandTournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int HandRoundId { get; set; }

        [Key, Column(Order = 2)]
        public int HandTableId { get; set; }

        [Key, Column(Order = 3)]
        public int HandId { get; set; }
        
        public string PlayerWinnerId { get; set; }

        public string PlayerLooserId { get; set; }

        public string HandScore { get; set; }

        public bool IsChickenHand { get; set; }

        public string PlayerEastPenalty { get; set; }

        public string PlayerSouthPenalty { get; set; }

        public string PlayerWestPenalty { get; set; }

        public string PlayerNorthPenalty { get; set; }
        
        public DBHand() {}

        public DBHand(int tournamentId, int roundId, int tableId, int id)
        {
            HandTournamentId = tournamentId;
            HandRoundId = roundId;
            HandTableId = tableId;
            HandId = id;
            PlayerWinnerId = string.Empty;
            PlayerLooserId = string.Empty;
            HandScore = string.Empty;
            IsChickenHand = false;
            PlayerEastPenalty = string.Empty;
            PlayerSouthPenalty = string.Empty;
            PlayerWestPenalty = string.Empty;
            PlayerNorthPenalty = string.Empty;
        }

        public DBHand(int tournamentId, int roundId, int tableId, int id,
            string playerWinnerId, string playerLooserId, string handScore, bool isChickenHand, 
            string playerEastPenalty, string playerSouthPenalty, 
            string playerWestPenalty, string playerNorthPenalty)
        {
            HandTournamentId = tournamentId;
            HandRoundId = roundId;
            HandTableId = tableId;
            HandId = id;
            PlayerWinnerId = playerWinnerId;
            PlayerLooserId = playerLooserId;
            HandScore = handScore;
            IsChickenHand = isChickenHand;
            PlayerEastPenalty = playerEastPenalty;
            PlayerSouthPenalty = playerSouthPenalty;
            PlayerWestPenalty = playerWestPenalty;
            PlayerNorthPenalty = playerNorthPenalty;
        }

        public HandType GetHandType()
        {
            if (HandScore.Equals(string.Empty))
                return HandType.NONE;
            else if (PlayerWinnerId.Equals(string.Empty))
            {
                if (PlayerLooserId.Equals(string.Empty))
                {
                    if (HandScore.Equals("0"))
                        return HandType.WASHOUT;
                    else
                        return HandType.NONE;
                }
                else
                    return HandType.NONE;
            }
            else if (PlayerLooserId.Equals(string.Empty))
                return HandType.TSUMO;
            else
                return HandType.RON;
        }
    }
}
