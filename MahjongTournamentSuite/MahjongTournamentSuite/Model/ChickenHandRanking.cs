using System.Drawing;

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
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_NAME = "PlayerCountryName";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_IMAGE_URL = "PlayerCountryImageUrl";
        public static readonly string COLUMN_PLAYER_CHICKEN_HAND_RANKING_COUNTRY_FLAG_IMAGE = "PlayerCountryFlagImage";

        #endregion

        #region Properties

        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerNumChickenHands { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public string PlayerCountryName { get; set; }
        public string PlayerCountryImageUrl { get; set; }
        public Image PlayerCountryFlagImage { get; set; }

        #endregion

        #region Constructor

        public ChickenHandRanking() { }

        public ChickenHandRanking(int playerId, string playerName, int playerPoints, int playerScore,
            string playerCountryName, string playerCountryImageUrl, Image playerCountryFlagImage)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerNumChickenHands = 0;
            PlayerPoints = playerPoints;
            PlayerScore = playerScore;
            PlayerCountryName = playerCountryName;
            PlayerCountryImageUrl = playerCountryImageUrl;
            PlayerCountryFlagImage = playerCountryFlagImage;
        }

        #endregion
    }
}
