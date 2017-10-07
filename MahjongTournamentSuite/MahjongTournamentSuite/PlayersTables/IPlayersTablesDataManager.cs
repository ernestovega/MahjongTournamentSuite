using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersTables
{
    public interface IPlayersTablesDataManager
    {
        VTournament GetTournament(int tournamentId);

        List<VPlayer> GetTournamentPlayers(int tournamentId);

        List<VTable> GetTournamentTables(int tournamentId);
    }
}