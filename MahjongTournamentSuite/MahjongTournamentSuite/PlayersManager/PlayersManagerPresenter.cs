using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using System;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.PlayersManager
{
    class PlayersManagerPresenter : IPlayersManagerPresenter
    {
        #region Fields

        private IPlayersManagerForm _form;
        private IDBManager _db;
        private DBTournament _tournament;
        private List<DBPlayer> _players;
        private List<DBTeam> _teams;
        private List<DBCountry> _countries;

        #endregion

        #region Constructor

        public PlayersManagerPresenter(IPlayersManagerForm playersManagerForm)
        {
            _form = playersManagerForm;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ICountryManagerPresenter implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            if (_tournament.IsTeams)
                _teams = _db.GetTournamentTeams(tournamentId);
            _countries = _db.GetCountries();

            List<DGVPlayer> dgvPlayers = new List<DGVPlayer>(_players.Count);
            foreach (DBPlayer player in _players)
            {
                dgvPlayers.Add(new DGVPlayer(player, getPlayerTeamName(player.PlayerTeamId), getPlayerCountryName(player.PlayerCountryId)));
            }

            _form.FillDGV(dgvPlayers, _tournament.IsTeams);
        }

        public void PlayerNameChanged(int playerId, string newPlayerName)
        {
            int ownerPlayerId = GetOwnerPlayerNameId(newPlayerName);
            if (ownerPlayerId == 0)
                _db.UpdatePlayerName(_tournament.TournamentId, playerId, newPlayerName);
            else
            {
                _form.DGVCancelEdit();
                _form.ShowMessagePlayerNameInUse(newPlayerName, ownerPlayerId);
            }
        }

        public int SaveNewPlayerTeam(int playerId, string newTeamName)
        {
            int newTeamId = GetTeamId(newTeamName);
            if (newTeamId > 0)
            {
                _db.UpdatePlayerTeam(_tournament.TournamentId, playerId, newTeamId);
                return newTeamId;
            }
            _form.ShowMessageTeamError();
            return 0;
        }

        public void PlayerTeamChanged()
        {
            List<WrongTeam> wrongTeams = GetWrongTeams();
            if (wrongTeams.Count > 0)
            {
                _form.MarkWrongTeamsPlayers(wrongTeams);
                _form.ShowWrongNumberOfPlayersPerTeamMessage(wrongTeams);
            }
            else
                _form.CleanWrongTeamsPlayers();
        }

        public int SaveNewPlayerCountry(int playerId, string newCountryName)
        {
            throw new NotImplementedException();
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

        private string getPlayerCountryName(int countryId)
        {
            return _db.GetCountryName(countryId);
        }
        
        private int GetOwnerPlayerNameId(string newName)
        {
            DBPlayer ownerPlayer = _players.Find(x => x.PlayerName.Equals(newName,
                StringComparison.InvariantCulture));
            if (ownerPlayer == null)
                return 0;
            else
                return ownerPlayer.PlayerId;
        }

        private int GetTeamId(string newTeamName)
        {
            DBTeam team = _teams.Find(x => x.TeamName == newTeamName);
            return team == null ? 0 : team.TeamId;
        }

        public int GetCountryId(string countryName)
        {
            DBCountry country = _countries.Find(x => x.CountryName == countryName);
            if (country == null)
                return 0;
            else
                return country.CountryId;
        }

        private List<WrongTeam> GetWrongTeams()
        {
            List<WrongTeam> wrongTeams = new List<WrongTeam>();
            foreach (DBTeam team in _teams)
            {
                List<DBPlayer> teamPlayers = _players.FindAll(
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
