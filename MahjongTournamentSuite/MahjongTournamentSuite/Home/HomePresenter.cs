using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;

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
            ResumeAndDeleteButtonsEnabling();
            _form.FillDataGridTournaments(_tournaments);
            _form.hideLoading();
        }

        public void DeleteClicked()
        {
            int tournamentId = _form.GetCurrentTournamentId();
            if (tournamentId > -1 && _form.RequestDeleteTournamentConfirmation())
            {
                _form.showLoading();
                DeleteTournament(tournamentId);
                _tournaments = _dbManager.GetTournaments();
                ResumeAndDeleteButtonsEnabling();
                _form.FillDataGridTournaments(_tournaments);
                _form.hideLoading();
            }
        }

        public void NameChanged(int tournamentId, string newName)
        {
            _dbManager.UpdateTournamentName(tournamentId, newName);
        }

        public string GetTournamentName(int tournamentId)
        {
            return _tournaments.Find(x => x.Id == tournamentId).Name;
        }

        #endregion

        #region Private

        private void ResumeAndDeleteButtonsEnabling()
        {
            if (_tournaments.Count > 0)
                _form.EnableResumeAndDeleteButton();
            else
                _form.DisableResumeAndDeleteButton();
        }

        private void DeleteTournament(int tournamentId)
        {
            _dbManager.DeleteTournament(tournamentId);
        }

        #endregion
    }
}
