using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TableManager
{
    class TableManagerPresenter : ITableManagerPresenter
    {
        #region Fields

        private ITableManagerForm _form;
        private IDBManager _db;
        private List<DBPlayer> _players;
        private DBTable _table;
        private List<DBHand> _hands;

        #endregion

        #region Constructor

        public TableManagerPresenter(ITableManagerForm form)
        {
            _form = form;
            _db = Injector.provideDBManager();
        }

        #endregion

        #region ITournamentManagerPresenter

        public void LoadTable(int tournamentId, int roundId, int tableId)
        {
            _form.SetTournamentName(_db.GetTournamentName(tournamentId));
            _form.SetRoundId(roundId);
            _form.SetTableId(tableId);
            _players = _db.GetTournamentPlayers(tournamentId);
            List<ComboItem> comboPlayers = new List<ComboItem>();
            foreach(DBPlayer player in _players)
            {
                comboPlayers.Add(new ComboItem(player.Name, player.Id.ToString()));
            }
            _form.FillCombosPlayers(comboPlayers);
            _table = _db.GetTable(tournamentId, roundId, tableId);
            _hands = _db.GetTableHands(tournamentId, roundId, tableId);
            _form.FillDataGridHands(_hands);

        }

        #endregion

        #region Private

        #endregion
    }
}
