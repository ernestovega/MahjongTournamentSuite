using System.Collections.Generic;

namespace MahjongTournamentSuite.ViewModel
{
    public class HTMLRankings
    {
        public string PlayersRanking { get; set; }
        public string TeamsRanking { get; set; }
        public string PlayersChickenHandsRanking { get; set; }
        public bool IsTeams { get; internal set; }

        public HTMLRankings() {}

        public HTMLRankings(string playersRanking, string teamsRanking, 
            string playerschickenHandsRanking, bool isTeams)
        {
            PlayersRanking = playersRanking;
            TeamsRanking = teamsRanking;
            PlayersChickenHandsRanking = playerschickenHandsRanking;
            IsTeams = isTeams;
        }
    }
}
