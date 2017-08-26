﻿using MahjongTournamentSuite.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace MahjongTournamentSuite.TableManager
{
    partial class TableManagerForm : Form, ITableManagerForm
    {
        #region Constants
        
        private const string COLUMN_TOURNAMENT_ID = "HandTournamentId";
        private const string COLUMN_ROUND_ID = "HandRoundId";
        private const string COLUMN_TABLE_ID = "HandTableId";
        private const string COLUMN_HAND_ID = "HandId";
        private const string COLUMN_PLAYER_WINNER_ID = "PlayerWinnerId";
        private const string COLUMN_PLAYER_LOOSER_ID = "PlayerLooserId";
        private const string COLUMN_HAND_SCORE = "HandScore";
        private const string COLUMN_IS_CHICKEN_HAND = "IsChickenHand";
        private const string COLUMN_PLAYER_EAST_PENALTY = "PlayerEastPenalty";
        private const string COLUMN_PLAYER_SOUTH_PENALTY = "PlayerSouthPenalty";
        private const string COLUMN_PLAYER_WEST_PENALTY = "PlayerWestPenalty";
        private const string COLUMN_PLAYER_NORTH_PENALTY = "PlayerNorthPenalty";
        private const string COLUMN_PLAYER_EAST_SCORE = "PlayerEastScore";
        private const string COLUMN_PLAYER_SOUTH_SCORE = "PlayerSouthScore";
        private const string COLUMN_PLAYER_WEST_SCORE = "PlayerWestScore";
        private const string COLUMN_PLAYER_NORTH_SCORE = "PlayerNorthScore";

        #endregion

        #region Fields

        private ITableManagerPresenter _presenter;

        #endregion

        #region Constructor

        public TableManagerForm(int tournamentId, int roundId, int tableId)
        {
            InitializeComponent();
            ShowWaitCursor();
            _presenter = Injector.provideTableManagerPresenter(this);
            _presenter.LoadForm(tournamentId, roundId, tableId);
            ShowDefaultCursor();
        }

        #endregion
        
        #region Events

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            ShowWaitCursor();
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
            ShowDefaultCursor();
        }

        private void comboEastPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameEastPlayerChanged(int.Parse(((ComboItem)comboEastPlayer.SelectedItem).Value));
            ShowDefaultCursor();
        }

        private void comboSouthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameSouthPlayerChanged(int.Parse(((ComboItem)comboSouthPlayer.SelectedItem).Value));
            ShowDefaultCursor();
        }

        private void comboWestPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameWestPlayerChanged(int.Parse(((ComboItem)comboWestPlayer.SelectedItem).Value));
            ShowDefaultCursor();
        }

        private void comboNorthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameNorthPlayerChanged(int.Parse(((ComboItem)comboNorthPlayer.SelectedItem).Value));
            ShowDefaultCursor();
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_WINNER_ID)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_LOOSER_ID)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_HAND_SCORE)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_EAST_PENALTY)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_SOUTH_PENALTY)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_WEST_PENALTY)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_NORTH_PENALTY))
                    dgv.BeginEdit(true);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_IS_CHICKEN_HAND))
                {
                    ShowWaitCursor();
                    DataGridViewCheckBoxCell checkCell = dgv.CurrentCell as DataGridViewCheckBoxCell;
                    checkCell.Value = checkCell.Value == null || !((bool)checkCell.Value);
                    dgv.BeginEdit(true);
                    dgv.RefreshEdit();
                    _presenter.IsChickenHandChanged(GetSelectedHandId(e.RowIndex), (bool)checkCell.Value);
                    ShowDefaultCursor();
                }
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                #region COLUMN_PLAYER_WINNER_ID
                if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_WINNER_ID))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                        _presenter.PlayerWinnerIdChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_LOOSER_ID
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_LOOSER_ID))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                        _presenter.PlayerLooserIdChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_HAND_SCORE
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_HAND_SCORE))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 
                        _presenter.HandScoreChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_EAST_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_EAST_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                        _presenter.PlayerEastPenalytChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_SOUTH_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_SOUTH_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                        _presenter.PlayerSouthPenalytChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_WEST_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_WEST_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                        _presenter.PlayerWestPenalytChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_NORTH_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_NORTH_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string newValue = ((string)e.FormattedValue).Trim();
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                        _presenter.PlayerNorthPenalytChanged(handId, previousValue, newValue);
                    ShowDefaultCursor();
                }
                #endregion
            }
        }

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

        public void FillDGV(List<DGVHand> dgvHands)
        {
            dgv.DataSource = dgvHands;
            //Column Visible
            dgv.Columns[COLUMN_TOURNAMENT_ID].Visible = false;
            dgv.Columns[COLUMN_ROUND_ID].Visible = false;
            dgv.Columns[COLUMN_TABLE_ID].Visible = false;
            //DisplayIndex
            dgv.Columns[COLUMN_HAND_ID].DisplayIndex = 0;
            dgv.Columns[COLUMN_PLAYER_WINNER_ID].DisplayIndex = 1;
            dgv.Columns[COLUMN_PLAYER_LOOSER_ID].DisplayIndex = 2;
            dgv.Columns[COLUMN_HAND_SCORE].DisplayIndex = 3;
            dgv.Columns[COLUMN_IS_CHICKEN_HAND].DisplayIndex = 4;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].DisplayIndex = 5;
            dgv.Columns[COLUMN_PLAYER_EAST_PENALTY].DisplayIndex = 6;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].DisplayIndex = 7;
            dgv.Columns[COLUMN_PLAYER_SOUTH_PENALTY].DisplayIndex = 8;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].DisplayIndex = 9;
            dgv.Columns[COLUMN_PLAYER_WEST_PENALTY].DisplayIndex = 10;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].DisplayIndex = 11;
            dgv.Columns[COLUMN_PLAYER_NORTH_PENALTY].DisplayIndex = 12;
            //Sortable
            dgv.Columns[COLUMN_HAND_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_WINNER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_LOOSER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_HAND_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_IS_CHICKEN_HAND].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_EAST_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_SOUTH_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_WEST_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[COLUMN_PLAYER_NORTH_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            //Column ReadOnly
            dgv.Columns[COLUMN_HAND_ID].ReadOnly = true;
            dgv.Columns[COLUMN_IS_CHICKEN_HAND].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].ReadOnly = true;
            //Readonly columns BackColor
            dgv.Columns[COLUMN_HAND_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgv.Columns[COLUMN_HAND_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //Column Header text
            dgv.Columns[COLUMN_HAND_ID].HeaderText = "Hand";
            dgv.Columns[COLUMN_PLAYER_WINNER_ID].HeaderText = "Winner Id";
            dgv.Columns[COLUMN_PLAYER_LOOSER_ID].HeaderText = "Looser Id";
            dgv.Columns[COLUMN_HAND_SCORE].HeaderText = "Hand points";
            dgv.Columns[COLUMN_IS_CHICKEN_HAND].HeaderText = "Chicken hand";
            dgv.Columns[COLUMN_PLAYER_EAST_PENALTY].HeaderText = "Penalty";
            dgv.Columns[COLUMN_PLAYER_SOUTH_PENALTY].HeaderText = "Penalty";
            dgv.Columns[COLUMN_PLAYER_WEST_PENALTY].HeaderText = "Penalty";
            dgv.Columns[COLUMN_PLAYER_NORTH_PENALTY].HeaderText = "Penalty";
            //AutoSizeMode
            dgv.Columns[COLUMN_HAND_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[COLUMN_PLAYER_EAST_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_SOUTH_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_WEST_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_NORTH_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].HeaderText = selectedText;
        }

        public void SetSouthPlayerHeader(string selectedText)
        {
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].HeaderText = selectedText;
        }

        public void SetWestPlayerHeader(string selectedText)
        {
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].HeaderText = selectedText;
        }

        public void SetNorthPlayerHeader(string selectedText)
        {
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].HeaderText = selectedText;
        }

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        public void ShowWaitCursor()
        {
            Cursor = Cursors.WaitCursor;
        }

        public void ShowDefaultCursor()
        {
            Cursor = Cursors.Default;
        }

        public void ShowDataGridHands()
        {
            dgv.Visible = true;
        }

        public void HideDataGridHands()
        {
            dgv.Visible = false;
        }

        public void PlayKoSound()
        {
            SystemSounds.Exclamation.Play();
        }

        #endregion

        #region Private

        private int GetSelectedHandId(int rowIndex)
        {
            return (int)dgv.Rows[rowIndex].Cells[COLUMN_HAND_ID].Value;
        }

        private void SetEastPlayerPoints(int handId, int value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_EAST_SCORE].Value = value;
        }

        private void SetSouthPlayerPoints(int handId, int value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_SOUTH_SCORE].Value = value;
        }

        private void SetWestPlayerPoints(int handId, int value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_WEST_SCORE].Value = value;
        }

        private void SetNorthPlayerPoints(int handId, int value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_NORTH_SCORE].Value = value;
        }

        #endregion
    }
}
