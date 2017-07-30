using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.Data
{
    internal interface IDBManager
    {
        void AddTournament(Tournament tournament);

        void DeleteTournament(int tournamentId);

        void UpdateTournamentName(int tournamentId, string newName);
    }
}