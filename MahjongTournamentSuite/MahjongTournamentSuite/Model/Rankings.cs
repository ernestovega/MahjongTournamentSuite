using System.Collections.Generic;

namespace MahjongTournamentSuite.Model
{
    public class Rankings
    {
        public List<PlayerRanking> PlayersRankings { get; set; }
        public List<TeamRanking> TeamsRankings { get; set; }
        public List<PlayerChickenHandRanking> PlayersChickenHandsRankings { get; set; }
        public bool IsTeams { get; internal set; }

        public Rankings() {}

        public Rankings(List<PlayerRanking> playersRankings, List<TeamRanking> teamsRankings, 
            List<PlayerChickenHandRanking> playerschickenHandsRankings, bool isTeams)
        {
            PlayersRankings = playersRankings;
            TeamsRankings = teamsRankings;
            PlayersChickenHandsRankings = playerschickenHandsRankings;
            IsTeams = isTeams;
        }
    }
}
