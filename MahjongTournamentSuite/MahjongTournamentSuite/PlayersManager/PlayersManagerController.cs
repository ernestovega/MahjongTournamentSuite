using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using System;
using MahjongTournamentSuite.ViewModel;
using MahjongTournamentSuite.Resources.flags;

namespace MahjongTournamentSuite.PlayersManager
{
    class PlayersManagerController : IPlayersManagerController
    {
        #region Fields

        private IPlayersManagerForm _form;
        private IPlayersManagerDataManager _data;
        private VTournament _tournament;
        private List<VPlayer> _players;
        private List<VTeam> _teams;
        private List<VCountry> _countries;

        #endregion

        #region Constructor

        public PlayersManagerController(IPlayersManagerForm playersManagerForm)
        {
            _form = playersManagerForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountryManagerController implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _data.GetTournament(tournamentId);
            _players = _data.GetTournamentPlayers(tournamentId);
            _teams = _tournament.IsTeams ? _data.GetTournamentTeams(tournamentId) : new List<VTeam>();
            _countries = _data.GetCountries();

            List<DGVPlayer> dgvPlayers = new List<DGVPlayer>(_players.Count);
            foreach (VPlayer player in _players)
            {
                dgvPlayers.Add(new DGVPlayer(player, getPlayerTeamName(player.PlayerTeamId), 
                    CountryFlags.GetFlagImage(player.PlayerCountryName)));
            }

            _form.FillDGV(dgvPlayers, _tournament.IsTeams);
            CheckWrongPlayersTeams();
        }

        public void PlayerNameChanged(int playerId, string newPlayerName)
        {
            int ownerPlayerId = GetOwnerPlayerNameId(newPlayerName);
            if (ownerPlayerId == 0)
            {
                _data.UpdatePlayerName(_tournament.TournamentId, playerId, newPlayerName);
                return;
            }
            _form.PlayKoSound();
            _form.DGVCancelEdit();
            _form.ShowMessagePlayerNameInUse(newPlayerName, ownerPlayerId);
        }

        public int SaveNewPlayerTeam(int playerId, string newTeamName)
        {
            int newTeamId = GetTeamId(newTeamName);
            if (newTeamId > 0)
            {
                _data.UpdatePlayerTeam(_tournament.TournamentId, playerId, newTeamId);
                return newTeamId;
            }
            _form.PlayKoSound();
            _form.ShowMessageTeamError();
            return 0;
        }

        public void CheckWrongPlayersTeams()
        {
            List<WrongTeam> wrongTeams = GetWrongTeams();
            if (wrongTeams.Count > 0)
            {
                _form.PlayKoSound();
                _form.MarkWrongTeamsPlayers(wrongTeams);
                _form.ShowWrongNumberOfPlayersPerTeamMessage(wrongTeams);
                return;
            }
            _form.CleanWrongTeamsPlayers();
        }

        public void AssignNewEmaPlayer(int playerId, string playerEmaNumber)
        {
            _data.AssignNewEmaPlayer(_tournament.TournamentId, playerId, playerEmaNumber);
        }

        public void UnassignEmaPlayer(int playerId)
        {
            _data.UnassignEmaPlayer(_tournament.TournamentId, playerId);
        }

        public bool IsWrongPlayersTeams()
        {
            return _tournament.IsTeams && GetWrongTeams().Count > 0;
        }

        #endregion

        #region Private

        private string getPlayerTeamName(int teamId)
        {
            if (_tournament.IsTeams && teamId > 0)
                return _teams.Find(x => x.TeamId == teamId).TeamName;
            else
                return "";
        }
        
        private int GetOwnerPlayerNameId(string newName)
        {
            VPlayer ownerPlayer = _players.Find(x => x.PlayerName.Equals(newName,
                StringComparison.InvariantCulture));
            if (ownerPlayer == null)
                return 0;
            else
                return ownerPlayer.PlayerId;
        }

        private int GetTeamId(string newTeamName)
        {
            VTeam team = _teams.Find(x => x.TeamName == newTeamName);
            return team == null ? 0 : team.TeamId;
        }

        private List<WrongTeam> GetWrongTeams()
        {
            _players = _data.GetTournamentPlayers(_tournament.TournamentId);
            List<WrongTeam> wrongTeams = new List<WrongTeam>();
            foreach (VTeam team in _teams)
            {
                List<VPlayer> teamPlayers = _players.FindAll(
                    x => x.PlayerTournamentId == _tournament.TournamentId
                    && x.PlayerTeamId == team.TeamId);
                if (teamPlayers.Count == 0)
                    wrongTeams.Add(new WrongTeam(team.TeamId, team.TeamName, 0));
                else if (teamPlayers.Count != 4)
                    wrongTeams.Add(new WrongTeam(team.TeamId, team.TeamName, teamPlayers.Count));
            }
            return wrongTeams;
        }

        #endregion
    }
}
