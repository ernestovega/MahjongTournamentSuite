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
        public static readonly string COLUMN_PLAYER_RANKING_COUNTRY_ID = "PlayerCountryId";
        public static readonly string COLUMN_PLAYER_RANKING_COUNTRY_NAME = "PlayerCountryName";

        #endregion

        #region Properties

        public int Order { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPoints { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerTeamName { get; set; }
        public int PlayerCountryId { get; set; }
        public string PlayerCountryName { get; set; }
        public string PlayerCountryImageUrl { get; set; }

        #endregion

        #region Constructors

        public PlayerRanking() { }

        public PlayerRanking(int playerId, string playerName, int playerTeamId, 
            string playerTeamName, int playerCountryId, string playerCountryName,
            string playerCountryImageUrl)
        {
            Order = 0;
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerPoints = 0;
            PlayerScore = 0;
            PlayerTeamId = playerTeamId;
            PlayerTeamName = playerTeamName;
            PlayerCountryId = playerCountryId;
            PlayerCountryName = playerCountryName;
            PlayerCountryImageUrl = playerCountryImageUrl;
        }

        #endregion
    }
}
