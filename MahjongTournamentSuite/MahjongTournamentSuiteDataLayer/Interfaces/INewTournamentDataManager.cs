using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuiteDataLayer.Interfaces
{
    public interface INewTournamentDataManager
    {
        bool ExistTournament(string tournamentName);

        void AddTournament(DBTournament tournament);

        void AddPlayers(List<DBPlayer> players);

        void AddTeams(List<DBTeam> teams);

        void AddTables(List<DBTable> tables, List<DBHand> hands);
    }
}
