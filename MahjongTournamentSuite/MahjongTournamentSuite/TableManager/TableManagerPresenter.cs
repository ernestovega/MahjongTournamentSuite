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
            FillCombosPlayers();
            FillDataGridHands();
            if(FillPlayerHeaders(false))
            {
                _form.ShowDataGridHands();
                CalculateAndFillAllScores();
            }
        }

        public void NameEastPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerEastId = selectedPlayerId;
            if (FillPlayerHeaders(true))
                _form.ShowDataGridHands();
            else
                _form.HideDataGridHands();
        }

        public void NameSouthPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerSouthId = selectedPlayerId;
            if (FillPlayerHeaders(true))
                _form.ShowDataGridHands();
            else
                _form.HideDataGridHands();
        }

        public void NameWestPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerWestId = selectedPlayerId;
            if (FillPlayerHeaders(true))
                _form.ShowDataGridHands();
            else
                _form.HideDataGridHands();
        }

        public void NameNorthPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerNorthId = selectedPlayerId;
            if (FillPlayerHeaders(true))
                _form.ShowDataGridHands();
            else
                _form.HideDataGridHands();
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
            FillDataGridHands();
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
            FillDataGridHands();
        }

        public void PointsChanged(int handId, int newPoints)
        {
            DBHand hand = _hands.Find(x => x.Id == handId);
            hand.Score = newPoints;
            if (hand.PlayerWinnerId > 0 && hand.PlayerLooserId > 0)
            {
                CalculateAndFillAllScores();
                FillDataGridHands();
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

        private bool FillPlayerHeaders(bool shouldSave)
        {
            if (IsAllPlayersIdsSelected() && !IsPlayerIdRepeated())
            {
                if (shouldSave)
                {
                    _db.UpdateTablePlayersPositions(_table);
                }
                _form.SetEastPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerEastId).Name);
                _form.SetSouthPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerSouthId).Name);
                _form.SetWestPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerWestId).Name);
                _form.SetNorthPlayerHeader(_tablePlayers.Find(x => x.Id == _table.PlayerNorthId).Name);
                return true;
            }
            return false;
        }

        private bool IsAllPlayersIdsSelected()
        {
            return _table.PlayerEastId > 0 && _table.PlayerSouthId > 0
                && _table.PlayerWestId > 0 && _table.PlayerNorthId > 0;
        }

        private bool IsPlayerIdRepeated()
        {
            return _table.PlayerEastId == _table.PlayerSouthId ||
                _table.PlayerEastId == _table.PlayerWestId ||
                _table.PlayerEastId == _table.PlayerNorthId ||
                _table.PlayerSouthId == _table.PlayerWestId ||
                _table.PlayerSouthId == _table.PlayerNorthId ||
                _table.PlayerWestId == _table.PlayerNorthId;
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
            if(IsAllPlayersIdsSelected())
            {
                _form.SelectPlayersInCombos(GetPlayerEastIndex(), GetPlayerSouthIndex(), GetPlayerWestIndex(), GetPlayerNorthIndex());
            }
        }

        private int GetPlayerEastIndex()
        {
            if (_table.PlayerEastId == _table.Player1Id)
                return 1;
            else if (_table.PlayerEastId == _table.Player2Id)
                return 2;
            else if (_table.PlayerEastId == _table.Player3Id)
                return 3;
            else
                return 4;
        }

        private int GetPlayerSouthIndex()
        {
            if (_table.PlayerSouthId == _table.Player1Id)
                return 1;
            else if (_table.PlayerSouthId == _table.Player2Id)
                return 2;
            else if (_table.PlayerSouthId == _table.Player3Id)
                return 3;
            else
                return 4;
        }

        private int GetPlayerWestIndex()
        {
            if (_table.PlayerWestId == _table.Player1Id)
                return 1;
            else if (_table.PlayerWestId == _table.Player2Id)
                return 2;
            else if (_table.PlayerWestId == _table.Player3Id)
                return 3;
            else
                return 4;
        }

        private int GetPlayerNorthIndex()
        {
            if (_table.PlayerNorthId == _table.Player1Id)
                return 1;
            else if (_table.PlayerNorthId == _table.Player2Id)
                return 2;
            else if (_table.PlayerNorthId == _table.Player3Id)
                return 3;
            else
                return 4;
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
