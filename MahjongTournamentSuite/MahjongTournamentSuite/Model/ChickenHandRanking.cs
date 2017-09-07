namespace MahjongTournamentSuite.Model
{
    public class ChickenHandRanking
    {
        #region Constants

        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_ORDER = "Order";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_ID = "PlayerId";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_NAME = "PlayerName";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_NUM_CHICKEN_HANDS = "PlayerNumChickenHands";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_POINTS = "PlayerPoints";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_PLAYER_SCORE = "PlayerScore";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_ID = "PlayerCountryId";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_NAME = "PlayerCountryName";

        #endregion

        #region Properties

        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerNumChickenHands { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerCountryId { get; set; }
        public string PlayerCountryName { get; set; }

        #endregion

        #region Constructor

        public ChickenHandRanking() { }

        public ChickenHandRanking(int playerId, string playerName, 
            int playerPoints, int playerScore, int playerCountryId, string playerCountryName)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerNumChickenHands = 0;
            PlayerPoints = playerPoints;
            PlayerScore = playerScore;
            PlayerCountryId = playerCountryId;
            PlayerCountryName = playerCountryName;
        }

        #endregion
    }
}
