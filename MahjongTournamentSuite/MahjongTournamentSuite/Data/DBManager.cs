using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System.Linq;
using static MahjongTournamentSuite.Data.DBContext;

namespace MahjongTournamentSuite.Data
{
    class DBManager : IDBManager
    {
        #region Fields

        TournamentSuiteDB _db = new TournamentSuiteDB();

        #endregion

        #region Constructor

        public DBManager() {}

        #endregion

        #region Tournament

        public void AddTournament(Tournament tournament)
        {
            _db.Tournaments.Add(tournament);
            _db.SaveChanges();
        }

        public void UpdateTournamentName(int tournamentId, string newName)
        {
            _db.Tournaments.ToList().First(x => x.Id == tournamentId).Name = newName;
            _db.SaveChanges();
        }

        public void DeleteTournament(int tournamentId)
        {
            DeleteTournamentTables(tournamentId);
            DeleteTournamentPlayers(tournamentId);
            _db.Tournaments.Remove(_db.Tournaments.First(x => x.Id == tournamentId));
            _db.SaveChanges();
        }

        public void DeleteTournamentTables(int tournamentId)
        {
            _db.Tables.RemoveRange(_db.Tables.ToList().FindAll(x => x.TournamentId == tournamentId));
            _db.SaveChanges();
        }

        public void DeleteTournamentPlayers(int tournamentId)
        {
            _db.Players.RemoveRange(_db.Players.ToList().FindAll(x => x.TournamentId == tournamentId));
            _db.SaveChanges();
        }


        #endregion



        #region Table

        #endregion

        #region Player



        #endregion
    }
}
