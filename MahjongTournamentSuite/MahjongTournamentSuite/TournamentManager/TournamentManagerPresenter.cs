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
                isTeamsSelected = true;
                _form.SelectTeamsButton();
                _form.FillDGVWithTeams(_teams);
            }
            else
            {
                isPlayersSelected = true;
                _form.SelectPlayersButton();
                _form.FillDGVWithPlayers(_players, _tournament.IsTeams);
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

        #endregion

        #region Private

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
