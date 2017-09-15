using System.Drawing;

namespace MahjongTournamentSuite.Model
{
    public class PlayerRanking
    {
        #region Constants

        public static readonly string COLUMN_PLAYER_RANKING_ORDER = "Order";
        public static readonly string COLUMN_PLAYER_RANKING_PLAYER_ID = "PlayerId";
        public static readonly string COLUMN_PLAYER_RANKING_PLAYER_NAME = "PlayerName";
        public static readonly string COLUMN_PLAYER_RANKING_PLAYER_POINTS = "PlayerPoints";
        public static readonly string COLUMN_PLAYER_RANKING_PLAYER_SCORE = "PlayerScore";
        public static readonly string COLUMN_PLAYER_RANKING_TEAM_ID = "PlayerTeamId";
        public static readonly string COLUMN_PLAYER_RANKING_TEAM_NAME = "PlayerTeamName";
        public static readonly string COLUMN_PLAYER_RANKING_COUNTRY_NAME = "PlayerCountryName";
        public static readonly string COLUMN_PLAYER_RANKING_COUNTRY_HTML_FLAG_URL = "PlayerCountryHtmlFlagUrl";
        public static readonly string COLUMN_PLAYER_RANKING_COUNTRY_FLAG = "PlayerCountryFlag";

        #endregion

        #region Properties

        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerTeamName { get; set; }
        public string PlayerCountryName { get; set; }
        public string PlayerCountryHtmlFlagUrl { get; set; }
        public Image PlayerCountryFlag { get; set; }

        #endregion

        #region Constructors

        public PlayerRanking() { }

        public PlayerRanking(int playerId, string playerName, int playerTeamId, 
            string playerTeamName, string playerCountryName,
            string playerCountryHtmlFlagUrl, Image playerCountryFlagImage)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerPoints = 0;
            PlayerScore = 0;
            PlayerTeamId = playerTeamId;
            PlayerTeamName = playerTeamName;
            PlayerCountryName = playerCountryName;
            PlayerCountryHtmlFlagUrl = playerCountryHtmlFlagUrl;
            PlayerCountryFlag = playerCountryFlagImage;
        }

        #endregion
    }
}
