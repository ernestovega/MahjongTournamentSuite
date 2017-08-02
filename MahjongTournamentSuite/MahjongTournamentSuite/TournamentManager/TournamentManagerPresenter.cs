using MahjongTournamentSuite.Data;
using System.Collections.Generic;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TournamentManager
{
    class TournamentManagerPresenter : ITournamentManagerPresenter
    {
        #region Fields

        private ITournamentManagerForm _form;
        private IDBManager _db;
        private DBTournament _tournament;

        #endregion

        #region Constructor

        public TournamentManagerPresenter(ITournamentManagerForm form)
        {
            _form = form;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ITournamentManagerPresenter

        public void LoadTournament(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _form.FillComboRounds(_tournament.NumRounds);
            _form.GenerateRoundTablesButtons(_tournament.NumPlayers / 4);
        }

        #endregion

        #region Private

        #endregion
    }
}
