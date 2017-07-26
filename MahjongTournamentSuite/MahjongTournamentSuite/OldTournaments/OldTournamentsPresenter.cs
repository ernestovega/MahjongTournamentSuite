using MahjongTournamentSuite.Model;
using System;
using System.Linq;
using MahjongTournamentSuite.Data;
using System.Collections.Generic;
using static MahjongTournamentSuite.Data.DBContext;

namespace MahjongTournamentSuite.OldTournaments
{
    internal class OldTournamentsPresenter : IOldTournamentsPresenter
    {
        #region Fields

        private IOldTournamentsForm _oldTournamentsForm;

        #endregion

        #region Constructor

        public OldTournamentsPresenter(IOldTournamentsForm oldTournamentsForm)
        {
            _oldTournamentsForm = oldTournamentsForm;
            using (var db = new TournamentSuiteDB())
            {
                db.Tournaments.Add(new Tournament(1, 40, 4, "1st Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(2, 40, 4, "2nd Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(3, 40, 4, "3rd Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(4, 40, 4, "4th Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(5, 40, 4, "5th Torneo de prueba", DateTime.Now));
                db.SaveChanges();
            }
        }

        #endregion

        #region IOldTournamentsPresenter implementation

        public void UpdateName(int tournamentId, string newName)
        {
            using (var db = new TournamentSuiteDB())
            {
                db.Tournaments.ToList().First(x => x.Id == tournamentId).Name = newName;
                db.SaveChanges();
            }
        }

        public void DeleteTournament(int tournamentId)
        {
            using (var db = new TournamentSuiteDB())
            {
                List<Table> tables = db.Tables.ToList().FindAll(x => x.TournamentId == tournamentId);
                db.Tables.RemoveRange(tables);
                db.SaveChanges();
            }
        }

        #endregion
    }
}