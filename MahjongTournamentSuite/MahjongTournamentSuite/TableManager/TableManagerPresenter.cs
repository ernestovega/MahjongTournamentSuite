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
            _tablePlayers = _db.GetTablePlayers(_table.TableTournamentId, _table.TableRoundId, _table.TableId);
            _hands = _db.GetTableHands(tournamentId, roundId, tableId);
            _form.SetTournamentName(_db.GetTournamentName(tournamentId));
            _form.SetRoundId(roundId);
            _form.SetTableId(tableId);
            FillCombosPlayers();
            FillDataGridHands();
            if(FillPlayerHeaders(false))
            {
                _form.ShowDataGridHands();
                CalculateAndFillAllScoresIfHandIsComplete();
            }
        }

        public void NameEastPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerEastId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public void NameSouthPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerSouthId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public void NameWestPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerWestId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public void NameNorthPlayerChanged(int selectedPlayerId)
        {
            _table.PlayerNorthId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public bool PlayerWinnerIdChanged(int handId, string sNewPlayerWinnerId)
        {
            if (IsValidPlayerId(sNewPlayerWinnerId))
            {
                _db.UpdateHandWinnerId(_hands.Find(x => x.HandId == handId), sNewPlayerWinnerId);
                CalculateAndFillAllScoresIfHandIsComplete();
                return true;
            }
            _form.DGVCancelEdit();
            return false;
        }

        public bool PlayerLooserIdChanged(int handId, string sNewPlayerLooserId)
        {
            if (IsValidPlayerId(sNewPlayerLooserId))
            {
                _db.UpdateHandLooserId(_hands.Find(x => x.HandId == handId), sNewPlayerLooserId);
                CalculateAndFillAllScoresIfHandIsComplete();
                return true;
            }
            _form.DGVCancelEdit();
            return false;
        }

        public bool HandScoreChanged(int handId, string sNewHandScore)
        {
            if (IsValidScore(sNewHandScore))
            {
                _db.UpdateHandScore(_hands.Find(x => x.HandId == handId), sNewHandScore);
                CalculateAndFillAllScoresIfHandIsComplete();
                return true;
            }
            _form.DGVCancelEdit();
            return false;
        }

        public void IsChickenHandChanged(int handId, bool newIsChickenHand)
        {
            _db.UpdateHandIsChickenHand(_hands.Find(x => x.HandId == handId), newIsChickenHand);
        }

        #endregion

        #region Private

        private void FillDataGridHands()
        {
            List<DGVHand> dgvHands = new List<DGVHand>();
            foreach(DBHand hand in _hands)
                dgvHands.Add(new DGVHand(hand));

            _form.FillDGV(dgvHands);
        }

        private void ShowDGVIfAllPlayersPositionsSelected()
        {
            if (FillPlayerHeaders(true))
                _form.ShowDataGridHands();
            else
                _form.HideDataGridHands();
        }

        private bool FillPlayerHeaders(bool shouldSave)
        {
            if (IsAllPlayersIdsSelected() && !IsPlayerIdRepeated())
            {
                if (shouldSave)
                {
                    _db.UpdateTablePlayersPositions(_table);
                }
                _form.SetEastPlayerHeader(_tablePlayers.Find(x => x.PlayerId == _table.PlayerEastId).PlayerName);
                _form.SetSouthPlayerHeader(_tablePlayers.Find(x => x.PlayerId == _table.PlayerSouthId).PlayerName);
                _form.SetWestPlayerHeader(_tablePlayers.Find(x => x.PlayerId == _table.PlayerWestId).PlayerName);
                _form.SetNorthPlayerHeader(_tablePlayers.Find(x => x.PlayerId == _table.PlayerNorthId).PlayerName);
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
                comboPlayers.Add(new ComboItem(string.Format("{0} - {1}", player.PlayerId, player.PlayerName), player.PlayerId.ToString()));
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

        private bool IsValidPlayerId(string stringValue)
        {
            int intValue;
            bool result = int.TryParse(stringValue, out intValue);
            return result && intValue > 0 && IsACurrentTablePlayerId(intValue);
        }

        private bool IsACurrentTablePlayerId(int playerId)
        {
            return _tablePlayers.Find(x => x.PlayerId == playerId) != null;
        }

        private bool IsValidScore(string stringValue)
        {
            int intValue;
            bool result = int.TryParse(stringValue, out intValue);
            return result && intValue >= 0;
        }

        private void CalculateAndFillAllScoresIfHandIsComplete()
        {
            foreach (DBHand hand in _hands)
            {
                //if(hand.HandScore.Equals(string.Empty))
                //    return;
                //else if (int.Parse(hand.HandScore) == 0)
                //{//WASHOUT
                //    _form.FillPlayersHandScores(hand.HandId, 0, 0, 0, 0);
                //}
                //else
                //{
                //    if(hand.PlayerLooserId > 0)
                //    {//RON
                //        _form.FillPlayersHandScores(hand.Id, 0, 0, 0, 0);
                //    }
                //    else
                //    {//TSUMO
                //        _form.FillPlayersHandScores(hand.Id, 0, 0, 0, 0);
                //    }
                //}
                CalculateTotalScores();
            }
            
        }

        private void CalculateTotalScores()
        {
            
        }

        #endregion
    }
}
