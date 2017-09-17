using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using System;

namespace MahjongTournamentSuite.Home
{
    class HomePresenter : IHomePresenter
    {
        #region Fields

        private IHomeForm _form;
        private IDBManager _db;
        private List<DBTournament> _tournaments;

        #endregion

        #region Constructor

        public HomePresenter(IHomeForm homeForm)
        {
            _form = homeForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region IHomePresenter implementation

        public void LoadTournaments()
        {
            _tournaments = _db.GetTournaments();
            EnableButtonsResumeAndDelete();
            _form.FillDGVTournaments(_tournaments);
        }

        public void OnFormResized()
        {
            _form.CenterMainButtons();
        }

        public void DeleteClicked(int tournamentId)
        {
            if (tournamentId > -1 && _form.RequestDeleteTournamentConfirmation())
            {
                _db.DeleteTournament(tournamentId);
                _tournaments = _db.GetTournaments();
                EnableButtonsResumeAndDelete();
                _form.FillDGVTournaments(_tournaments);
            }
        }

        public void NameChanged(int tournamentId, string newName)
        {
            _db.UpdateTournamentName(tournamentId, newName);
        }

        public string GetTournamentName(int tournamentId)
        {
            return _tournaments.Find(x => x.TournamentId == tournamentId).TournamentName;
        }

        #endregion

        #region Private

        private void EnableButtonsResumeAndDelete()
        {
            if (_tournaments.Count > 0)
                _form.EnableResumeAndDeleteButton();
            else
                _form.DisableResumeAndDeleteButton();
        }

        #endregion
    }
}
