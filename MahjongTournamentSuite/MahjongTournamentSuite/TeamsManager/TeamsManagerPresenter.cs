using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TeamsManager
{
    class TeamsManagerPresenter : ITeamsManagerPresenter
    {
        #region Fields

        private ITeamsManagerForm _form;
        private IDBManager _db;
        private DBTournament _tournament;
        private List<DBTeam> _teams;

        #endregion

        #region Constructor

        public TeamsManagerPresenter(ITeamsManagerForm teamsManagerForm)
        {
            _form = teamsManagerForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ICountryManagerPresenter implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _teams = _db.GetTournamentTeams(tournamentId);
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
            _db.UpdateTeamName(_tournament.TournamentId, teamId, newName);
        }

        #endregion

        #region Private

        private int GetOwnerTeamNameId(string newName)
        {
            DBTeam ownerTeam = _teams.Find(x => x.TeamName.Equals(newName,
                StringComparison.InvariantCulture));
            if (ownerTeam == null)
                return 0;
            else
                return ownerTeam.TeamId;
        }

        #endregion
    }
}
