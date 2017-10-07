using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamSelector
{
    public interface ITeamSelectorDataManager
    {
        List<string> GetTeamsNames(int tournamentId);
    }
}