namespace MahjongTournamentSuite.TeamsManager
{
    public class DGVTeamPlayer
    {
        public static readonly string COLUMN_TEAMPLAYER_ID = "TeamPlayerId";
        public static readonly string COLUMN_TEAMPLAYER_NAME = "TeamPlayerName";

        public int TeamPlayerId { get; set; }
        public string TeamPlayerName { get; set; }

        public DGVTeamPlayer(int playerId, string playerName)
        {
            TeamPlayerId = playerId;
            TeamPlayerName = playerName;
        }
    }
}