using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;
using static MahjongTournamentSuite.Model.DBHand;

namespace MahjongTournamentSuite.TableManager
{
    class TableManagerPresenter : ITableManagerPresenter
    {
        #region Constants

        private readonly int MCR_MIN_POINTS = 8;

        #endregion
        
        #region Fields

        private ITableManagerForm _form;
        private IDBManager _db;
        private DBTable _table;
        private List<DBPlayer> _tablePlayers;
        private List<DBHand> _hands;
        private List<DGVHand> _dgvHands;

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
            FillDataGridHandsWithoutScores();
            if(FillPlayerHeaders(false))
            {
                _form.ShowPanelTotals();
                _form.ShowDataGridHands();
                CalculateAndFillAllHandsScoresAndPlayersTotals();
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

        public string TotalScoreEastPlayerChanged(string newScore)
        {
            int validValue;
            if (int.TryParse(newScore, out validValue))
            {
                string sValidValue = validValue.ToString();
                _table.PlayerEastTotalScore = sValidValue;
                ValidateAndSaveTotalsScores();
                return validValue.ToString();
            }
            _form.PlayKoSound();
            return _table.PlayerEastTotalScore;
        }

        public string TotalScoreSouthPlayerChanged(string newScore)
        {
            int validValue;
            if (int.TryParse(newScore, out validValue))
            {
                string sValidValue = validValue.ToString();
                _table.PlayerSouthTotalScore = sValidValue;
                ValidateAndSaveTotalsScores();
                return validValue.ToString();
            }
            _form.PlayKoSound();
            return _table.PlayerSouthTotalScore;
        }

        public string TotalScoreWestPlayerChanged(string newScore)
        {
            int validValue;
            if (int.TryParse(newScore, out validValue))
            {
                string sValidValue = validValue.ToString();
                _table.PlayerWestTotalScore = sValidValue;
                ValidateAndSaveTotalsScores();
                return validValue.ToString();
            }
            _form.PlayKoSound();
            return _table.PlayerWestTotalScore;
        }

        public string TotalScoreNorthPlayerChanged(string newScore)
        {
            int validValue;
            if (int.TryParse(newScore, out validValue))
            {
                string sValidValue = validValue.ToString();
                _table.PlayerNorthTotalScore = sValidValue;
                ValidateAndSaveTotalsScores();
                return sValidValue;
            }
            _form.PlayKoSound();
            return _table.PlayerNorthTotalScore;
        }

        public string PlayerWinnerIdChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = null;
            if (newValue == null || newValue.Equals(string.Empty))
                returnValue = string.Empty;
            else
            {
                int validValue;
                if (int.TryParse(newValue, out validValue) && validValue > 0 
                    && IsACurrentTablePlayerId(validValue) && 
                    !hand.PlayerLooserId.Equals(validValue.ToString()))
                    returnValue = validValue.ToString();
                else
                {
                    _form.PlayKoSound();
                    return hand.PlayerWinnerId;
                }
            }
            _db.UpdateHandWinnerId(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        public string PlayerLooserIdChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = null;
            if (newValue == null || newValue.Equals(string.Empty))
                returnValue = string.Empty;
            else
            {
                int validValue;
                if (int.TryParse(newValue, out validValue) && validValue > 0 
                    && IsACurrentTablePlayerId(validValue) 
                    && !hand.PlayerWinnerId.Equals(validValue.ToString()))
                    returnValue = validValue.ToString();
                else
                {
                    _form.PlayKoSound();
                    return hand.PlayerLooserId;
                }
            }
            _db.UpdateHandLooserId(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        public string HandScoreChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = null;
            if (newValue == null || newValue.Equals(string.Empty))
                returnValue = string.Empty;
            else
            {
                int validValue;
                if (int.TryParse(newValue, out validValue) && validValue >= MCR_MIN_POINTS)
                    returnValue = validValue.ToString();
                else
                {
                    _form.PlayKoSound();
                    return hand.HandScore;
                }
            }
            _db.UpdateHandScore(_hands.Find(x => x.HandId == handId), returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        public bool IsChickenHandChanged(int handId)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            bool returnValue;
            if (hand.IsChickenHand)
                returnValue = false;
            else
            {
                if (!hand.HandScore.Equals(string.Empty) &&
                    !hand.PlayerWinnerId.Equals(string.Empty) &&
                    !hand.PlayerLooserId.Equals(string.Empty))
                    returnValue = true;
                else
                {
                    _form.PlayKoSound();
                    _form.ShowMessageChickenHandNeedWinnerLooserAndScore();
                    returnValue = false;
                }
            }
            _db.UpdateHandIsChickenHand(_hands.Find(x => x.HandId == handId), returnValue);
            return returnValue;
        }

        public string PlayerEastPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerEastPenalty, newValue);
            _db.UpdateHandPlayerEastPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        public string PlayerSouthPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerSouthPenalty, newValue);
            _db.UpdateHandPlayerSouthPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        public string PlayerWestPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerWestPenalty, newValue);
            _db.UpdateHandPlayerWestPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        public string PlayerNorthPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerNorthPenalty, newValue);
            _db.UpdateHandPlayerNorthPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotals();
            return returnValue;
        }

        #endregion

        #region Private

        private void FillDataGridHandsWithoutScores()
        {
            _dgvHands = new List<DGVHand>();
            foreach(DBHand hand in _hands)
                _dgvHands.Add(new DGVHand(hand));
            _form.FillDGV(_dgvHands);
        }

        private void ShowDGVIfAllPlayersPositionsSelected()
        {
            if (FillPlayerHeaders(true))
            {
                _form.ShowPanelTotals();
                _form.ShowDataGridHands();
                CalculateAndFillAllHandsScoresAndPlayersTotals();
            }
            else
            {
                _form.HidePanelTotals();
                _form.HideDataGridHands();
            }
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
                _form.SelectPlayersInCombos(
                    GetPlayerEastIndex(), GetPlayerSouthIndex(), 
                    GetPlayerWestIndex(), GetPlayerNorthIndex());
            }
        }

        private bool FillPlayerHeaders(bool shouldSave)
        {
            if (IsAllPlayersIdsSelected() && !IsPlayerIdRepeated())
            {
                if (shouldSave)
                {
                    _db.UpdateTableAllPlayersPositions(_table);
                }
                DBPlayer playerEast = _tablePlayers.Find(x => x.PlayerId == _table.PlayerEastId);
                DBPlayer playerSouth = _tablePlayers.Find(x => x.PlayerId == _table.PlayerSouthId);
                DBPlayer playerWest = _tablePlayers.Find(x => x.PlayerId == _table.PlayerWestId);
                DBPlayer playerNorth = _tablePlayers.Find(x => x.PlayerId == _table.PlayerNorthId);
                _form.SetEastPlayerHeader(string.Format("{0} ({1})", playerEast.PlayerName, playerEast.PlayerId));
                _form.SetSouthPlayerHeader(string.Format("{0} ({1})", playerSouth.PlayerName, playerSouth.PlayerId));
                _form.SetWestPlayerHeader(string.Format("{0} ({1})", playerWest.PlayerName, playerWest.PlayerId));
                _form.SetNorthPlayerHeader(string.Format("{0} ({1})", playerNorth.PlayerName, playerNorth.PlayerId));
                FillAllPlayersTotalScores();
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

        private bool IsACurrentTablePlayerId(int playerId)
        {
            return _tablePlayers.Find(x => x.PlayerId == playerId) != null;
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

        private string ValidatePenalty(string previousValue, string newValue)
        {
            if (newValue == null || newValue.Equals(string.Empty))
                return string.Empty;
            else
            {
                int validValue;
                if (int.TryParse(newValue, out validValue) && validValue > 0)
                    return validValue.ToString();
                else if (validValue == 0)
                    return string.Empty;
                else
                {
                    _form.PlayKoSound();
                    return previousValue;
                }
            }
        }

        private bool ValidateAndSaveTotalsScores()
        {
            if (!IsAllTotalScoresFilled())
                return false;
            if (!IsTotalScoresSumEqualsZero())
            {
                _form.ShowErrorTotalScores();
                _form.CleanTotalPoints();
                return false;
            }
            _db.UpdateTableAllPlayersTotalScores(_table);
            _form.HideErrorTotalScores();
            FillAllPlayersTotalScores();
            CalculateAndFillAllPlayersTotalPoints();
            return true;
        }

        private bool IsAllTotalScoresFilled()
        {
            return !_table.PlayerEastTotalScore.Equals(string.Empty) &&
                !_table.PlayerSouthTotalScore.Equals(string.Empty) &&
                !_table.PlayerWestTotalScore.Equals(string.Empty) &&
                !_table.PlayerNorthTotalScore.Equals(string.Empty);
        }

        private bool IsTotalScoresSumEqualsZero()
        {
            return (int.Parse(_table.PlayerEastTotalScore)
                + int.Parse(_table.PlayerSouthTotalScore)
                + int.Parse(_table.PlayerWestTotalScore)
                + int.Parse(_table.PlayerNorthTotalScore)) == 0;
        }

        private void CalculateAndFillAllHandsScoresAndPlayersTotals()
        {
            if (!IsFilledAnyHand())
            {
                _form.EnableTotalScoresTextBoxes();
                FillAllPlayersTotalScores();
                return;
            }
            _form.DisableTotalScoresTextBoxes();

            if (_dgvHands != null)
            {
                bool finishCalculation = false;
                foreach (DBHand hand in _dgvHands)
                {
                    DGVHand dgvHand = _dgvHands.Find(x => x.HandId == hand.HandId);
                    dgvHand = new DGVHand(hand);
                    if (finishCalculation)
                        _form.FillHandPlayersScoresCells(dgvHand);
                    else
                    {
                        switch (hand.GetHandType())
                        {
                            case HandType.WASHOUT:
                                CalculatePenalties(dgvHand);
                                _form.FillHandPlayersScoresCells(dgvHand);
                                break;
                            case HandType.TSUMO:
                                CalculateTsumo(dgvHand);
                                CalculatePenalties(dgvHand);
                                _form.FillHandPlayersScoresCells(dgvHand);
                                break;
                            case HandType.RON:
                                CalculateRon(dgvHand);
                                CalculatePenalties(dgvHand);
                                _form.FillHandPlayersScoresCells(dgvHand);
                                break;
                            case HandType.NONE:
                            default:
                                finishCalculation = true;
                                _form.FillHandPlayersScoresCells(new DGVHand(hand));
                                break;
                        }
                    }
                }
                CalculateAndSaveAndFillAllPlayersTotalScores();
                CalculateAndFillAllPlayersTotalPoints();
            } 
        }

        private DGVHand CalculateTsumo(DGVHand dgvHand)
        {
            string winnerPoints = ((int.Parse(dgvHand.HandScore) + 8) * 3).ToString();
            string looserPoints = (int.Parse(dgvHand.HandScore) + 8).ToString();
            if (dgvHand.PlayerWinnerId.Equals(_table.PlayerEastId))
            {
                dgvHand.PlayerEastScore = winnerPoints;
                dgvHand.PlayerSouthScore = looserPoints;
                dgvHand.PlayerWestScore = looserPoints;
                dgvHand.PlayerNorthScore = looserPoints;
            }
            else if (dgvHand.PlayerWinnerId.Equals(_table.PlayerSouthId))
            {
                dgvHand.PlayerEastScore = looserPoints;
                dgvHand.PlayerSouthScore = winnerPoints;
                dgvHand.PlayerWestScore = looserPoints;
                dgvHand.PlayerNorthScore = looserPoints;
            }
            else if (dgvHand.PlayerWinnerId.Equals(_table.PlayerWestId))
            {
                dgvHand.PlayerEastScore = looserPoints;
                dgvHand.PlayerSouthScore = looserPoints;
                dgvHand.PlayerWestScore = winnerPoints;
                dgvHand.PlayerNorthScore = looserPoints;
            }
            else
            {
                dgvHand.PlayerEastScore = looserPoints;
                dgvHand.PlayerSouthScore = looserPoints;
                dgvHand.PlayerWestScore = looserPoints;
                dgvHand.PlayerNorthScore = winnerPoints;
            }
            return dgvHand;
        }

        private DGVHand CalculateRon(DGVHand dgvHand)
        {
            string winnerPoints = (int.Parse(dgvHand.HandScore) + (8 * 3)).ToString();
            string looserPoints = (int.Parse(dgvHand.HandScore) + 8).ToString();
            string restPlayersPoints = "8";
            //EAST
            if (_table.PlayerEastId.Equals(dgvHand.PlayerWinnerId))
                dgvHand.PlayerEastScore = winnerPoints;
            else if(_table.PlayerEastId.Equals(dgvHand.PlayerLooserId))
                dgvHand.PlayerEastScore = looserPoints;
            else
                dgvHand.PlayerEastScore = restPlayersPoints;
            //SOUTH
            if (_table.PlayerSouthId.Equals(dgvHand.PlayerWinnerId))
                dgvHand.PlayerSouthScore = winnerPoints;
            else if (_table.PlayerSouthId.Equals(dgvHand.PlayerLooserId))
                dgvHand.PlayerSouthScore = looserPoints;
            else
                dgvHand.PlayerSouthScore = restPlayersPoints;
            //WEST
            if (_table.PlayerWestId.Equals(dgvHand.PlayerWinnerId))
                dgvHand.PlayerWestScore = winnerPoints;
            else if (_table.PlayerWestId.Equals(dgvHand.PlayerLooserId))
                dgvHand.PlayerWestScore = looserPoints;
            else
                dgvHand.PlayerWestScore = restPlayersPoints;
            //NORTH
            if (_table.PlayerNorthId.Equals(dgvHand.PlayerWinnerId))
                dgvHand.PlayerNorthScore = winnerPoints;
            else if (_table.PlayerNorthId.Equals(dgvHand.PlayerLooserId))
                dgvHand.PlayerNorthScore = looserPoints;
            else
                dgvHand.PlayerNorthScore = restPlayersPoints;

            return dgvHand;
        }

        private DGVHand CalculatePenalties(DGVHand dgvHand)
        {
            if (!dgvHand.PlayerEastPenalty.Equals(string.Empty))
            {
                dgvHand.PlayerEastScore = (int.Parse(dgvHand.PlayerEastScore) - int.Parse(dgvHand.PlayerEastPenalty)).ToString();
                dgvHand.PlayerSouthScore = (int.Parse(dgvHand.PlayerSouthScore) + (int.Parse(dgvHand.PlayerEastPenalty) / 3)).ToString();
                dgvHand.PlayerWestScore = (int.Parse(dgvHand.PlayerWestScore) + (int.Parse(dgvHand.PlayerEastPenalty) / 3)).ToString();
                dgvHand.PlayerNorthScore = (int.Parse(dgvHand.PlayerNorthScore) + (int.Parse(dgvHand.PlayerEastPenalty) / 3)).ToString();
            }
            if (!dgvHand.PlayerSouthPenalty.Equals(string.Empty))
            {
                dgvHand.PlayerEastScore = (int.Parse(dgvHand.PlayerEastScore) + (int.Parse(dgvHand.PlayerSouthPenalty) / 3)).ToString();
                dgvHand.PlayerSouthScore = (int.Parse(dgvHand.PlayerSouthScore) - int.Parse(dgvHand.PlayerSouthPenalty)).ToString();
                dgvHand.PlayerWestScore = (int.Parse(dgvHand.PlayerWestScore) + (int.Parse(dgvHand.PlayerSouthPenalty) / 3)).ToString();
                dgvHand.PlayerNorthScore = (int.Parse(dgvHand.PlayerNorthScore) + (int.Parse(dgvHand.PlayerSouthPenalty) / 3)).ToString();
            }
            if (!dgvHand.PlayerEastPenalty.Equals(string.Empty))
            {
                dgvHand.PlayerEastScore = (int.Parse(dgvHand.PlayerEastScore) + (int.Parse(dgvHand.PlayerWestPenalty) / 3)).ToString();
                dgvHand.PlayerSouthScore = (int.Parse(dgvHand.PlayerSouthScore) + (int.Parse(dgvHand.PlayerWestPenalty) / 3)).ToString();
                dgvHand.PlayerWestScore = (int.Parse(dgvHand.PlayerWestScore) - int.Parse(dgvHand.PlayerWestPenalty)).ToString();
                dgvHand.PlayerNorthScore = (int.Parse(dgvHand.PlayerNorthScore) + (int.Parse(dgvHand.PlayerWestPenalty) / 3)).ToString();
            }
            if (!dgvHand.PlayerEastPenalty.Equals(string.Empty))
            {
                dgvHand.PlayerEastScore = (int.Parse(dgvHand.PlayerEastScore) + (int.Parse(dgvHand.PlayerNorthPenalty) / 3)).ToString();
                dgvHand.PlayerSouthScore = (int.Parse(dgvHand.PlayerSouthScore) + (int.Parse(dgvHand.PlayerNorthPenalty) / 3)).ToString();
                dgvHand.PlayerWestScore = (int.Parse(dgvHand.PlayerWestScore) + (int.Parse(dgvHand.PlayerNorthPenalty) / 3)).ToString();
                dgvHand.PlayerNorthScore = (int.Parse(dgvHand.PlayerNorthScore) - int.Parse(dgvHand.PlayerNorthPenalty)).ToString();
            }
            return dgvHand;
        }

        private bool IsFilledAnyHand()
        {
            foreach(DBHand hand in _hands)
            {
                if (!hand.HandScore.Equals(string.Empty) &&
                    (!hand.PlayerWinnerId.Equals(string.Empty) ||
                    hand.PlayerLooserId.Equals(string.Empty)))
                    return true;
            }
            return false;
        }

        private void CalculateAndSaveAndFillAllPlayersTotalScores()
        {
            //Calcular scores totales aqui!
            ValidateAndSaveTotalsScores();
            FillAllPlayersTotalScores();
        }

        private void FillAllPlayersTotalScores()
        {
            _form.FillAllTotalScoreTextBoxes(
                _table.PlayerEastTotalScore, _table.PlayerSouthTotalScore,
                _table.PlayerWestTotalScore, _table.PlayerNorthTotalScore);
        }

        private void CalculateAndFillAllPlayersTotalPoints()
        {
            //_form.FillAllTotalPointsTextBoxes("0", "0", "0", "0");
        }

        #endregion
    }
}
