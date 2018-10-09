using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    public interface IEmaPlayersSelectorDataManager
    {
        List<string> GetAvailableEmaPlayersNames(int tournamentId);
    }
}