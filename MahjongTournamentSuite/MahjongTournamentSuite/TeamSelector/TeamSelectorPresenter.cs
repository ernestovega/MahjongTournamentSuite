using MahjongTournamentSuite.Data;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamSelector
{
    class TeamSelectorPresenter : ITeamSelectorPresenter
    {
        #region Fields

        private ITeamSelectorForm _form;
        private IDBManager _db;
        private List<string> _teamsNames;

        #endregion

        #region Constructor

        public TeamSelectorPresenter(ITeamSelectorForm teamSelectorForm)
        {
            _form = teamSelectorForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ITeamSelectorPresenter implementation

        public void LoadTeams(int tournamentId)
        {
            _teamsNames = _db.GetTeamsNames(tournamentId);
            _form.FillLbTeams(_teamsNames);
        }

        #endregion
    }
}
