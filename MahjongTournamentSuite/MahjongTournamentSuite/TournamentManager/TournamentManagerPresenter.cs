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
            _form.ShowDGV();
            if(_tournament.IsTeams)
            {
                _teams = _db.GetTournamentTeams(tournamentId);
                _form.AddButtonTeams();
                _form.FillDGVWithTeams(_teams);
            }
            else
            {
                _form.FillDGVWithPlayers(_players);
            }
            _form.AddPlayersButton();
            _form.AddRoundsButtons(_tournament.NumRounds);
        }

        public void OnFormResized()
        {

        }

        public void ButtonTeamsClicked()
        {
            _form.EmptyPanelTournament();
            _form.ShowDGV();
            _form.FillDGVWithTeams(_teams);
        }

        public void ButtonPlayersClicked()
        {
            _form.EmptyPanelTournament();
            _form.ShowDGV();
            _form.FillDGVWithPlayers(_players);
        }

        public void ButtonRoundClicked(int tag)
        {
            _form.EmptyPanelTournament();
            _form.HideDGV();
            _form.FillPanelTournamentWithRoundButtons(tag, _tournament.NumPlayers / 4);
        }

        #endregion
    }
}
