using MahjongTournamentSuite.TeamsManager;
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersSelector
{
    interface IPlayersSelectorForm
    {
        void FillLbPlayersNames(List<string> teamPlayersNames);
    }
}
