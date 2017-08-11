using MahjongTournamentSuite.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        private const string COLUMN_POINTS = "Points";
        private const string COLUMN_IS_CHICKEN_HAND = "IsChickenHand";
        private const string COLUMN_PLAYER_EAST_POINTS = "PlayerEastPoints";
        private const string COLUMN_PLAYER_SOUTH_POINTS = "PlayerSouthPoints";
        private const string COLUMN_PLAYER_WEST_POINTS = "PlayerWestPoints";
        private const string COLUMN_PLAYER_NORTH_POINTS = "PlayerNorthPoints";
        private const int COLUMN_ID_INDEX = 3;
        private const int COLUMN_PLAYER_WINNER_ID_INDEX = 4;
        private const int COLUMN_PLAYER_LOOSER_ID_INDEX = 5;
        private const int COLUMN_POINTS_INDEX = 6;
        private const int COLUMN_IS_CHICKEN_HAND_INDEX = 7;

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
            _presenter.LoadTable(_tournamentId, _roundId, _tableId);
        }

        #endregion

        #region Lifecycle

        #endregion

        #region Events

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Combos
        private void comboEastPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameEastPlayerChanged(((ComboItem)comboEastPlayer.SelectedItem).Text);
        }

        private void comboSouthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameSouthPlayerChanged(((ComboItem)comboSouthPlayer.SelectedItem).Text);
        }

        private void comboWestPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameWestPlayerChanged(((ComboItem)comboWestPlayer.SelectedItem).Text);
        }

        private void comboNorthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _presenter.NameNorthPlayerChanged(((ComboItem)comboNorthPlayer.SelectedItem).Text);
        }
        #endregion

        #region Cells
        private void dataGridHands_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex != COLUMN_IS_CHICKEN_HAND_INDEX)
                dataGridHands.BeginEdit(true);
        }

        private void dataGridHands_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == COLUMN_IS_CHICKEN_HAND_INDEX)
            {
                DataGridViewCheckBoxCell checkCell = dataGridHands.CurrentCell as DataGridViewCheckBoxCell;
                checkCell.Value = checkCell.Value == null || !((bool)checkCell.Value);
                dataGridHands.RefreshEdit();
                dataGridHands.NotifyCurrentCellDirty(true);
            }
        }

        private void dataGridHands_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dataGridHands.Rows[e.RowIndex];
                object cellValue = row.Cells[e.ColumnIndex].Value;
                switch (e.ColumnIndex)
                {
                    case COLUMN_PLAYER_WINNER_ID_INDEX:
                        //if (cellValue == null || ((string)cellValue).Length == 0) cellValue = 0;
                        //_presenter.playerWinnerIdChanged((int)row.Cells[COLUMN_ID_INDEX].Value, (int)cellValue);
                        break;
                    case COLUMN_PLAYER_LOOSER_ID_INDEX:
                        //if (cellValue == null || ((string)cellValue).Length == 0) cellValue = 0;
                        //_presenter.playerLooserIdChanged((int)row.Cells[COLUMN_ID_INDEX].Value, (int)cellValue);
                        break;
                    case COLUMN_POINTS_INDEX:
                        //if (cellValue == null || ((string)cellValue).Length == 0) cellValue = 0;
                        //_presenter.PointsChanged((int)row.Cells[COLUMN_ID_INDEX].Value, (int)cellValue);
                        break;
                    case COLUMN_IS_CHICKEN_HAND_INDEX:
                        //if (cellValue == null) cellValue = true;
                        //_presenter.IsChickenHandChanged((int)row.Cells[COLUMN_ID_INDEX].Value, (bool)cellValue);
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

        public void FillDataGridHands(List<DBHand> hands)
        {
            dataGridHands.DataSource = hands;

            //AutosizeMode
            dataGridHands.Columns[COLUMN_PLAYER_EAST_POINTS].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridHands.Columns[COLUMN_PLAYER_SOUTH_POINTS].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridHands.Columns[COLUMN_PLAYER_WEST_POINTS].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridHands.Columns[COLUMN_PLAYER_NORTH_POINTS].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Column Visible
            dataGridHands.Columns[COLUMN_TOURNAMENT_ID].Visible = false;
            dataGridHands.Columns[COLUMN_ROUND_ID].Visible = false;
            dataGridHands.Columns[COLUMN_TABLE_ID].Visible = false;
            //Column ReadOnly
            dataGridHands.Columns[COLUMN_ID].ReadOnly = true;
            dataGridHands.Columns[COLUMN_PLAYER_EAST_POINTS].ReadOnly = true;
            dataGridHands.Columns[COLUMN_PLAYER_SOUTH_POINTS].ReadOnly = true;
            dataGridHands.Columns[COLUMN_PLAYER_WEST_POINTS].ReadOnly = true;
            dataGridHands.Columns[COLUMN_PLAYER_NORTH_POINTS].ReadOnly = true;
            //Column Header text
            dataGridHands.Columns[COLUMN_ID].HeaderText = "Hand";
            dataGridHands.Columns[COLUMN_PLAYER_WINNER_ID].HeaderText = "Winner Id";
            dataGridHands.Columns[COLUMN_PLAYER_LOOSER_ID].HeaderText = "Looser Id";
            dataGridHands.Columns[COLUMN_POINTS].HeaderText = "Hand points";
            dataGridHands.Columns[COLUMN_IS_CHICKEN_HAND].HeaderText = "Chicken hand";
            dataGridHands.Columns[COLUMN_PLAYER_EAST_POINTS].HeaderText = "Player east";
            dataGridHands.Columns[COLUMN_PLAYER_SOUTH_POINTS].HeaderText = "Player south";
            dataGridHands.Columns[COLUMN_PLAYER_WEST_POINTS].HeaderText = "Player west";
            dataGridHands.Columns[COLUMN_PLAYER_NORTH_POINTS].HeaderText = "Player north";
        }

        public void SetDataGridHeaderEastPlayerText(string selectedText)
        {
            dataGridHands.Columns[COLUMN_PLAYER_EAST_POINTS].HeaderText = selectedText;
        }

        public void SetDataGridHeaderSouthPlayerText(string selectedText)
        {
            dataGridHands.Columns[COLUMN_PLAYER_SOUTH_POINTS].HeaderText = selectedText;
        }

        public void SetDataGridHeaderWestPlayerText(string selectedText)
        {
            dataGridHands.Columns[COLUMN_PLAYER_WEST_POINTS].HeaderText = selectedText;
        }

        public void SetDataGridHeaderNorthPlayerText(string selectedText)
        {
            dataGridHands.Columns[COLUMN_PLAYER_NORTH_POINTS].HeaderText = selectedText;
        }

        #endregion

        #region Private

        #endregion

    }
}
