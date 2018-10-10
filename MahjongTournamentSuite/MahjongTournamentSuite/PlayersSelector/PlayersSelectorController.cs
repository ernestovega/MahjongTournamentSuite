using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.TeamsManager;
using System.Collections.Generic;

namespace MahjongTournamentSuite.PlayersSelector
{
    class PlayersSelectorController : IPlayersSelectorController
    {
        #region Fields

        private IPlayersSelectorForm _form;
        private IPlayersSelectorDataManager _data;

        #endregion

        #region Constructor

        public PlayersSelectorController(IPlayersSelectorForm PlayersSelectorForm)
        {
            _form = PlayersSelectorForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountrySelectorController implementation

        public void LoadForm(int tournamentId, int teamId)
        {
            List<string> availableTeamPlayersNames = new List<string>();
            availableTeamPlayersNames.Add(string.Empty);
            availableTeamPlayersNames.AddRange(_data.GetAvailableTeamPlayersNames(tournamentId, teamId));
            _form.FillLbPlayersNames(availableTeamPlayersNames);
        }

        #endregion
    }
}
