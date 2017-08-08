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
        private const int COLUMN_TOURNAMENT_ID_ORDER = 0;
        private const int COLUMN_ROUND_ID_ORDER = 1;
        private const int COLUMN_TABLE_ID_ORDER = 2;
        private const int COLUMN_ID_ORDER = 3;
        private const int COLUMN_PLAYER_WINNER_ID_ORDER = 4;
        private const int COLUMN_PLAYER_LOOSER_ID_ORDER = 5;
        private const int COLUMN_POINTS_ORDER = 6;
        private const int COLUMN_IS_CHICKEN_HAND_ORDER = 7;
        private const int COLUMN_PLAYER_EAST_POINTS_ORDER = 8;
        private const int COLUMN_PLAYER_SOUTH_POINTS_ORDER = 9;
        private const int COLUMN_PLAYER_WEST_POINTS_ORDER = 10;
        private const int COLUMN_PLAYER_NORTH_POINTS_ORDER = 11;

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

        //private void dataGridHands_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataGridHands.CurrentCell == null)
        //        return;
        //    else if (dataGridHands.CurrentCell.ColumnIndex == COLUMN_PLAYER_WINNER_ID_ORDER
        //        || dataGridHands.CurrentCell.ColumnIndex == COLUMN_PLAYER_LOOSER_ID_ORDER
        //        || dataGridHands.CurrentCell.ColumnIndex == COLUMN_POINTS_ORDER
        //        || dataGridHands.CurrentCell.ColumnIndex == COLUMN_PLAYER_EAST_POINTS_ORDER
        //        || dataGridHands.CurrentCell.ColumnIndex == COLUMN_PLAYER_SOUTH_POINTS_ORDER
        //        || dataGridHands.CurrentCell.ColumnIndex == COLUMN_PLAYER_WEST_POINTS_ORDER
        //        || dataGridHands.CurrentCell.ColumnIndex == COLUMN_PLAYER_NORTH_POINTS_ORDER)
        //        dataGridHands.Cursor = Cursors.IBeam;
        //    else if (dataGridHands.CurrentCell.ColumnIndex == COLUMN_IS_CHICKEN_HAND_ORDER)
        //        dataGridHands.Cursor = Cursors.Hand;
        //    else
        //        dataGridHands.Cursor = Cursors.Default;
        //}

        //private void dataGridHands_MouseLeave(object sender, EventArgs e)
        //{
        //    dataGridHands.Cursor = Cursors.Default;
        //}

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
        
        private void dataGridHands_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridHands.CurrentCell.ColumnIndex != COLUMN_IS_CHICKEN_HAND_ORDER)
                dataGridHands.BeginEdit(true);
        }

        private void dataGridHands_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridHands.CurrentCell.ColumnIndex == COLUMN_IS_CHICKEN_HAND_ORDER)
            {
                DataGridViewCheckBoxCell checkCell = dataGridHands.CurrentCell as DataGridViewCheckBoxCell;
                checkCell.Value = checkCell.Value == null || !((bool)checkCell.Value);
                dataGridHands.RefreshEdit();
                dataGridHands.NotifyCurrentCellDirty(true);
            }
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
