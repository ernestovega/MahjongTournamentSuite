using System.Collections.Generic;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingForm
    {
        void FillDGVPlayersFromThread(List<PlayerRanking> playersRankingsRange);

        void FillDGVTeamsFromThread(List<TeamRanking> teamsRankingsRange);
    }
}
