using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;

namespace MahjongTournamentSuite.TournamentManager
{
    class TournamentManagerPresenter : ITournamentManagerPresenter
    {
        #region Fields

        private ITournamentManagerForm _form;
        private IDBManager _db;
        private DBTournament _tournament;
        private List<DBTeam> _teams;
        private List<DBPlayer> _players;
        private bool isTeamsSelected = false;
        private bool isPlayersSelected = false;
        private bool isRoundsSelected = false;
        private int roundSelected = 0;
        private int tableSelected = 0;

        #endregion

        #region Constructor

        public TournamentManagerPresenter(ITournamentManagerForm form)
        {
            _form = form;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ITournamentManagerPresenter

        public void LoadTournament(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            if(_tournament.IsTeams)
            {
                _teams = _db.GetTournamentTeams(tournamentId);
                _form.ShowButtonTeams();
            }
            ButtonRoundsClicked();
        }

        public void OnFormResized()
        {
            if(isRoundsSelected)
            {
                _form.EmptyPanelRoundsButtons();
                _form.AddRoundsSubButtons(_tournament.NumRounds);
                _form.EmptyPanelRoundTablesButtons();
                _form.AddRoundTablesButtons(roundSelected, _tournament.NumPlayers / 4);
                _form.SelectRoundButton(roundSelected);
                if(tableSelected > 0)
                    _form.SelectRoundTableButton(tableSelected);
            }
        }

        public void ButtonTeamsClicked()
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            UnselectRounds();
            UnselectPlayers();
            isTeamsSelected = true;
            _form.SelectTeamsButton();
            _form.HideRoundsButtonsAndTablesPanel();
            _form.ShowDGV();
            _form.FillDGVWithTeams(_teams);
        }

        public void ButtonPlayersClicked()
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            UnselectRounds();
            UnselectTeams();
            isPlayersSelected = true;
            _form.SelectPlayersButton();
            _form.HideRoundsButtonsAndTablesPanel();
            _form.ShowDGV();            
            _form.FillDGVWithPlayers(GetDataGridPlayers(), _tournament.IsTeams);
        }

        public void ButtonRoundsClicked()
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            UnselectPlayers();
            UnselectTeams();
            isRoundsSelected = true;
            _form.SelectRoundsButton();
            _form.HideDGV();
            _form.ShowRoundsButtonsAndTablesPanel();
            _form.EmptyPanelRoundsButtons();
            _form.AddRoundsSubButtons(_tournament.NumRounds);
            _form.EmptyPanelRoundTablesButtons();
            roundSelected = 1;
            _form.AddRoundTablesButtons(roundSelected, _tournament.NumPlayers / 4);
            _form.SelectRoundButton(roundSelected);
        }

        public void ButtonRoundClicked(int roundId)
        {
            UnselectTable(tableSelected);
            UnselectRound(roundSelected);
            roundSelected = roundId;
            _form.SelectRoundButton(roundId);
            _form.EmptyPanelRoundTablesButtons();
            _form.AddRoundTablesButtons(roundId, _tournament.NumPlayers / 4);
        }

        public void ButtonRoundTableClicked(int tableId)
        {
            UnselectTable(tableSelected);
            tableSelected = tableId;
            _form.SelectRoundTableButton(tableId);
            _form.GoToTableManager(_tournament.TournamentId, roundSelected, tableId);
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

        public void PlayerNameChanged(int playerId, string newName)
        {
            int ownerPlayerId = GetOwnerPlayerNameId(newName);
            if (ownerPlayerId == 0)
                _db.UpdatePlayerName(_tournament.TournamentId, playerId, newName);
            else
            {
                _form.DGVCancelEdit();
                _form.ShowMessagePlayerNameInUse(newName, ownerPlayerId);
                return;
            }
        }

        public string PlayerCountryChanged(int playerId, string newCountryName)
        {
            int newCountryId = _db.GetCountryId(newCountryName);
            if (newCountryId > 0)
            {
                _db.UpdatePlayerCountry(_tournament.TournamentId, playerId, newCountryId);
                return newCountryName;
            }
            else
            {
                _form.ShowMessageCountryError();
                return "";
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Si los nombres de los jugadores no son números enteros, es que están rellenos.
        /// </summary>
        /// <returns></returns>
        private bool IsPlayersFilledByUser()
        {
            foreach (DBPlayer player in _players)
            {
                int parseResult;
                if (int.TryParse(player.PlayerName, out parseResult))
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Look for a team that were using the same name. 
        /// </summary>
        /// <param name="newName">Name to search</param>
        /// <returns>0 if doesn´t find it, the TeamId if yes.</returns>
        private int GetOwnerTeamNameId(string newName)
        {
            DBTeam ownerTeam = _teams.Find(x => x.TeamName.Equals(newName, 
                StringComparison.InvariantCulture));
            if (ownerTeam == null)
                return 0;
            else
                return ownerTeam.TeamId;
        }

        /// <summary>
        /// Look for a player that were using the same name. 
        /// </summary>
        /// <param name="newName">Name to search</param>
        /// <returns>0 if doesn´t find it, the PlayerId if yes.</returns>
        private int GetOwnerPlayerNameId(string newName)
        {
            DBPlayer ownerPlayer = _players.Find(x => x.PlayerName.Equals(newName, 
                StringComparison.InvariantCulture));
            if (ownerPlayer == null)
                return 0;
            else
                return ownerPlayer.PlayerId;
        }

        private List<DGVPlayer> GetDataGridPlayers()
        {
            List<DGVPlayer> dgPlayers = new List<DGVPlayer>(_players.Count);
            foreach (DBPlayer player in _players)
            {
                dgPlayers.Add(new DGVPlayer(player, 
                    getPlayerTeamName(player.PlayerTeamId), getPlayerCountryName(player.PlayerCountryId)));
            }
            return dgPlayers;
        }

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

        private void UnselectTeams()
        {
            _form.UnselectTeamsButton();
            isTeamsSelected = false;
        }

        private void UnselectPlayers()
        {
            _form.UnselectPlayersButton();
            isPlayersSelected = false;
        }

        private void UnselectRounds()
        {
            _form.UnselectRoundsButton();
            isRoundsSelected = false;
        }

        private void UnselectRound(int roundId)
        {
            if (roundId > 0)
            {
                _form.UnselectRoundButton(roundId);
                roundSelected = 0;
            }
        }

        private void UnselectTable(int tableId)
        {
            if (tableId > 0)
            {
                _form.UnselectTableButton(tableId);
                tableSelected = 0;
            }
        }

        #endregion
    }
}
