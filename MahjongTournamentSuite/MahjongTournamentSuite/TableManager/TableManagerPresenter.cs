using MahjongTournamentSuite.Data;
using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System;
using static MahjongTournamentSuite.Model.DBHand;
using static MahjongTournamentSuite.TableManager.PlayerTablePoints;

namespace MahjongTournamentSuite.TableManager
{
    class TableManagerPresenter : ITableManagerPresenter
    {
        #region Constants

        private readonly int MCR_MIN_POINTS = 8;
        private const int NUM_LOOSER_PLAYERS = 3;

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
                CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
            }
        }

        public void NameEastPlayerChanged(string selectedPlayerId)
        {
            _table.PlayerEastId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public void NameSouthPlayerChanged(string selectedPlayerId)
        {
            _table.PlayerSouthId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public void NameWestPlayerChanged(string selectedPlayerId)
        {
            _table.PlayerWestId = selectedPlayerId;
            ShowDGVIfAllPlayersPositionsSelected();
        }

        public void NameNorthPlayerChanged(string selectedPlayerId)
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
                ValidateAndSaveAndFillTotalsScoresAndPoints();
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
                ValidateAndSaveAndFillTotalsScoresAndPoints();
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
                ValidateAndSaveAndFillTotalsScoresAndPoints();
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
                ValidateAndSaveAndFillTotalsScoresAndPoints();
                return sValidValue;
            }
            _form.PlayKoSound();
            return _table.PlayerNorthTotalScore;
        }

        public string PlayerWinnerIdChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePlayerId(hand, newValue);
            _db.UpdateHandWinnerId(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
            return returnValue;
        }

        public string PlayerLooserIdChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePlayerId(hand, newValue);
            _db.UpdateHandLooserId(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
            return returnValue;
        }

        public string HandScoreChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidateHandScore(hand, newValue);
            _db.UpdateHandScore(_hands.Find(x => x.HandId == handId), returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
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
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
            return returnValue;
        }

        public string PlayerSouthPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerSouthPenalty, newValue);
            _db.UpdateHandPlayerSouthPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
            return returnValue;
        }

        public string PlayerWestPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerWestPenalty, newValue);
            _db.UpdateHandPlayerWestPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
            return returnValue;
        }

        public string PlayerNorthPenalytChanged(int handId, string newValue)
        {
            DBHand hand = _hands.Find(x => x.HandId == handId);
            string returnValue = ValidatePenalty(hand.PlayerNorthPenalty, newValue);
            _db.UpdateHandPlayerNorthPenalty(hand, returnValue);
            CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
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
                CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints();
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
                DBPlayer playerEast = _tablePlayers.Find(x => x.PlayerId == int.Parse(_table.PlayerEastId));
                DBPlayer playerSouth = _tablePlayers.Find(x => x.PlayerId == int.Parse(_table.PlayerSouthId));
                DBPlayer playerWest = _tablePlayers.Find(x => x.PlayerId == int.Parse(_table.PlayerWestId));
                DBPlayer playerNorth = _tablePlayers.Find(x => x.PlayerId == int.Parse(_table.PlayerNorthId));
                _form.SetEastPlayerHeader(string.Format("{0} ({1})", playerEast.PlayerName, playerEast.PlayerId));
                _form.SetSouthPlayerHeader(string.Format("{0} ({1})", playerSouth.PlayerName, playerSouth.PlayerId));
                _form.SetWestPlayerHeader(string.Format("{0} ({1})", playerWest.PlayerName, playerWest.PlayerId));
                _form.SetNorthPlayerHeader(string.Format("{0} ({1})", playerNorth.PlayerName, playerNorth.PlayerId));
                FillAllPlayersTotalScores();
                FillAllPlayersPoints();
                return true;
            }
            return false;
        }

        private bool IsAllPlayersIdsSelected()
        {
            return !_table.PlayerEastId.Equals(string.Empty) && int.Parse(_table.PlayerEastId) > 0 &&
                !_table.PlayerSouthId.Equals(string.Empty) && int.Parse(_table.PlayerSouthId) > 0 &&
                !_table.PlayerWestId.Equals(string.Empty) && int.Parse(_table.PlayerWestId) > 0 &&
                !_table.PlayerNorthId.Equals(string.Empty) && int.Parse(_table.PlayerNorthId) > 0;
        }

        private bool IsPlayerIdRepeated()
        {
            return _table.PlayerEastId.Equals(_table.PlayerSouthId) ||
                _table.PlayerEastId.Equals(_table.PlayerWestId) ||
                _table.PlayerEastId.Equals(_table.PlayerNorthId) ||
                _table.PlayerSouthId.Equals(_table.PlayerWestId) ||
                _table.PlayerSouthId.Equals(_table.PlayerNorthId) ||
                _table.PlayerWestId.Equals(_table.PlayerNorthId);
        }

        private bool IsACurrentTablePlayerId(int playerId)
        {
            return _tablePlayers.Find(x => x.PlayerId == playerId) != null;
        }

        private int GetPlayerEastIndex()
        {
            if (int.Parse(_table.PlayerEastId) == _table.Player1Id)
                return 1;
            else if (int.Parse(_table.PlayerEastId) == _table.Player2Id)
                return 2;
            else if (int.Parse(_table.PlayerEastId) == _table.Player3Id)
                return NUM_LOOSER_PLAYERS;
            else
                return 4;
        }

        private int GetPlayerSouthIndex()
        {
            if (int.Parse(_table.PlayerSouthId) == _table.Player1Id)
                return 1;
            else if (int.Parse(_table.PlayerSouthId) == _table.Player2Id)
                return 2;
            else if (int.Parse(_table.PlayerSouthId) == _table.Player3Id)
                return NUM_LOOSER_PLAYERS;
            else
                return 4;
        }

        private int GetPlayerWestIndex()
        {
            if (int.Parse(_table.PlayerWestId) == _table.Player1Id)
                return 1;
            else if (int.Parse(_table.PlayerWestId) == _table.Player2Id)
                return 2;
            else if (int.Parse(_table.PlayerWestId) == _table.Player3Id)
                return NUM_LOOSER_PLAYERS;
            else
                return 4;
        }

        private int GetPlayerNorthIndex()
        {
            if (int.Parse(_table.PlayerNorthId) == _table.Player1Id)
                return 1;
            else if (int.Parse(_table.PlayerNorthId) == _table.Player2Id)
                return 2;
            else if (int.Parse(_table.PlayerNorthId) == _table.Player3Id)
                return NUM_LOOSER_PLAYERS;
            else
                return 4;
        }

        private string ValidatePlayerId(DBHand hand, string newValue)
        {
            if (newValue == null || newValue.Equals(string.Empty))
                return string.Empty;
            int validValue;
            if (int.TryParse(newValue, out validValue))
            {
                if (validValue == 0)
                    return string.Empty;
                else if (validValue > 0 && IsACurrentTablePlayerId(validValue) &&
                !hand.PlayerLooserId.Equals(validValue.ToString()))
                    return validValue.ToString();
            }
            _form.PlayKoSound();
            return hand.PlayerWinnerId;
        }

        private string ValidateHandScore(DBHand hand, string newValue)
        {
            if (newValue == null || newValue.Equals(string.Empty))
                return string.Empty;
            int validValue;
            if (int.TryParse(newValue, out validValue) &&
                (validValue == 0 || validValue >= MCR_MIN_POINTS))
                return validValue.ToString();
            _form.PlayKoSound();
            return hand.HandScore;
        }

        private string ValidatePenalty(string previousValue, string newValue)
        {
            if (newValue == null || newValue.Equals(string.Empty))
                return string.Empty;
            int validValue;
            if (int.TryParse(newValue, out validValue))
                return validValue.ToString();
            else if (validValue == 0)
                return string.Empty;
            _form.PlayKoSound();
            return previousValue;
        }

        private bool IsAllTotalScoresFilled()
        {
            return !_table.PlayerEastTotalScore.Equals(string.Empty) &&
                !_table.PlayerSouthTotalScore.Equals(string.Empty) &&
                !_table.PlayerWestTotalScore.Equals(string.Empty) &&
                !_table.PlayerNorthTotalScore.Equals(string.Empty);
        }
        
        private void CalculateAndFillAllHandsScoresAndPlayersTotalsAndPoints()
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
                                CalculateWashout(dgvHand);
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
                CalculateAndSaveAndFillAllPlayersTotalScoresAndPoints();
            } 
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

        private void CalculateWashout(DGVHand dgvHand)
        {
            dgvHand.PlayerEastScore = "0";
            dgvHand.PlayerSouthScore = "0";
            dgvHand.PlayerWestScore = "0";
            dgvHand.PlayerNorthScore = "0";
        }

        private void CalculateTsumo(DGVHand dgvHand)
        {
            string winnerPoints = ((int.Parse(dgvHand.HandScore) + MCR_MIN_POINTS) * NUM_LOOSER_PLAYERS).ToString();
            string looserPoints = (-(int.Parse(dgvHand.HandScore) + MCR_MIN_POINTS)).ToString();
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
        }

        private void CalculateRon(DGVHand dgvHand)
        {
            string winnerPoints = (int.Parse(dgvHand.HandScore) + (8 * NUM_LOOSER_PLAYERS)).ToString();
            string looserPoints = (-(int.Parse(dgvHand.HandScore) + 8)).ToString();
            string restPlayersPoints = "-8";
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
        }

        private void CalculatePenalties(DGVHand dgvHand)
        {
            if (!dgvHand.PlayerEastPenalty.Equals(string.Empty))
                    dgvHand.PlayerEastScore = (int.Parse(dgvHand.PlayerEastScore) + int.Parse(dgvHand.PlayerEastPenalty)).ToString();
            if (!dgvHand.PlayerSouthPenalty.Equals(string.Empty))
                    dgvHand.PlayerSouthScore = (int.Parse(dgvHand.PlayerSouthScore) + int.Parse(dgvHand.PlayerSouthPenalty)).ToString();
            if (!dgvHand.PlayerWestPenalty.Equals(string.Empty))
                    dgvHand.PlayerWestScore = (int.Parse(dgvHand.PlayerWestScore) + int.Parse(dgvHand.PlayerWestPenalty)).ToString();
            if (!dgvHand.PlayerNorthPenalty.Equals(string.Empty))
                    dgvHand.PlayerNorthScore = (int.Parse(dgvHand.PlayerNorthScore) + int.Parse(dgvHand.PlayerNorthPenalty)).ToString();
        }
        
        private void CalculateAndSaveAndFillAllPlayersTotalScoresAndPoints()
        {
            int eastTotalScore = 0, southTotalScore = 0, westTotalScore = 0, northTotalScore = 0;
            foreach(DGVHand dgvHand in _dgvHands)
            {
                if(!dgvHand.PlayerEastScore.Equals(string.Empty))
                    eastTotalScore += int.Parse(dgvHand.PlayerEastScore);
                if (!dgvHand.PlayerEastPenalty.Equals(string.Empty))
                    eastTotalScore += int.Parse(dgvHand.PlayerEastPenalty);
                if (!dgvHand.PlayerSouthScore.Equals(string.Empty))
                    southTotalScore += int.Parse(dgvHand.PlayerSouthScore);
                if (!dgvHand.PlayerSouthPenalty.Equals(string.Empty))
                    southTotalScore += int.Parse(dgvHand.PlayerSouthPenalty);
                if (!dgvHand.PlayerWestScore.Equals(string.Empty))
                    westTotalScore += int.Parse(dgvHand.PlayerWestScore);
                if (!dgvHand.PlayerWestPenalty.Equals(string.Empty))
                    westTotalScore += int.Parse(dgvHand.PlayerWestPenalty);
                if (!dgvHand.PlayerNorthScore.Equals(string.Empty))
                    northTotalScore += int.Parse(dgvHand.PlayerNorthScore);
                if (!dgvHand.PlayerNorthPenalty.Equals(string.Empty))
                    northTotalScore += int.Parse(dgvHand.PlayerNorthPenalty);
            }
            _table.PlayerEastTotalScore = eastTotalScore.ToString();
            _table.PlayerSouthTotalScore = southTotalScore.ToString();
            _table.PlayerWestTotalScore = westTotalScore.ToString();
            _table.PlayerNorthTotalScore = northTotalScore.ToString();
            ValidateAndSaveAndFillTotalsScoresAndPoints();
            FillAllPlayersTotalScores();
        }

        private bool ValidateAndSaveAndFillTotalsScoresAndPoints()
        {
            if (!IsAllTotalScoresFilled())
                return false;
            _db.UpdateTableAllPlayersTotalScores(_table);
            FillAllPlayersTotalScores();
            CalculateAndSaveAndFillAllPlayersPoints();
            return true;
        }

        private void FillAllPlayersTotalScores()
        {
            _form.FillAllTotalScoreTextBoxes(
                _table.PlayerEastTotalScore, _table.PlayerSouthTotalScore,
                _table.PlayerWestTotalScore, _table.PlayerNorthTotalScore);
        }

        private void CalculateAndSaveAndFillAllPlayersPoints()
        {
            List<PlayerTablePoints> ptp = new List<PlayerTablePoints>(4);
            ptp.Add(new PlayerTablePoints(Seats.EAST, int.Parse(_table.PlayerEastTotalScore)));
            ptp.Add(new PlayerTablePoints(Seats.SOUTH, int.Parse(_table.PlayerSouthTotalScore)));
            ptp.Add(new PlayerTablePoints(Seats.WEST, int.Parse(_table.PlayerWestTotalScore)));
            ptp.Add(new PlayerTablePoints(Seats.NORTH, int.Parse(_table.PlayerNorthTotalScore)));
            ptp.Sort((x, y) => -1 * x.Score.CompareTo(y.Score));

            if(ptp[0].Score == ptp[1].Score && ptp[1].Score == ptp[2].Score && ptp[2].Score == ptp[3].Score)
            {
                ptp[0].Points = "1.75";
                ptp[1].Points = "1.75";
                ptp[2].Points = "1.75";
                ptp[3].Points = "1.75";
            }
            else if(ptp[0].Score == ptp[1].Score && ptp[1].Score == ptp[2].Score)
            {
                ptp[0].Points = "2.33";
                ptp[1].Points = "2.33";
                ptp[2].Points = "2.33";
                ptp[3].Points = "0";
            }
            else if (ptp[1].Score == ptp[2].Score && ptp[2].Score == ptp[3].Score)
            {
                ptp[0].Points = "4";
                ptp[1].Points = "1";
                ptp[2].Points = "1";
                ptp[3].Points = "1";
            }
            else if (ptp[0].Score == ptp[1].Score)
                {
                    ptp[0].Points = "3";
                    ptp[1].Points = "3";
                    ptp[2].Points = "1";
                    ptp[3].Points = "0";
                    if (ptp[2].Score == ptp[3].Score)
                    {
                        ptp[0].Points = "3";
                        ptp[1].Points = "3";
                        ptp[2].Points = "0.5";
                        ptp[3].Points = "0.5";
                    }
            }
            else if(ptp[1].Score == ptp[2].Score)
            {
                ptp[0].Points = "4";
                ptp[1].Points = "1.5";
                ptp[2].Points = "1.5";
                ptp[3].Points = "0";
            }
            else if(ptp[2].Score == ptp[3].Score)
            {
                ptp[0].Points = "4";
                ptp[1].Points = "2";
                ptp[2].Points = "0.5";
                ptp[3].Points = "0.5";
            }
            else
            {
                ptp[0].Points = "4";
                ptp[1].Points = "2";
                ptp[2].Points = "1";
                ptp[3].Points = "0";
            }

            _table.PlayerEastPoints = ptp.Find(x => x.Seat == Seats.EAST).Points;
            _table.PlayerSouthPoints = ptp.Find(x => x.Seat == Seats.SOUTH).Points;
            _table.PlayerWestPoints = ptp.Find(x => x.Seat == Seats.WEST).Points;
            _table.PlayerNorthPoints = ptp.Find(x => x.Seat == Seats.NORTH).Points;

            _db.UpdateTableAllPlayersPoints(_table);
        }

        private void FillAllPlayersPoints()
        {
            _form.FillAllTotalPointsTextBoxes(
                _table.PlayerEastPoints,
                _table.PlayerSouthPoints,
                _table.PlayerWestPoints,
                _table.PlayerNorthPoints);
        }

        #endregion
    }
}
