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
                _form.FillDGVWithTeams(_teams);
            }
            else
            {
                _form.FillDGVWithPlayers(_players, _tournament.IsTeams);
            }
        }

        public void OnFormResized()
        {

        }

        public void ButtonTeamsClicked()
        {
            _form.HideRoundsButtonsAndTablesPanel();
            _form.ShowDGV();
            _form.FillDGVWithTeams(_teams);
        }

        public void ButtonPlayersClicked()
        {
            _form.HideRoundsButtonsAndTablesPanel();
            _form.ShowDGV();
            _form.FillDGVWithPlayers(_players, _tournament.IsTeams);
        }

        public void ButtonRoundsClicked()
        {
            _form.HideDGV();
            _form.ShowRoundsButtonsAndTablesPanel();
            _form.EmptyPanelRoundsButtons();
            _form.AddRoundsSubButtons(_tournament.NumRounds);
            _form.EmptyPanelRoundTablesButtons();
            _form.AddRoundTablesButtons(1, _tournament.NumPlayers / 4);
            _form.FillDGVWithPlayers(_players, _tournament.IsTeams);
        }

        public void ButtonRoundClicked(int tag)
        {
            _form.EmptyPanelRoundTablesButtons();
            _form.HideDGV();
            _form.AddRoundTablesButtons(tag, _tournament.NumPlayers / 4);
        }

        #endregion
    }
}
