namespace MahjongTournamentSuite.Model
{
    public class TeamRanking
    {
        #region Constants

        public static readonly string COLUMN_TEAM_RANKING_ORDER = "Order";
        public static readonly string COLUMN_TEAM_RANKING_TEAM_ID = "TeamId";
        public static readonly string COLUMN_TEAM_RANKING_TEAM_NAME = "TeamName";
        public static readonly string COLUMN_TEAM_RANKING_TEAM_POINTS = "TeamPoints";
        public static readonly string COLUMN_TEAM_RANKING_TEAM_SCORE = "TeamScore";

        #endregion

        #region Properties

        public int Order { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int TeamPoints { get; set; }
        public int TeamScore { get; set; }

        #endregion

        #region Constructors

        public TeamRanking() { }

        public TeamRanking(int teamId, string teamName)
        {
            Order = 0;
            TeamId = teamId;
            TeamName = teamName;
            TeamPoints = 0;
            TeamScore = 0;
        }

        #endregion
    }
}
