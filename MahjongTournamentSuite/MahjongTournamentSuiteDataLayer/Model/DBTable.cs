using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBTable {

        [Key, Column(Order = 0)]
        public int TableTournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int TableRoundId { get; set; }

        [Key, Column(Order = 2)]
        public int TableId { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }

        public int Player3Id { get; set; }

        public int Player4Id { get; set; }

        public string PlayerEastId { get; set; }

        public string PlayerSouthId { get; set; }

        public string PlayerWestId { get; set; }

        public string PlayerNorthId { get; set; }

        public string PlayerEastScore { get; set; }

        public string PlayerSouthScore { get; set; }

        public string PlayerWestScore { get; set; }

        public string PlayerNorthScore { get; set; }

        public string PlayerEastPoints { get; set; }

        public string PlayerSouthPoints { get; set; }

        public string PlayerWestPoints { get; set; }

        public string PlayerNorthPoints { get; set; }

        public DBTable() {}

        public DBTable(int tournamentId, int roundId, int id,
            int player1Id, int player2Id, int player3Id, int player4Id)
        {
            TableTournamentId = tournamentId;
            TableRoundId = roundId;
            TableId = id;
            Player1Id = player1Id;
            Player2Id = player2Id;
            Player3Id = player3Id;
            Player4Id = player4Id;
            PlayerEastId = string.Empty;
            PlayerSouthId = string.Empty;
            PlayerWestId = string.Empty;
            PlayerNorthId = string.Empty;
            PlayerEastScore = string.Empty;
            PlayerSouthScore = string.Empty;
            PlayerWestScore = string.Empty;
            PlayerNorthScore = string.Empty;
            PlayerEastPoints = string.Empty;
            PlayerSouthPoints = string.Empty;
            PlayerWestPoints = string.Empty;
            PlayerNorthPoints = string.Empty;
        }

        public DBTable(int tournamentId, int roundId, int id,
            int player1Id, int player2Id, int player3Id, int player4Id,
            string playerEastId, string playerSouthId, string playerWestId, string playerNorthId,
            string playerEastScore, string playerSouthScore, string playerWestScore, string playerNorthScore,
            string playerEastPoints, string playerSouthPoints, string playerWestPoints, string playerNorthPoints)
        {
            TableTournamentId = tournamentId;
            TableRoundId = roundId;
            TableId = id;
            Player1Id = player1Id;
            Player2Id = player2Id;
            Player3Id = player3Id;
            Player4Id = player4Id;
            PlayerEastId = playerEastId;
            PlayerSouthId = playerSouthId;
            PlayerWestId = playerWestId;
            PlayerNorthId = playerNorthId;
            PlayerEastScore = playerEastScore;
            PlayerSouthScore = playerSouthScore;
            PlayerWestScore = playerWestScore;
            PlayerNorthScore = playerNorthScore;
            PlayerEastPoints = playerEastPoints;
            PlayerSouthPoints = playerSouthPoints;
            PlayerWestPoints = playerWestPoints;
            PlayerNorthPoints = playerNorthPoints;
        }
    }
}
