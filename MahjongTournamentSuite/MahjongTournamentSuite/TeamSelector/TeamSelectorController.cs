using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamSelector
{
    class TeamSelectorController : ITeamSelectorController
    {
        #region Fields

        private ITeamSelectorForm _form;
        private ITeamSelectorDataManager _data;
        private List<string> _allTeamsNames;
        private List<string> _filteredTeamsNames;
        private string filter;

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
            _allTeamsNames = _data.GetTeamsNames(tournamentId);
            _form.FillLbTeams(_allTeamsNames);
        }

        public void FilterList(string text)
        {
            filter = text;
            _filteredTeamsNames = new List<string>(_allTeamsNames);
            _filteredTeamsNames = _filteredTeamsNames.FindAll(
                x => x.ToLower().Contains(filter.ToLower()));
            _form.FillLbTeams(_filteredTeamsNames);
        }

        #endregion
    }
}
