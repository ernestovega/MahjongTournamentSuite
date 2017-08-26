using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
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

        public int PlayerEastId { get; set; }

        public int PlayerSouthId { get; set; }

        public int PlayerWestId { get; set; }

        public int PlayerNorthId { get; set; }

        public string PlayerEastTotalScore { get; set; }

        public string PlayerSouthTotalScore { get; set; }

        public string PlayerWestTotalScore { get; set; }

        public string PlayerNorthTotalScore { get; set; }

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
            PlayerEastId = 0;
            PlayerSouthId = 0;
            PlayerWestId = 0;
            PlayerNorthId = 0;
            PlayerEastTotalScore = string.Empty;
            PlayerSouthTotalScore = string.Empty;
            PlayerWestTotalScore = string.Empty;
            PlayerNorthTotalScore = string.Empty;
        }
    }
}
