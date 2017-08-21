﻿using MahjongTournamentSuite.Data;
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
                if (IsTeamsFilledByUser())
                {
                    _form.ShowButtonPlayers();
                    if (IsPlayersFilledByUser())
                    {
                        _form.ShowButtonRounds();
                        ButtonRoundsClicked();
                    }
                    else
                        ButtonPlayersClicked();
                }
                else
                    ButtonTeamsClicked();
            }
            else
            {
                _form.ShowButtonPlayers();
                if (IsPlayersFilledByUser())
                {
                    _form.ShowButtonRounds();
                    ButtonRoundsClicked();
                }
                else
                    ButtonPlayersClicked();
            }
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
            _form.FillDGVWithPlayers(_players, _tournament.IsTeams);
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
            _form.GoToTableManager(_tournament.Id, roundSelected, tableId);
        }

        public void TeamNameChanged(int tournamentId, int teamId, string newName)
        {
            int ownerTeamId = GetOwnerTeamNameId(newName);
            if (ownerTeamId > 0)
            {
                _form.DGVCancelEdit();
                _form.ShowMessageTeamNameInUse(newName, ownerTeamId);
                return;
            }
            _db.UpdateTeamName(tournamentId, teamId, newName);//Comprobar que _teams ha cambiado
            if(IsTeamsFilledByUser())
                _form.ShowButtonPlayers();
        }

        public void PlayerNameChanged(int tournamentId, int playerId, string newName)
        {
            int ownerPlayerId = GetOwnerPlayerNameId(newName);
            if (ownerPlayerId > 0)
            {
                _form.DGVCancelEdit();
                _form.ShowMessagePlayerNameInUse(newName, ownerPlayerId);
                return;
            }
            _db.UpdatePlayerName(tournamentId, playerId, newName);//Comprobar que _teams ha cambiado
            if (IsPlayersFilledByUser())
                _form.ShowButtonRounds();
        }

        #endregion

        #region Private

        /// <summary>
        /// Si los nombres de los equipos no son números enteros, es que están rellenos.
        /// </summary>
        /// <returns></returns>
        private bool IsTeamsFilledByUser()
        {
            foreach (DBTeam team in _teams)
            {
                int parseResult;
                if (int.TryParse(team.Name, out parseResult))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Si los nombres de los jugadores no son números enteros, es que están rellenos.
        /// </summary>
        /// <returns></returns>
        private bool IsPlayersFilledByUser()
        {
            foreach (DBPlayer player in _players)
            {
                int parseResult;
                if (int.TryParse(player.Name, out parseResult))
                    return false;
            }
            return true;
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
        
        /// <summary>
        /// Look for a team that were using the same name. 
        /// </summary>
        /// <param name="newName">Name to search</param>
        /// <returns>0 if doesn´t find it, the TeamId if yes.</returns>
        private int GetOwnerTeamNameId(string newName)
        {
            DBTeam ownerTeam = _teams.Find(x => x.Name.Equals(newName, 
                StringComparison.InvariantCultureIgnoreCase));
            if (ownerTeam == null)
                return 0;
            else
                return ownerTeam.Id;
        }

        /// <summary>
        /// Look for a player that were using the same name. 
        /// </summary>
        /// <param name="newName">Name to search</param>
        /// <returns>0 if doesn´t find it, the PlayerId if yes.</returns>
        private int GetOwnerPlayerNameId(string newName)
        {
            DBPlayer ownerPlayer = _players.Find(x => x.Name.Equals(newName, 
                StringComparison.InvariantCultureIgnoreCase));
            if (ownerPlayer == null)
                return 0;
            else
                return ownerPlayer.Id;
        }

        #endregion
    }
}
