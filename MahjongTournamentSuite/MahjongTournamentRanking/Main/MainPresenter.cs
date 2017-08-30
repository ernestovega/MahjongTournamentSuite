using System;
using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;

namespace MahjongTournamentRanking.Main
{
    class MainPresenter : IMainPresenter
    {
        private IDBManager _db;
        private DBTournament _tournament;
        private List<DBPlayer> _players;
        private List<DBTeam> _teams;
        private List<DBTable> _tables;

        public MainPresenter()
        {
            _db = new DBManager();
        }

        public void LoadRanking(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            _teams = _db.GetTournamentTeams(tournamentId);
            _tables = _db.GetTournamentTables(tournamentId);
        }
    }
}
