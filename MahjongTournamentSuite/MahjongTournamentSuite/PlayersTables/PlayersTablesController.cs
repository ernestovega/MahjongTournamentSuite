using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.PlayersTables
{
    class PlayersTablesController : IPlayersTablesController
    {
        #region Fields

        private IPlayersTablesForm _form;
        private IPlayersTablesDataManager _data;
        private VTournament _tournament;
        private List<VPlayer> _players;
        private List<VTable> _tables;

        #endregion

        #region Constructor

        public PlayersTablesController(IPlayersTablesForm form)
        {
            _form = form;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region IPlayersTablesController implementation

        public void LoadForm(int tournamentId)
        {
            _tournament = _data.GetTournament(tournamentId);
            _players = _data.GetTournamentPlayers(tournamentId);
            _tables = _data.GetTournamentTables(tournamentId);
            _form.GeneratePlayersButtons(_players.Count);
        }

        public void ButtonPlayerClicked(int playerId)
        {
            List<DGVPlayerTable> dgvPlayerTables = new List<DGVPlayerTable>(_tournament.NumRounds);
            for (int i = 1; i <= _tournament.NumRounds; i++)
            {
                int tableId = _tables.Find(x => x.TableRoundId == i &&
                    (x.Player1Id == playerId || x.Player2Id == playerId ||
                    x.Player3Id == playerId || x.Player4Id == playerId))
                    .TableId;
                dgvPlayerTables.Add(new DGVPlayerTable(i, tableId));
            }
            string playerName = _players.Find(x => x.PlayerId == playerId).PlayerName;

            _form.ShowPlayerTables(new PlayerTables(playerId, playerName, dgvPlayerTables));
        }

        #endregion
    }
}
