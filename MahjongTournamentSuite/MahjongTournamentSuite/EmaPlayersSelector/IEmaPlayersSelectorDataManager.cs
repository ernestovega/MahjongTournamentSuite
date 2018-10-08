using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    public interface IEmaPlayersSelectorDataManager
    {
        List<string> GetEmaPlayersNames();
    }
}