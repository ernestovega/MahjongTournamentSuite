using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TableManager
{
    internal class DGVHand : DBHand
    {
        public int PlayerEastScore { get; set; }

        public int PlayerSouthScore { get; set; }

        public int PlayerWestScore { get; set; }

        public int PlayerNorthScore { get; set; }

        public DGVHand() {}

        public DGVHand(DBHand dbHand, int playerEastScore, 
            int playerSouthScore, int playerWestScore, int playerNorthScore) 
            : base(dbHand.HandTournamentId, dbHand.HandRoundId, dbHand.HandTableId, dbHand.HandId)
        {
            PlayerEastScore = playerEastScore;
            PlayerSouthScore = playerSouthScore;
            PlayerWestScore = playerWestScore;
            PlayerNorthScore = playerNorthScore;
        }
    }
}