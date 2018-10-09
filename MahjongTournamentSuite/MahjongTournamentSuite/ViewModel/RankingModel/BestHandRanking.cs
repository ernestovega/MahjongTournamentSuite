using System.Drawing;

namespace MahjongTournamentSuite.ViewModel
{
    public class BestHandRanking
    {
        #region Constants

        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_ORDER = "Order";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_ID = "PlayerId";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_NAME = "PlayerName";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_BEST_HAND_VALUE = "PlayerBestHandValue";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_POINTS = "PlayerPoints";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_PLAYER_SCORE = "PlayerScore";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_NAME = "PlayerCountryName";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_HTML_FLAG_URL = "PlayerCountryHtmlFlagUrl";
        public static readonly string COLUMN_PLAYER_BEST_HAND_RANKING_COUNTRY_FLAG = "PlayerCountryFlag";

        #endregion

        #region Properties

        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerBestHandValue { get; set; }
        public float PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public string PlayerCountryName { get; set; }
        public string PlayerCountryHtmlFlagUrl { get; set; }
        public Image PlayerCountryFlag { get; set; }

        #endregion

        #region Constructor

        public BestHandRanking() { }

        public BestHandRanking(int playerId, string playerName, int playerBestHandValue, float playerPoints, int playerScore,
            string playerCountryName, string playerCountryHtmlFlagUrl, Image playerCountryFlagImage)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerBestHandValue = playerBestHandValue;
            PlayerPoints = playerPoints;
            PlayerScore = playerScore;
            PlayerCountryName = playerCountryName;
            PlayerCountryHtmlFlagUrl = playerCountryHtmlFlagUrl;
            PlayerCountryFlag = playerCountryFlagImage;
        }

        #endregion
    }
}
