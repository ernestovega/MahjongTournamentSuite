﻿using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuiteDataLayer.Data
{
    public interface IDBManager
    {
        #region NewTournament

        bool ExistTournament(string tournamentName);

        int GetExistingMaxTournamentId();

        void AddTournament(DBTournament tournament);

        void DeleteTournament(int tournamentId);

        void UpdateTournamentName(int tournamentId, string newName);

        void AddPlayers(List<DBPlayer> players);

        void AddTables(List<DBTable> tables, List<DBHand> hands);

        void AddTeams(List<DBTeam> teams);

        #endregion

        #region Tournament Manager

        DBTournament GetTournament(int tournamentId);

        List<DBTournament> GetTournaments();

        List<DBPlayer> GetTournamentPlayers(int tournamentId);

        List<DBTeam> GetTournamentTeams(int tournamentId);

        List<DBTable> GetTournamentTables(int tournamentId);

        List<string> GetTeamsNames(int tournamentId);

        string GetTeamName(int tournamentId, int teamId);

        int GetTeamId(int tournamentId, string teamName);

        string GetCountryName(int countryId);

        int GetCountryId(string countryName);

        void UpdateTeamName(int tournamentId, int teamId, string newName);

        void UpdatePlayerName(int tournamentId, int playerId, string newName);

        void UpdatePlayerTeam(int tournamentId, int playerId, int countryId);

        void UpdatePlayerCountry(int tournamentId, int playerId, int countryId);

        #endregion

        #region Countries Selector

        List<string> GetCountriesNames();

        #endregion

        #region Table Manager

        DBTable GetTable(int tournamentId, int roundId, int tableId);

        string GetTournamentName(int tournamentId);

        List<DBHand> GetTableHands(int tournamentId, int roundId, int tableId);

        List<DBPlayer> GetTablePlayers(int tournamentId, int roundId, int tableId);

        void UpdateTableAllPlayersPositions(DBTable table);

        void UpdateTableAllPlayersTotalScores(DBTable table);

        void UpdateTableAllPlayersPoints(DBTable table);

        void UpdateHandWinnerId(DBHand hand, string newPlayerWinnerId);

        void UpdateHandLooserId(DBHand hand, string newPlayerLooserId);

        void UpdateHandScore(DBHand hand, string newHandScore);

        void UpdateHandIsChickenHand(DBHand hand, bool newIsChickenHand);

        void UpdateHandPlayerEastPenalty(DBHand hand, string newPlayerEastPenaltyValue);

        void UpdateHandPlayerSouthPenalty(DBHand hand, string newPlayerSouthPenaltyValue);

        void UpdateHandPlayerWestPenalty(DBHand hand, string newPlayerWestPenaltyValue);

        void UpdateHandPlayerNorthPenalty(DBHand hand, string newPlayerNorthPenaltyValue);


        #endregion
    }
}