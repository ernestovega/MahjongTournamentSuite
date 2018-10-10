using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.TeamsManager;
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersSelector
{
    public interface IPlayersSelectorDataManager
    {
        List<string> GetAvailableTeamPlayersNames(int tournamentId, int teamId);
    }
}