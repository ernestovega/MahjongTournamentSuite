using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using MahjongTournamentSuite._Data.Interfaces;

namespace MahjongTournamentSuite.Home
{
    class HomeController : IHomeController
    {
        #region Fields

        private IHomeForm _form;
        private IHomeDataManager _data;
        private List<VTournament> _tournaments;

        #endregion

        #region Constructor

        public HomeController(IHomeForm homeForm)
        {
            _form = homeForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region IHomeController implementation

        public void LoadTournaments()
        {
            _tournaments = _data.GetTournaments();
            EnableButtonsResumeAndDelete();
            _form.FillDGVTournaments(_tournaments);
        }

        public void DeleteClicked(int tournamentId)
        {
            if (tournamentId > -1 && _form.RequestDeleteTournamentConfirmation())
            {
                _data.DeleteTournament(tournamentId);
                _tournaments = _data.GetTournaments();
                EnableButtonsResumeAndDelete();
                _form.FillDGVTournaments(_tournaments);
            }
        }

        public void NameChanged(int tournamentId, string newName)
        {
            _data.UpdateTournamentName(tournamentId, newName);
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
