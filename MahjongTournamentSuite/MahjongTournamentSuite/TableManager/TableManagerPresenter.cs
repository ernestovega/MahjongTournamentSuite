using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;

namespace MahjongTournamentSuite.TableManager
{
    class TableManagerPresenter : ITableManagerPresenter
    {
        #region Fields

        private ITableManagerForm _form;
        private IDBManager _db;
        private List<DBPlayer> _tablePlayers;
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
            _tablePlayers = _db.GetTablePlayers(tournamentId, roundId, tableId);
            List<ComboItem> comboPlayers = new List<ComboItem>(5);
            comboPlayers.Add(new ComboItem("Select player", "0"));
            foreach (DBPlayer player in _tablePlayers)
            {
                comboPlayers.Add(new ComboItem(string.Format("{0} - {1}", player.Id, player.Name), player.Id.ToString()));
            }
            _form.FillCombosPlayers(comboPlayers);
            _table = _db.GetTable(tournamentId, roundId, tableId);
            _hands = _db.GetTableHands(tournamentId, roundId, tableId);
            _form.FillDataGridHands(_hands);

        }

        public void NameEastPlayerChanged(string selectedValue)
        {
            _form.SetDataGridHeaderEastPlayerText(selectedValue);
        }

        public void NameSouthPlayerChanged(string selectedValue)
        {
            _form.SetDataGridHeaderSouthPlayerText(selectedValue);
        }

        public void NameWestPlayerChanged(string selectedValue)
        {
            _form.SetDataGridHeaderWestPlayerText(selectedValue);
        }

        public void NameNorthPlayerChanged(string selectedValue)
        {
            _form.SetDataGridHeaderNorthPlayerText(selectedValue);
        }

        #endregion

        #region Private

        #endregion
    }
}
