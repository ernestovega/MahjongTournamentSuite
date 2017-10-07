using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    public interface IHomeDataManager
    {
        List<VTournament> GetTournaments();

        void DeleteTournament(int tournamentId);

        void UpdateTournamentName(int tournamentId, string newName);
    }
}
