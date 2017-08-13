using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TableManager
{
    internal class DataGridHand : DBHand
    {
        public int PlayerEastScore { get; set; }

        public int PlayerSouthScore { get; set; }

        public int PlayerWestScore { get; set; }

        public int PlayerNorthScore { get; set; }

        public DataGridHand() {}

        public DataGridHand(DBHand dbHand, int playerEastScore, 
            int playerSouthScore, int playerWestScore, int playerNorthScore) 
            : base(dbHand.TournamentId, dbHand.RoundId, dbHand.TableId, dbHand.Id)
        {
            PlayerEastScore = playerEastScore;
            PlayerSouthScore = playerSouthScore;
            PlayerWestScore = playerWestScore;
            PlayerNorthScore = playerNorthScore;
        }
    }
}