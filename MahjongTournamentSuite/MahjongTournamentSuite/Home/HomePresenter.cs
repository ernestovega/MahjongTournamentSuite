using System;
using MahjongTournamentSuite.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace MahjongTournamentSuite.Home
{
    class HomePresenter : IHomePresenter
    {
        #region Fields

        private IHomeForm _form;
        private IDBManager _dbManager; 

        #endregion

        #region Constructor

        public HomePresenter(IHomeForm homeForm)
        {
            _form = homeForm;
            _dbManager = Injector.provideTournamentsDBManager();
        }

        #endregion

        #region IHomePresenter implementation

        public void EditNameClicked()
        {
            string currentTournamentName = _form.GetCurrentTournamentName();
            if(currentTournamentName.Length > 0)
            {
                string newTournamentName = _form.RequestNewTournamentName();
                if (newTournamentName.Length > 0 && !newTournamentName.Equals(currentTournamentName))
                {
                    int tournamentId = _form.GetCurrentTournamentId();
                    UpdateName(tournamentId, newTournamentName);
                    _form.ReloadDataGridTournaments();
                }
            }
        }

        public void DeleteClicked()
        {
            int tournamentId = _form.GetCurrentTournamentId();
            if (tournamentId > -1 && _form.RequestDeleteTournamentConfirmation())
            {
                DeleteTournament(tournamentId);
                _form.ReloadDataGridTournaments();
            }
        }

        #endregion

        #region Private

        private void UpdateName(int tournamentId, string newName)
        {
            _dbManager.UpdateTournamentName(tournamentId, newName);
        }

        private void DeleteTournament(int tournamentId)
        {
            _dbManager.DeleteTournament(tournamentId);
        }

        #endregion
    }
}
