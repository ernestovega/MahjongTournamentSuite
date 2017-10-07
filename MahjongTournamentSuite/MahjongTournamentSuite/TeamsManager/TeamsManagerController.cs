using MahjongTournamentSuite._Data.DataModel;
using System;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamsManager
{
    class TeamsManagerController : ITeamsManagerController
    {
        #region Fields

        private ITeamsManagerForm _form;
        private ITeamsManagerDataManager _data;
        private VTournament _tournament;
        private List<VTeam> _teams;

        #endregion

        #region Constructor

        public TeamsManagerController(ITeamsManagerForm teamsManagerForm)
        {
            _form = teamsManagerForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountryManagerController implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _data.GetTournament(tournamentId);
            _teams = _data.GetTournamentTeams(tournamentId);
            _form.FillDGV(_teams);
        }

        public void TeamNameChanged(int teamId, string newName)
        {
            int ownerTeamId = GetOwnerTeamNameId(newName);
            if (ownerTeamId > 0)
            {
                _form.DGVCancelEdit();
                _form.ShowMessageTeamNameInUse(newName, ownerTeamId);
                return;
            }
            _data.UpdateTeamName(_tournament.TournamentId, teamId, newName);
        }

        #endregion

        #region Private

        private int GetOwnerTeamNameId(string newName)
        {
            VTeam ownerTeam = _teams.Find(x => x.TeamName.Equals(newName,
                StringComparison.InvariantCulture));
            if (ownerTeam == null)
                return 0;
            else
                return ownerTeam.TeamId;
        }

        #endregion
    }
}
