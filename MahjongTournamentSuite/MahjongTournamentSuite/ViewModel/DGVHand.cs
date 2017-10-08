using MahjongTournamentSuite._Data.DataModel;

namespace MahjongTournamentSuite.TableManager
{
    internal class DGVHand : VHand
    {
        public static readonly string COLUMN_PLAYER_EAST_SCORE = "PlayerEastScore";
        public static readonly string COLUMN_PLAYER_SOUTH_SCORE = "PlayerSouthScore";
        public static readonly string COLUMN_PLAYER_WEST_SCORE = "PlayerWestScore";
        public static readonly string COLUMN_PLAYER_NORTH_SCORE = "PlayerNorthScore";

        public string PlayerEastScore { get; set; }

        public string PlayerSouthScore { get; set; }

        public string PlayerWestScore { get; set; }

        public string PlayerNorthScore { get; set; }

        public DGVHand() {}

        public DGVHand(VHand dbHand) 
            : base(dbHand.HandTournamentId, dbHand.HandRoundId, 
                  dbHand.HandTableId, dbHand.HandId, dbHand.PlayerWinnerId,
                  dbHand.PlayerLooserId, dbHand.HandScore, dbHand.IsChickenHand,
                  dbHand.PlayerEastPenalty, dbHand.PlayerSouthPenalty, 
                  dbHand.PlayerWestPenalty, dbHand.PlayerNorthPenalty)
        {
            PlayerEastScore = string.Empty;
            PlayerSouthScore = string.Empty;
            PlayerWestScore = string.Empty;
            PlayerNorthScore = string.Empty;
        }
    }
}