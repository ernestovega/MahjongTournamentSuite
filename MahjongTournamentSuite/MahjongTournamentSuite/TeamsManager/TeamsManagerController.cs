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
            _form.FillDGVTeams(_teams);
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
        private int GetOwnerTeamNameId(string newName)
        {
            VTeam ownerTeam = _teams.Find(x => x.TeamName.Equals(newName, StringComparison.InvariantCulture));
            if (ownerTeam == null)
                return 0;
            else
                return ownerTeam.TeamId;
        }

        public void LoadTeamPlayers(int teamId)
        {
            List<VPlayer> teamPlayers = _data.GetTeamPlayers(_tournament.TournamentId, teamId);
            List<DGVTeamPlayer> dgvTeamPlayers = new List<DGVTeamPlayer>(teamPlayers.Count);
            foreach(VPlayer teamPlayer in teamPlayers)
            {
                dgvTeamPlayers.Add(new DGVTeamPlayer(teamPlayer.PlayerId, teamPlayer.PlayerName));
            }
            _form.FillDGVTeamPlayers(dgvTeamPlayers);
        }

        public void AssignTeamPlayer(int tournamentId, int playerId, int teamId)
        {
            _data.AssignTeamPlayer(tournamentId, playerId, teamId);
        }

        public void UnassignTeamPlayer(int tournamentId, int teamPlayerId)
        {
            _data.UnassignTeamPlayer(tournamentId, teamPlayerId);
        }

        #endregion

        #region Private


        #endregion
    }
}
