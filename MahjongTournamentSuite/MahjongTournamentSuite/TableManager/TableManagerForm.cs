using MahjongTournamentSuite.Model;
using MahjongTournamentSuite.Resources;
using System;
using System.Collections.Generic;
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
        private int _tournamentId;
        private int _roundId;
        private int _tableId;

        #endregion

        #region Constructor

        public TableManagerForm(int tournamentId, int roundId, int tableId)
        {
            InitializeComponent();
            _tournamentId = tournamentId;
            _roundId = roundId;
            _tableId = tableId;
        }

        #endregion

        #region Events

        #region Form

        private void TableManagerForm_Load(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter = Injector.provideTableManagerPresenter(this);
            _presenter.LoadForm(_tournamentId, _roundId, _tableId);
            ShowDefaultCursor();
        }

        private void TableManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Quitamos el foco el DGV para forzar el evento CellEndEdit y que se guarden los datos.
            lblTournamentName.Focus();
        }
        #endregion

        #region Logos Click
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
        #endregion

        #region Combos SelectionChangeCommitted
        private void comboEastPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameEastPlayerChanged(((ComboItem)comboEastPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }

        private void comboSouthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameSouthPlayerChanged(((ComboItem)comboSouthPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }

        private void comboWestPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameWestPlayerChanged(((ComboItem)comboWestPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }

        private void comboNorthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _presenter.NameNorthPlayerChanged(((ComboItem)comboNorthPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }
        #endregion

        #region TotalScores KeyPress
        private void tbEastPlayerTotalScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ShowWaitCursor();
                tbEastPlayerTotalScore_Leave(null, null);
                ShowDefaultCursor();
            }
        }

        private void tbSouthPlayerTotalScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ShowWaitCursor();
                tbSouthPlayerTotalScore_Leave(null, null);
                ShowDefaultCursor();
            }
        }

        private void tbWestPlayerTotalScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ShowWaitCursor();
                tbWestPlayerTotalScore_Leave(null, null);
                ShowDefaultCursor();
            }
        }

        private void tbNorthPlayerTotalScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ShowWaitCursor();
                tbNorthPlayerTotalScore_Leave(null, null);
                ShowDefaultCursor();
            }
        }
        #endregion

        #region TotalScores Leave
        private void tbEastPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbEastPlayerTotalScore.Text = _presenter.TotalScoreEastPlayerChanged(tbEastPlayerTotalScore.Text.Trim());
            ShowDefaultCursor();
        }

        private void tbSouthPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbSouthPlayerTotalScore.Text = _presenter.TotalScoreSouthPlayerChanged(tbSouthPlayerTotalScore.Text);
            ShowDefaultCursor();
        }

        private void tbWestPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbWestPlayerTotalScore.Text = _presenter.TotalScoreWestPlayerChanged(tbWestPlayerTotalScore.Text);
            ShowDefaultCursor();
        }

        private void tbNorthPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbNorthPlayerTotalScore.Text = _presenter.TotalScoreNorthPlayerChanged(tbNorthPlayerTotalScore.Text);
            ShowDefaultCursor();
        }
        #endregion

        #region Cells
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor = 
                    dgv.CurrentCell.ReadOnly ? 
                    MyConstants.GREEN_MM_DARKEST : 
                    MyConstants.GREEN_MM_DARKER;
            }
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
                    CellIsChickenHandChanged(e.RowIndex, e.ColumnIndex);
                }
            }
        }

        private void dgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space && dgv.CurrentCell != null 
                && dgv.CurrentCell.RowIndex >= 0 
                && dgv.CurrentCell.OwningColumn.Name.Equals(COLUMN_IS_CHICKEN_HAND))
                CellIsChickenHandChanged(dgv.CurrentCell.RowIndex,
                    dgv.CurrentCell.ColumnIndex);
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                #region COLUMN_PLAYER_WINNER_ID
                if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_WINNER_ID))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_WINNER_ID].Value;
                    DGVCancelEdit();
                    string returnedValue = _presenter.PlayerWinnerIdChanged(handId, newValue);
                    dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_WINNER_ID].Value = returnedValue;
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_LOOSER_ID
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_LOOSER_ID))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_LOOSER_ID].Value;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_LOOSER_ID].Value =
                        _presenter.PlayerLooserIdChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_HAND_SCORE
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_HAND_SCORE))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_HAND_SCORE].Value;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[COLUMN_HAND_SCORE].Value = 
                        _presenter.HandScoreChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_EAST_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_EAST_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_EAST_PENALTY].Value;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_EAST_PENALTY].Value =
                        _presenter.PlayerEastPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_SOUTH_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_SOUTH_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_SOUTH_PENALTY].Value;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_SOUTH_PENALTY].Value =
                        _presenter.PlayerSouthPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_WEST_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_WEST_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_WEST_PENALTY].Value;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_WEST_PENALTY].Value =
                        _presenter.PlayerWestPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_NORTH_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_PLAYER_NORTH_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_NORTH_PENALTY].Value;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[COLUMN_PLAYER_NORTH_PENALTY].Value =
                        _presenter.PlayerNorthPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
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

        public void FillDGV(List<DGVHand> dgvHands)
        {
            dgv.DataSource = dgvHands;
            
            //Column Visible
            dgv.Columns[COLUMN_TOURNAMENT_ID].Visible = false;
            dgv.Columns[COLUMN_ROUND_ID].Visible = false;
            dgv.Columns[COLUMN_TABLE_ID].Visible = false;
            //Column ReadOnly
            dgv.Columns[COLUMN_HAND_ID].ReadOnly = true;
            dgv.Columns[COLUMN_IS_CHICKEN_HAND].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].ReadOnly = true;
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
            //AutoSizeMode
            dgv.Columns[COLUMN_HAND_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_IS_CHICKEN_HAND].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_EAST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_SOUTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_WEST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_NORTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_EAST_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_SOUTH_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_WEST_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[COLUMN_PLAYER_NORTH_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
        }
        
        public void FillHandPlayersScoresCells(DGVHand dgvHand)
        {
            SetEastPlayerHandScoreCell(dgvHand.HandId, dgvHand.PlayerEastScore);
            SetSouthPlayerHandScoreCell(dgvHand.HandId, dgvHand.PlayerSouthScore);
            SetWestPlayerHandScoreCell(dgvHand.HandId, dgvHand.PlayerWestScore);
            SetNorthPlayerHandScoreCell(dgvHand.HandId, dgvHand.PlayerNorthScore);
        }

        public void FillAllTotalScoreTextBoxes(string playerEastTotalScore, string playerSouthTotalScore, 
            string playerWestTotalScore, string playerNorthTotalScore)
        {
            SetEastPlayerTotalScoreTextBox(playerEastTotalScore);
            SetSouthPlayerTotalScoreTextBox(playerSouthTotalScore);
            SetWestPlayerTotalScoreTextBox(playerWestTotalScore);
            SetNorthPlayerTotalScoreTextBox(playerNorthTotalScore);
        }

        public void FillAllTotalPointsTextBoxes(string playerEastTotalPoints, string playerSouthTotalPoints,
            string playerWestTotalPoints, string playerNorthTotalPoints)
        {
            SetEastPlayerTotalPointsTextBox(playerEastTotalPoints);
            SetSouthPlayerTotalPointsTextBox(playerSouthTotalPoints);
            SetWestPlayerTotalPointsTextBox(playerWestTotalPoints);
            SetNorthPlayerTotalPointsTextBox(playerNorthTotalPoints);
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

        public void UncheckChickenHand(int handId)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if(((int)row.Cells[COLUMN_HAND_ID].Value) == handId)
                {
                    CellIsChickenHandChanged(row.Index, row.Cells[COLUMN_IS_CHICKEN_HAND].ColumnIndex);
                    MessageBox.Show("Chicken hand has been unchecked because it need to have Winner, Looser and Hand Score.", "Wrong Chicken hand");
                    return;
                }
            }
        }

        public void ShowPanelTotals()
        {
            panelTotals.Visible = true;
        }

        public void HidePanelTotals()
        {
            panelTotals.Visible = false;
        }

        public void EnableTotalScoresTextBoxes()
        {
            tbEastPlayerTotalScore.Enabled = true;
            tbSouthPlayerTotalScore.Enabled = true;
            tbWestPlayerTotalScore.Enabled = true;
            tbNorthPlayerTotalScore.Enabled = true;
        }

        public void DisableTotalScoresTextBoxes()
        {
            tbEastPlayerTotalScore.Enabled = false;
            tbSouthPlayerTotalScore.Enabled = false;
            tbWestPlayerTotalScore.Enabled = false;
            tbNorthPlayerTotalScore.Enabled = false;
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

        public void ShowErrorTotalScores()
        {
            tbEastPlayerTotalScore.ForeColor = MyConstants.RED_CANCEL;
            tbSouthPlayerTotalScore.ForeColor = MyConstants.RED_CANCEL;
            tbWestPlayerTotalScore.ForeColor = MyConstants.RED_CANCEL;
            tbNorthPlayerTotalScore.ForeColor = MyConstants.RED_CANCEL;
        }

        public void HideErrorTotalScores()
        {

            tbEastPlayerTotalScore.ForeColor = SystemColors.ControlText;
            tbSouthPlayerTotalScore.ForeColor = SystemColors.ControlText;
            tbWestPlayerTotalScore.ForeColor = SystemColors.ControlText;
            tbNorthPlayerTotalScore.ForeColor = SystemColors.ControlText;
        }

        public void ShowMessageChickenHandNeedWinnerLooserAndScore()
        {
            MessageBox.Show("Chicken hand need to have Winner, Looser and Hand Score.", "Wrong Chicken hand");
        }

        public void CleanTotalPoints()
        {
            tbEastPlayerTotalPoints.Text = string.Empty;
            tbSouthPlayerTotalPoints.Text = string.Empty;
            tbWestPlayerTotalPoints.Text = string.Empty;
            tbNorthPlayerTotalPoints.Text = string.Empty;
        }

        public void PlayKoSound()
        {
            SystemSounds.Exclamation.Play();
        }

        #endregion

        #region Private

        private void CellIsChickenHandChanged(int rowIndex, int colIndex)
        {
            ShowWaitCursor();
            DataGridViewCheckBoxCell checkCell = dgv.Rows[rowIndex].Cells[colIndex] as DataGridViewCheckBoxCell;            
            checkCell.Value = _presenter.IsChickenHandChanged(GetSelectedHandId(rowIndex));
            dgv.BeginEdit(true);
            dgv.RefreshEdit();
            ShowDefaultCursor();
        }

        private int GetSelectedHandId(int rowIndex)
        {
            return (int)dgv.Rows[rowIndex].Cells[COLUMN_HAND_ID].Value;
        }

        private void SetEastPlayerHandScoreCell(int handId, string value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_EAST_SCORE].Value = value;
        }

        private void SetSouthPlayerHandScoreCell(int handId, string value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_SOUTH_SCORE].Value = value;
        }

        private void SetWestPlayerHandScoreCell(int handId, string value)
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_WEST_SCORE].Value = value;
        }

        private void SetNorthPlayerHandScoreCell(int handId, string value) 
        {
            dgv.Rows[handId - 1].Cells[COLUMN_PLAYER_NORTH_SCORE].Value = value;
        }

        private void SetEastPlayerTotalScoreTextBox(string value)
        {
            tbEastPlayerTotalScore.Text = value;
        }

        private void SetSouthPlayerTotalScoreTextBox(string value)
        {
            tbSouthPlayerTotalScore.Text = value;
        }

        private void SetWestPlayerTotalScoreTextBox(string value)
        {
            tbWestPlayerTotalScore.Text = value;
        }

        private void SetNorthPlayerTotalScoreTextBox(string value)
        {
            tbNorthPlayerTotalScore.Text = value;
        }

        private void SetEastPlayerTotalPointsTextBox(string value)
        {
            tbEastPlayerTotalPoints.Text = value;
        }

        private void SetSouthPlayerTotalPointsTextBox(string value)
        {
            tbSouthPlayerTotalPoints.Text = value;
        }

        private void SetWestPlayerTotalPointsTextBox(string value)
        {
            tbWestPlayerTotalPoints.Text = value;
        }

        private void SetNorthPlayerTotalPointsTextBox(string value)
        {
            tbNorthPlayerTotalPoints.Text = value;
        }

        #endregion
    }
}
