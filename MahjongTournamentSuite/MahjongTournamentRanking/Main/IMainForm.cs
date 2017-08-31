﻿using System.Collections.Generic;
using MahjongTournamentRanking.Model;

namespace MahjongTournamentRanking.Main
{
    interface IMainForm
    {
        void FillDGVPlayersFromThread(List<PlayerRanking> playersRankingsRange);

        void FillDGVTeamsFromThread(List<TeamRanking> teamsRankingsRange);
    }
}
