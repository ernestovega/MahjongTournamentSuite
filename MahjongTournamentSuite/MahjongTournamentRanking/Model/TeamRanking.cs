namespace MahjongTournamentRanking.Model
{
    public class TeamRanking
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int TeamPoints { get; set; }
        public int TeamScore { get; set; }

        public TeamRanking() { }

        public TeamRanking(int teamId, string teamName)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeamPoints = 0;
            TeamScore = 0;
        }
    }
}
