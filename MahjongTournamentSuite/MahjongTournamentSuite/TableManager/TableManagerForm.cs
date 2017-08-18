using MahjongTournamentSuite.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.TableManager
{
    partial class TableManagerForm : Form, ITableManagerForm
    {
        #region Constants
        
        private const string COLUMN_TOURNAMENT_ID = "TournamentId";
        private const string COLUMN_ROUND_ID = "RoundId";
        private const string COLUMN_TABLE_ID = "TableId";
        private const string COLUMN_ID = "Id";
        private const string COLUMN_PLAYER_WINNER_ID = "PlayerWinnerId";
        private const string COLUMN_PLAYER_LOOSER_ID = "PlayerLooserId";
        private const string COLUMN_SCORE = "Score";
        private const string COLUMN_IS_CHICKEN_HAND = "IsChickenHand";
        private const string COLUMN_PLAYER_EAST_SCORE = "PlayerEastScore";
        private const string COLUMN_PLAYER_SOUTH_SCORE = "PlayerSouthScore";
        private const string COLUMN_PLAYER_WEST_SCORE = "PlayerWestScore";
        private const string COLUMN_PLAYER_NORTH_SCORE = "PlayerNorthScore";
        private const int COLUMN_ID_INDEX = 7;
        private const int COLUMN_PLAYER_WINNER_ID_INDEX = 8;
        private const int COLUMN_PLAYER_LOOSER_ID_INDEX = 9;
        private const int COLUMN_POINTS_INDEX = 10;
        private const int COLUMN_IS_CHICKEN_HAND_INDEX = 11;

        #endregion

        #region Fields

        private ITableManagerPresenter _presenter;
        private int _tournamentId, _roundId, _tableId;

        #endregion

        #region Constructor

        public TableManagerForm(int tournamentId, int roundId, int tableId)
        {
            InitializeComponent();
            _tournamentId = tournamentId;
            _roundId = roundId;
            _tableId = tableId;
            _presenter = Injector.provideTableManagerPresenter(this);
            _presenter.LoadForm(_tournamentId, _roundId, _tableId);
        }

        #endregion

        #region Lifecycle

        #endregion

        #region Events

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
        }

        #region Combos
        private void comboEastPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameEastPlayerChanged(int.Parse(((ComboItem)comboEastPlayer.SelectedItem).Value));
        }

        private void comboSouthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameSouthPlayerChanged(int.Parse(((ComboItem)comboSouthPlayer.SelectedItem).Value));
        }

        private void comboWestPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameWestPlayerChanged(int.Parse(((ComboItem)comboWestPlayer.SelectedItem).Value));
        }

        private void comboNorthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameNorthPlayerChanged(int.Parse(((ComboItem)comboNorthPlayer.SelectedItem).Value));
        }
        #endregion

        #region Cells
        private void dataGridHands_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex != COLUMN_IS_CHICKEN_HAND_INDEX)
                dataGridView.BeginEdit(true);
        }

        private void dataGridHands_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == COLUMN_IS_CHICKEN_HAND_INDEX)
            {
                DataGridViewCheckBoxCell checkCell = dataGridView.CurrentCell as DataGridViewCheckBoxCell;
                checkCell.Value = checkCell.Value == null || !((bool)checkCell.Value);
                dataGridView.RefreshEdit();
                dataGridView.NotifyCurrentCellDirty(true);
                _presenter.IsChickenHandChanged(GetSelectedHandId(e.RowIndex), (bool)checkCell.Value);
            }
        }

        private void dataGridHands_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridHands_CellClick(sender, e);
        }

        private void dataGridHands_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == COLUMN_IS_CHICKEN_HAND_INDEX)
            {
                dataGridView.RefreshEdit();
            }
        }

        private void dataGridHands_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridHands_CellContentClick(sender, e);
        }

        private void dataGridHands_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex != COLUMN_IS_CHICKEN_HAND_INDEX && !dataGridView.CurrentCell.ReadOnly)
            {
                int handId = GetSelectedHandId(e.RowIndex);
                int iCellValue = (int)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                switch (e.ColumnIndex)
                {
                    case COLUMN_PLAYER_WINNER_ID_INDEX:
                        _presenter.playerWinnerIdChanged(handId, iCellValue);
                        break;
                    case COLUMN_PLAYER_LOOSER_ID_INDEX:
                        _presenter.playerLooserIdChanged(handId, iCellValue);
                        break;
                    case COLUMN_POINTS_INDEX:
                        _presenter.PointsChanged(handId, iCellValue);
                        break;
                }
            }
        }
        #endregion

        #endregion

        #region ITableManagerForm implementation

        public void SetTournamentName(string tournamentName)
        {
            lblTournamentName.Text = tournamentName;
        }

        public void SetRoundId(int roundId)
        {
            lblRoundId.Text = roundId.ToString();
        }

        public void SetTableId(int tableId)
        {
            lblTableId.Text = tableId.ToString();
        }

        public void FillCombosPlayers(List<ComboItem> playersList)
        {
            comboEastPlayer.DataSource = playersList;
            comboEastPlayer.DisplayMember = "Text";
            comboEastPlayer.ValueMember = "Value";
            comboSouthPlayer.DataSource = new List<ComboItem>(playersList);
            comboSouthPlayer.DisplayMember = "Text";
            comboSouthPlayer.ValueMember = "Value";
            comboWestPlayer.DataSource = new List<ComboItem>(playersList);
            comboWestPlayer.DisplayMember = "Text";
            comboWestPlayer.ValueMember = "Value";
            comboNorthPlayer.DataSource = new List<ComboItem>(playersList);
            comboNorthPlayer.DisplayMember = "Text";
            comboNorthPlayer.ValueMember = "Value";
        }

        public void SelectPlayersInCombos(int playerEastIndex, int playerSouthIndex, 
            int playerWestIndex, int playerNorthIndex)
        {
            comboEastPlayer.SelectedIndex = playerEastIndex;
            comboSouthPlayer.SelectedIndex = playerSouthIndex;
            comboWestPlayer.SelectedIndex = playerWestIndex;
            comboNorthPlayer.SelectedIndex = playerNorthIndex;
        }

        public void FillDataGridHands(List<DataGridHand> dataGridHands)
        {
            dataGridView.DataSource = dataGridHands;
            //DisplayIndex
            dataGridView.Columns[COLUMN_ID].DisplayIndex = 0;
            dataGridView.Columns[COLUMN_PLAYER_WINNER_ID].DisplayIndex = 1;
            dataGridView.Columns[COLUMN_PLAYER_LOOSER_ID].DisplayIndex = 2;
            dataGridView.Columns[COLUMN_SCORE].DisplayIndex = 3;
            dataGridView.Columns[COLUMN_IS_CHICKEN_HAND].DisplayIndex = 4;
            dataGridView.Columns[COLUMN_PLAYER_EAST_SCORE].DisplayIndex = 5;
            dataGridView.Columns[COLUMN_PLAYER_SOUTH_SCORE].DisplayIndex = 6;
            dataGridView.Columns[COLUMN_PLAYER_WEST_SCORE].DisplayIndex = 7;
            dataGridView.Columns[COLUMN_PLAYER_NORTH_SCORE].DisplayIndex = 8;
            //AutosizeMode
            dataGridView.Columns[COLUMN_PLAYER_EAST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[COLUMN_PLAYER_SOUTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[COLUMN_PLAYER_WEST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[COLUMN_PLAYER_NORTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Column Visible
            dataGridView.Columns[COLUMN_TOURNAMENT_ID].Visible = false;
            dataGridView.Columns[COLUMN_ROUND_ID].Visible = false;
            dataGridView.Columns[COLUMN_TABLE_ID].Visible = false;
            //Column ReadOnly
            dataGridView.Columns[COLUMN_ID].ReadOnly = true;
            dataGridView.Columns[COLUMN_PLAYER_EAST_SCORE].ReadOnly = true;
            dataGridView.Columns[COLUMN_PLAYER_SOUTH_SCORE].ReadOnly = true;
            dataGridView.Columns[COLUMN_PLAYER_WEST_SCORE].ReadOnly = true;
            dataGridView.Columns[COLUMN_PLAYER_NORTH_SCORE].ReadOnly = true;
            //Column Header text
            dataGridView.Columns[COLUMN_ID].HeaderText = "Hand";
            dataGridView.Columns[COLUMN_PLAYER_WINNER_ID].HeaderText = "Winner Id";
            dataGridView.Columns[COLUMN_PLAYER_LOOSER_ID].HeaderText = "Looser Id";
            dataGridView.Columns[COLUMN_SCORE].HeaderText = "Hand points";
            dataGridView.Columns[COLUMN_IS_CHICKEN_HAND].HeaderText = "Chicken hand";
        }
        
        public void FillPlayersHandScores(int handId, int eastPlayerScore, int southPlayerScore, 
            int westPlayerScore, int northPlayerScore)
        {
            SetEastPlayerPoints(handId, eastPlayerScore);
            SetSouthPlayerPoints(handId, southPlayerScore);
            SetWestPlayerPoints(handId, westPlayerScore);
            SetNorthPlayerPoints(handId, northPlayerScore);
        }

        public void SetEastPlayerHeader(string selectedText)
        {
            dataGridView.Columns[COLUMN_PLAYER_EAST_SCORE].HeaderText = selectedText;
        }

        public void SetSouthPlayerHeader(string selectedText)
        {
            dataGridView.Columns[COLUMN_PLAYER_SOUTH_SCORE].HeaderText = selectedText;
        }

        public void SetWestPlayerHeader(string selectedText)
        {
            dataGridView.Columns[COLUMN_PLAYER_WEST_SCORE].HeaderText = selectedText;
        }

        public void SetNorthPlayerHeader(string selectedText)
        {
            dataGridView.Columns[COLUMN_PLAYER_NORTH_SCORE].HeaderText = selectedText;
        }

        public void SetEastPlayerPoints(int handId, int value)
        {
            dataGridView.Rows[handId - 1].Cells[COLUMN_PLAYER_EAST_SCORE].Value = value;
        }

        public void SetSouthPlayerPoints(int handId, int value)
        {
            dataGridView.Rows[handId - 1].Cells[COLUMN_PLAYER_SOUTH_SCORE].Value = value;
        }

        public void SetWestPlayerPoints(int handId, int value)
        {
            dataGridView.Rows[handId - 1].Cells[COLUMN_PLAYER_WEST_SCORE].Value = value;
        }

        public void SetNorthPlayerPoints(int handId, int value)
        {
            dataGridView.Rows[handId - 1].Cells[COLUMN_PLAYER_NORTH_SCORE].Value = value;
        }

        public void ShowDataGridHands()
        {
            dataGridView.Visible = true;
        }

        public void HideDataGridHands()
        {
            dataGridView.Visible = false;
        }

        #endregion

        #region Private

        private int GetSelectedHandId(int rowIndex)
        {
            return (int)dataGridView.Rows[rowIndex].Cells[COLUMN_ID_INDEX].Value;
        }

        #endregion
    }
}
