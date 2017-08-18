using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Data
{
    internal interface IDBManager
    {
        #region NewTournament

        int GetExistingMaxTournamentId();

        void AddTournament(DBTournament tournament);

        void DeleteTournament(int tournamentId);

        void UpdateTournamentName(int tournamentId, string newName);

        void AddPlayers(List<DBPlayer> players);

        void AddTables(List<DBTable> tables);

        void AddHands(List<DBHand> hands);

        void AddTeams(List<DBTeam> teams);

        #endregion

        #region Tournament Manager

        DBTournament GetTournament(int tournamentId);

        List<DBTournament> GetTournaments();

        List<DBTeam> GetTournamentTeams(int tournamentId);

        List<DBPlayer> GetTournamentPlayers(int tournamentId);

        #endregion

        #region Table Manager

        DBTable GetTable(int tournamentId, int roundId, int tableId);

        string GetTournamentName(int tournamentId);

        List<DBHand> GetTableHands(int tournamentId, int roundId, int tableId);

        List<DBPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId);

        void UpdateHand(DBHand hand);

        void UpdateTablePlayersPositions(DBTable table);

        #endregion
    }
}