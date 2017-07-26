using MahjongTournamentSuite.Model;
using System;
using System.Linq;
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
        }

        #endregion

        #region IOldTournamentsPresenter implementation

        public void loadTournaments()
        {
            using (var db = new TournamentSuiteDB())
            {
                db.Tournaments.Add(new Tournament(1, 40, 4, "1st Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(2, 40, 4, "2nd Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(3, 40, 4, "3rd Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(4, 40, 4, "4th Torneo de prueba", DateTime.Now));
                db.Tournaments.Add(new Tournament(5, 40, 4, "5th Torneo de prueba", DateTime.Now));
                db.SaveChanges();
                
                var tournamentsByDate = from tournament in db.Tournaments
                            orderby tournament.CreationDate descending
                            select tournament;

                _oldTournamentsForm.BindDataGrid();
            }
        }

        #endregion
    }
}