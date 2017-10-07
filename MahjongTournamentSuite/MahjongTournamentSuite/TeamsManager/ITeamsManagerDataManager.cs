using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamsManager
{
    public interface ITeamsManagerDataManager
    {
        VTournament GetTournament(int tournamentId);

        List<VTeam> GetTournamentTeams(int tournamentId);

        void UpdateTeamName(int tournamentId, int teamId, string newName);
    }
}