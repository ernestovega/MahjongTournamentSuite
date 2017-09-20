using MahjongTournamentSuiteDataLayer.Model;
using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Data;
using System;

namespace MahjongTournamentSuite.PlayersTables
{
    class PlayersTablesPresenter : IPlayersTablesPresenter
    {
        #region Fields

        private IPlayersTablesForm _form;
        private IDBManager _db;
        private DBTournament _tournament;
        private List<DBPlayer> _players;
        private List<DBTable> _tables;

        #endregion

        #region Constructor

        public PlayersTablesPresenter(IPlayersTablesForm form)
        {
            _form = form;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region IPlayersTablesPresenter implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _db.GetTournament(tournamentId);
            _players = _db.GetTournamentPlayers(tournamentId);
            _tables = _db.GetTournamentTables(tournamentId);
            _form.GeneratePlayersButtons(_players.Count);
        }

        public void ButtonPlayerClicked(int playerId)
        {
            List<int> tablesPlayer = new List<int>(_tournament.NumRounds);
            for (int i = 1; i <= _tournament.NumRounds; i++)
            {
                tablesPlayer.Add(_tables.Find(x => x.TableRoundId == i &&
                    (x.Player1Id == playerId || x.Player2Id == playerId ||
                    x.Player3Id == playerId || x.Player4Id == playerId))
                    .TableId);
            }
            _form.ShowMessageBoxTablesPlayer(playerId, tablesPlayer);
        }

        #endregion
    }
}
