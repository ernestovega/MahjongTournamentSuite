using System;
using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    class HomePresenter : IHomePresenter
    {
        #region Fields

        private IHomeForm _form;
        private IDBManager _dbManager;
        private List<DBTournament> _tournaments;

        #endregion

        #region Constructor

        public HomePresenter(IHomeForm homeForm)
        {
            _form = homeForm;
            _dbManager = Injector.provideDBManager();
        }

        #endregion

        #region IHomePresenter implementation

        public void LoadTournaments()
        {
            _form.showLoading();
            _tournaments = _dbManager.GetTournaments();
            _form.FillDataGridTournaments(_tournaments);
            _form.hideLoading();
        }

        public void DeleteClicked()
        {
            _form.showLoading();
            int tournamentId = _form.GetCurrentTournamentId();
            if (tournamentId > -1 && _form.RequestDeleteTournamentConfirmation())
            {
                DeleteTournament(tournamentId);
                _form.FillDataGridTournaments(_tournaments);
            }
            _form.hideLoading();
        }

        #endregion

        #region Private

        private void DeleteTournament(int tournamentId)
        {
            _dbManager.DeleteTournament(tournamentId);
        }

        #endregion
    }
}
