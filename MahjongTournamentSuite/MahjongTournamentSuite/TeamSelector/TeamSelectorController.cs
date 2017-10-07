using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamSelector
{
    class TeamSelectorController : ITeamSelectorController
    {
        #region Fields

        private ITeamSelectorForm _form;
        private ITeamSelectorDataManager _data;
        private List<string> _teamsNames;

        #endregion

        #region Constructor

        public TeamSelectorController(ITeamSelectorForm teamSelectorForm)
        {
            _form = teamSelectorForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ITeamSelectorController implementation

        public void LoadForm(int tournamentId)
        {
            _teamsNames = _data.GetTeamsNames(tournamentId);
            _form.FillLbTeams(_teamsNames);
        }

        #endregion
    }
}
