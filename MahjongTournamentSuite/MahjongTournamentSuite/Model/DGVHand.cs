using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TableManager
{
    internal class DGVHand : DBHand
    {
        public string PlayerEastScore { get; set; }

        public string PlayerSouthScore { get; set; }

        public string PlayerWestScore { get; set; }

        public string PlayerNorthScore { get; set; }

        public DGVHand() {}

        public DGVHand(DBHand dbHand) 
            : base(dbHand.HandTournamentId, dbHand.HandRoundId, 
                  dbHand.HandTableId, dbHand.HandId, dbHand.PlayerWinnerId,
                  dbHand.PlayerLooserId, dbHand.HandScore, dbHand.IsChickenHand)
        {
            PlayerEastScore = string.Empty;
            PlayerSouthScore = string.Empty;
            PlayerWestScore = string.Empty;
            PlayerNorthScore = string.Empty;
        }
    }
}