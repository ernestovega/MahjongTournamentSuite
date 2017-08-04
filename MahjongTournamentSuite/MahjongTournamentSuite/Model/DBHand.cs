using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MahjongTournamentSuite.Model
{
    class DBHand
    {
        [Key, Column(Order = 0)]
        public int TournamentId { get; set; }

        [Key, Column(Order = 1)]
        public int RoundId { get; set; }

        [Key, Column(Order = 2)]
        public int TableId { get; set; }

        [Key, Column(Order = 3)]
        public int Id { get; set; }

        public int Points { get; set; }

        public bool IsChickenHand { get; set; }

        public int PlayerEastPoints { get; set; }

        public int PlayerSouthPoints { get; set; }

        public int PlayerWestPoints { get; set; }

        public int PlayerNorthPoints { get; set; }

        public DBHand() { }

        public DBHand(int tournamentId, int roundId, int tableId, int id)
        {
            TournamentId = tournamentId;
            RoundId = roundId;
            TableId = tableId;
            Id = id;
        }

        public DBHand(int id, int tournamentId, int roundId, int tableId, int points, bool isChickenHand,
            int playerEastPoints, int playerSouthPoints, int playerWestPoints, int playerNorthPoints)
        {
            Id = id;
            TournamentId = tournamentId;
            RoundId = roundId;
            TableId = tableId;
            Points = points;
            IsChickenHand = isChickenHand;
            RoundId = roundId;
            RoundId = roundId;
            PlayerEastPoints = playerEastPoints;
            PlayerSouthPoints = playerSouthPoints;
            PlayerWestPoints = playerWestPoints;
            PlayerNorthPoints = playerNorthPoints;
        }
    }
}
