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
        private DBTable _table;
        private List<DBPlayer> _tablePlayers;
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

        public void LoadForm(int tournamentId, int roundId, int tableId)
        {
            _table = _db.GetTable(tournamentId, roundId, tableId);
            _tablePlayers = _db.GetTablePlayers(_table.TournamentId, _table.RoundId, _table.Id);
            _hands = _db.GetTableHands(tournamentId, roundId, tableId);
            _form.SetTournamentName(_db.GetTournamentName(tournamentId));
            _form.SetRoundId(roundId);
            _form.SetTableId(tableId);
            FillDataGridHands();
            if(FillPlayerHeaders())
            {
                _form.ShowDataGridHands();
                CalculateAndFillAllScores();
            }
            else
            {
                FillCombosPlayers();
            }
        }

        public void NameEastPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerEastId = selectedPlayerId;
            IfFillHeadersThenSavePlayersPositionsAndShowDataGridHands();
        }

        public void NameSouthPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerSouthId = selectedPlayerId;
            IfFillHeadersThenSavePlayersPositionsAndShowDataGridHands();
        }

        public void NameWestPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerWestId = selectedPlayerId;
            IfFillHeadersThenSavePlayersPositionsAndShowDataGridHands();
        }

        public void NameNorthPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerNorthId = selectedPlayerId;
            IfFillHeadersThenSavePlayersPositionsAndShowDataGridHands();
        }

        public void playerWinnerIdChanged(int handId, int newPlayerWinnerId)
        {
            DBHand hand = _hands.Find(x => x.Id == handId);
            hand.PlayerWinnerId = newPlayerWinnerId;
            if (hand.PlayerLooserId > 0 && hand.Score > -1)
            {
                CalculateAndFillAllScores();
            }
            _db.UpdateHand(hand);
            _form.RefreshDataGridHands(_hands);
        }

        public void playerLooserIdChanged(int handId, int newPlayerLooserId)
        {
            DBHand hand = _hands.Find(x => x.Id == handId);
            hand.PlayerLooserId = newPlayerLooserId;
            if (hand.PlayerWinnerId > 0 && hand.Score > -1)
            {
                CalculateAndFillAllScores();
            }
            _db.UpdateHand(hand);
            _form.RefreshDataGridHands(_hands);
        }

        public void PointsChanged(int handId, int newPoints)
        {
            DBHand hand = _hands.Find(x => x.Id == handId);
            hand.Score = newPoints;
            if (hand.PlayerWinnerId > 0 && hand.PlayerLooserId > 0)
            {
                CalculateAndFillAllScores();
                _form.RefreshDataGridHands(_hands);
            }
            _db.UpdateHand(hand);
        }

        public void IsChickenHandChanged(int handId, bool cellValue)
        {
            DBHand hand = _hands.Find(x => x.Id == handId);
            hand.IsChickenHand = cellValue;
            _db.UpdateHand(hand);
        }

        #endregion

        #region Private

        private void FillDataGridHands()
        {
            List<DataGridHand> dataGridHands = new List<DataGridHand>();
            foreach(DBHand hand in _hands)
            {
                dataGridHands.Add(new DataGridHand(hand, 0, 0, 0, 0));
            }
            _form.FillDataGridHands(dataGridHands);
        }

        private bool FillPlayerHeaders()
        {
            if (_table.PlayerEastId > 0 && _table.PlayerSouthId > 0 && _table.PlayerWestId > 0 && _table.PlayerNorthId > 0)
            {
                _form.SetEastPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerEastId).Name);
                _form.SetSouthPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerSouthId).Name);
                _form.SetWestPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerWestId).Name);
                _form.SetNorthPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerSouthId).Name);
                return true;
            }
            return false;
        }

        private void FillCombosPlayers()
        {
            List<ComboItem> comboPlayers = new List<ComboItem>(5);
            comboPlayers.Add(new ComboItem("Select player", "-1"));
            foreach (DBPlayer player in _tablePlayers)
            {
                comboPlayers.Add(new ComboItem(string.Format("{0} - {1}", player.Id, player.Name), player.Id.ToString()));
            }
            _form.FillCombosPlayers(comboPlayers);
        }

        private void IfFillHeadersThenSavePlayersPositionsAndShowDataGridHands()
        {
            if (FillPlayerHeaders())
            {
                _db.UpdateTablePlayersPositions(_table);
                _form.ShowDataGridHands();
            }
        }

        private void CalculateAndFillAllScores()
        {
            foreach (DBHand hand in _hands)
            {
                if(hand.Score < 0)
                {
                    return;
                }
                else if (hand.Score == 0)//Washout
                {
                    _form.FillPlayersHandScores(hand.Id, 0, 0, 0, 0);
                }
                else
                {
                    if(hand.PlayerLooserId > 0)//Ron
                    {
                        //_form.FillPlayersHandScores(hand.Id, 0, 0, 0, 0);
                    }
                    else//Tsumo
                    {
                        //_form.FillPlayersHandScores(hand.Id, 0, 0, 0, 0);
                    }
                }
                CalculateTotalScores();
            }
            
        }

        private void CalculateTotalScores()
        {
            
        }

        #endregion
    }
}
