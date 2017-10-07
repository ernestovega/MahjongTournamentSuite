using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite._Data.Interfaces
{
    public interface INewTournamentDataManager
    {
        bool ExistTournamentByName(string tournamentName);

        void AddTournament(VTournament tournament);

        void AddPlayers(List<VPlayer> players);

        void AddTeams(List<VTeam> teams);

        void AddTables(List<VTable> tables, List<VHand> hands);

        int GetIdForNewTournament();
    }
}
