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

        public DBManager()
        {
            
        }

        #endregion

        #region Tournament

        public int GetExistingMaxTournamentId()
        {
            if (_db.Tournaments.Count() == 0)
            {
                return 0;
            }
            else
            {
                return _db.Tournaments.Max(x => x.Id);
            }
        }

        public void AddTournament(DBTournament tournament)
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

        #region Player

        public void AddPlayers(List<DBPlayer> players)
        {
            _db.Players.AddRange(players);
            _db.SaveChanges();
        }

        #endregion

        #region Table

        public void AddTables(List<DBTable> tables)
        {
            _db.Tables.AddRange(tables);
            _db.SaveChanges();
        }

        #endregion
    }
}
