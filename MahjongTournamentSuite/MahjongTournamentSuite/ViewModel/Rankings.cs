using System.Collections.Generic;

namespace MahjongTournamentSuite.ViewModel
{
    public class Rankings
    {
        public List<PlayerRanking> PlayersRankings { get; set; }
        public List<TeamRanking> TeamsRankings { get; set; }
        public List<ChickenHandRanking> PlayersChickenHandsRankings { get; set; }
        public List<BestHandRanking> PlayersBestHandsRankings { get; set; }
        public bool IsTeams { get; internal set; }

        public Rankings() {}

        public Rankings(List<PlayerRanking> playersRankings, List<TeamRanking> teamsRankings, 
            List<ChickenHandRanking> playersChickenHandsRankings, List<BestHandRanking> playersBestHandsRankings, 
            bool isTeams)
        {
            PlayersRankings = playersRankings;
            TeamsRankings = teamsRankings;
            PlayersChickenHandsRankings = playersChickenHandsRankings;
            PlayersBestHandsRankings = playersBestHandsRankings;
            IsTeams = isTeams;
        }
    }
}
