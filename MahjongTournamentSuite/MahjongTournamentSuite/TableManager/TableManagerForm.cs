using MahjongTournamentSuite.ViewModel;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite._Data.DataModel;
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
        #region Fields

        private ITableManagerController _controller;
        private int _tournamentId;
        private int _roundId;
        private int _tableId;

        #endregion

        #region Constructor

        public TableManagerForm(int tournamentId, int roundId, int tableId)
        {
            InitializeComponent();
            _controller = Injector.provideTableManagerController(this);
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
            _controller.LoadForm(_tournamentId, _roundId, _tableId);
            ShowDefaultCursor();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
                return EnterPressedBehaviour();
            else if (keyData == Keys.Tab)
                return TabPressedBehaviour();
            else if (keyData == Keys.Space)
                return SpacePressedBehaviour();
            else if (keyData == Keys.Delete)
                return SuprPressedBehaviour();
            else
                return false;
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

        #region CheckBoxes
        private void cbCompleted_CheckedChanged(object sender, EventArgs e)
        {
            _controller.IsCompletedCheckedChanged(cbIsCompleted.Checked);
        }

        private void cbUseTotalsOnly_CheckedChanged(object sender, EventArgs e)
        {
            _controller.UseTotalsOnlyCheckedChanged(cbUseTotalsOnly.Checked);
        }
        #endregion

        #region Combos
        private void comboEastPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.NameEastPlayerChanged(((ComboItem)comboEastPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }

        private void comboSouthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.NameSouthPlayerChanged(((ComboItem)comboSouthPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }

        private void comboWestPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.NameWestPlayerChanged(((ComboItem)comboWestPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }

        private void comboNorthPlayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowWaitCursor();
            _controller.NameNorthPlayerChanged(((ComboItem)comboNorthPlayer.SelectedItem).Value);
            ShowDefaultCursor();
        }
        #endregion

        #region TotalScores Leave
        private void tbEastPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbEastPlayerTotalScore.Text = _controller.TotalScoreEastPlayerChanged(tbEastPlayerTotalScore.Text.Trim());
            ShowDefaultCursor();
        }

        private void tbSouthPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbSouthPlayerTotalScore.Text = _controller.TotalScoreSouthPlayerChanged(tbSouthPlayerTotalScore.Text);
            ShowDefaultCursor();
        }

        private void tbWestPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbWestPlayerTotalScore.Text = _controller.TotalScoreWestPlayerChanged(tbWestPlayerTotalScore.Text);
            ShowDefaultCursor();
        }

        private void tbNorthPlayerTotalScore_Leave(object sender, EventArgs e)
        {
            ShowWaitCursor();
            tbNorthPlayerTotalScore.Text = _controller.TotalScoreNorthPlayerChanged(tbNorthPlayerTotalScore.Text);
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
                    ColorConstants.GREEN_MM_DARKEST :
                    ColorConstants.GREEN_MM_DARKER;
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_WINNER_ID)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_LOOSER_ID)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_HAND_SCORE)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_EAST_PENALTY)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_SOUTH_PENALTY)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_WEST_PENALTY)
                    || dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_NORTH_PENALTY))
                    dgv.BeginEdit(true);
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_IS_CHICKEN_HAND))
                {
                    CellIsChickenHandChanged(e.RowIndex, e.ColumnIndex);
                }
            }
        }
        
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                #region COLUMN_PLAYER_WINNER_ID
                if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_WINNER_ID))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_WINNER_ID].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    string returnedValue = _controller.PlayerWinnerIdChanged(handId, newValue);
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_WINNER_ID].Value = returnedValue;
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_LOOSER_ID
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_LOOSER_ID))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_LOOSER_ID].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_LOOSER_ID].Value =
                        _controller.PlayerLooserIdChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_HAND_SCORE
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_HAND_SCORE))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_HAND_SCORE].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_HAND_SCORE].Value = 
                        _controller.HandScoreChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_EAST_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_EAST_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_EAST_PENALTY].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_EAST_PENALTY].Value =
                        _controller.PlayerEastPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_SOUTH_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_SOUTH_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_SOUTH_PENALTY].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_SOUTH_PENALTY].Value =
                        _controller.PlayerSouthPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_WEST_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_WEST_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_WEST_PENALTY].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_WEST_PENALTY].Value =
                        _controller.PlayerWestPenalytChanged(handId, newValue);
                    ShowDefaultCursor();
                }
                #endregion
                #region COLUMN_PLAYER_NORTH_PENALTY
                else if (dgv.Columns[e.ColumnIndex].Name.Equals(VHand.COLUMN_PLAYER_NORTH_PENALTY))
                {
                    ShowWaitCursor();
                    int handId = GetSelectedHandId(e.RowIndex);
                    string newValue = (string)dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_NORTH_PENALTY].Value;
                    if (newValue == null)
                        newValue = string.Empty;
                    DGVCancelEdit();
                    dgv.Rows[e.RowIndex].Cells[VHand.COLUMN_PLAYER_NORTH_PENALTY].Value =
                        _controller.PlayerNorthPenalytChanged(handId, newValue);
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

        public void SetIsCompleted(bool isCompleted)
        {
            cbIsCompleted.Checked = isCompleted;
        }

        public void SetUseTotalsOnly(bool useTotalsOnly)
        {
            cbUseTotalsOnly.Checked = useTotalsOnly;
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
            dgv.Columns[VHand.COLUMN_TOURNAMENT_ID].Visible = false;
            dgv.Columns[VHand.COLUMN_ROUND_ID].Visible = false;
            dgv.Columns[VHand.COLUMN_TABLE_ID].Visible = false;
            //Column ReadOnly
            dgv.Columns[VHand.COLUMN_HAND_ID].ReadOnly = true;
            dgv.Columns[VHand.COLUMN_IS_CHICKEN_HAND].ReadOnly = true;
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].ReadOnly = true;
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].ReadOnly = true;
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].ReadOnly = true;
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].ReadOnly = true;
            //Column Header text
            dgv.Columns[VHand.COLUMN_HAND_ID].HeaderText = "Hand";
            dgv.Columns[VHand.COLUMN_PLAYER_WINNER_ID].HeaderText = "Winner Id";
            dgv.Columns[VHand.COLUMN_PLAYER_LOOSER_ID].HeaderText = "Looser Id";
            dgv.Columns[VHand.COLUMN_HAND_SCORE].HeaderText = "Hand points";
            dgv.Columns[VHand.COLUMN_IS_CHICKEN_HAND].HeaderText = "Chicken hand";
            dgv.Columns[VHand.COLUMN_PLAYER_EAST_PENALTY].HeaderText = "Penalty";
            dgv.Columns[VHand.COLUMN_PLAYER_SOUTH_PENALTY].HeaderText = "Penalty";
            dgv.Columns[VHand.COLUMN_PLAYER_WEST_PENALTY].HeaderText = "Penalty";
            dgv.Columns[VHand.COLUMN_PLAYER_NORTH_PENALTY].HeaderText = "Penalty";
            //DisplayIndex
            dgv.Columns[VHand.COLUMN_HAND_ID].DisplayIndex = 0;
            dgv.Columns[VHand.COLUMN_PLAYER_WINNER_ID].DisplayIndex = 1;
            dgv.Columns[VHand.COLUMN_PLAYER_LOOSER_ID].DisplayIndex = 2;
            dgv.Columns[VHand.COLUMN_HAND_SCORE].DisplayIndex = 3;
            dgv.Columns[VHand.COLUMN_IS_CHICKEN_HAND].DisplayIndex = 4;
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].DisplayIndex = 5;
            dgv.Columns[VHand.COLUMN_PLAYER_EAST_PENALTY].DisplayIndex = 6;
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].DisplayIndex = 7;
            dgv.Columns[VHand.COLUMN_PLAYER_SOUTH_PENALTY].DisplayIndex = 8;
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].DisplayIndex = 9;
            dgv.Columns[VHand.COLUMN_PLAYER_WEST_PENALTY].DisplayIndex = 10;
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].DisplayIndex = 11;
            dgv.Columns[VHand.COLUMN_PLAYER_NORTH_PENALTY].DisplayIndex = 12;
            //Sortable
            dgv.Columns[VHand.COLUMN_HAND_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_PLAYER_WINNER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_PLAYER_LOOSER_ID].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_HAND_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_IS_CHICKEN_HAND].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_PLAYER_EAST_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_PLAYER_SOUTH_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_PLAYER_WEST_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[VHand.COLUMN_PLAYER_NORTH_PENALTY].SortMode = DataGridViewColumnSortMode.NotSortable;
            //AutoSizeMode
            dgv.Columns[VHand.COLUMN_HAND_ID].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[VHand.COLUMN_IS_CHICKEN_HAND].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[VHand.COLUMN_PLAYER_EAST_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[VHand.COLUMN_PLAYER_SOUTH_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[VHand.COLUMN_PLAYER_WEST_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns[VHand.COLUMN_PLAYER_NORTH_PENALTY].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //Readonly columns BackColor
            dgv.Columns[VHand.COLUMN_HAND_ID].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly columns ForeColor
            dgv.Columns[VHand.COLUMN_HAND_ID].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
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
            dgv.Columns[DGVHand.COLUMN_PLAYER_EAST_SCORE].HeaderText = selectedText;
        }

        public void SetSouthPlayerHeader(string selectedText)
        {
            dgv.Columns[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].HeaderText = selectedText;
        }

        public void SetWestPlayerHeader(string selectedText)
        {
            dgv.Columns[DGVHand.COLUMN_PLAYER_WEST_SCORE].HeaderText = selectedText;
        }

        public void SetNorthPlayerHeader(string selectedText)
        {
            dgv.Columns[DGVHand.COLUMN_PLAYER_NORTH_SCORE].HeaderText = selectedText;
        }

        public void DGVCancelEdit()
        {
            dgv.CancelEdit();
        }

        public void UncheckChickenHand(int handId)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if(((int)row.Cells[VHand.COLUMN_HAND_ID].Value) == handId)
                {
                    CellIsChickenHandChanged(row.Index, row.Cells[VHand.COLUMN_IS_CHICKEN_HAND].ColumnIndex);
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

        public void EnableIsCompleted()
        {
            cbIsCompleted.Enabled = true;
        }

        public void DisableIsCompleted()
        {
            cbIsCompleted.Enabled = false;
        }

        public void EnableUseTotalsOnly()
        {
            cbUseTotalsOnly.Enabled = true;
        }

        public void DisableUseTotalsOnly()
        {
            cbUseTotalsOnly.Enabled = false;
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
            tbEastPlayerTotalScore.ForeColor = ColorConstants.RED_CANCEL;
            tbSouthPlayerTotalScore.ForeColor = ColorConstants.RED_CANCEL;
            tbWestPlayerTotalScore.ForeColor = ColorConstants.RED_CANCEL;
            tbNorthPlayerTotalScore.ForeColor = ColorConstants.RED_CANCEL;
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

        public void ShowMessageChickenHandNeedWinnerAtLeastInUseTotalsOnlyMode()
        {
            MessageBox.Show("In \"Use Totals Only\" mode, Chicken hand must have only the winner or all the fields(winner, looser and hand points).", "Wrong Chicken hand");
        }

        public void ShowMessageInvalidChickenHandValue()
        {
            MessageBox.Show("Chicken hand can´t be higher than 16 points.", "Wrong Chicken hand");
        }

        public void ShowMessageCantCompleteTableWithUseTotalsOnlyChecked()
        {
            MessageBox.Show("You can´t complete a table if \"Use Totals Only\" mode is enabled.", "Use Totals Only mode enabled");
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

        private bool EnterPressedBehaviour()
        {
            #region TEXTBOX EAST TOTAL SCORE
            if (tbEastPlayerTotalScore.Focused)
            {
                tbSouthPlayerTotalScore.Focus();
                return true;
            }
            #endregion
            #region TEXTBOX SOUTH TOTAL SCORE
            else if (tbSouthPlayerTotalScore.Focused)
            {
                tbWestPlayerTotalScore.Focus();
                return true;
            }
            #endregion
            #region TEXTBOX WEST TOTAL SCORE
            else if (tbWestPlayerTotalScore.Focused)
            {
                tbNorthPlayerTotalScore.Focus();
                return true;
            }
            #endregion
            #region TEXTBOX NORTH TOTAL SCORE
            else if (tbNorthPlayerTotalScore.Focused)
            {
                lblStub.Focus();
                return true;
            }
            #endregion
            else if (dgv.Visible && dgv.CurrentCell != null && dgv.CurrentCell.RowIndex >= 0)
            {
                #region DGV CELL WINNER
                if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_WINNER_ID)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[VHand.COLUMN_PLAYER_LOOSER_ID];
                    dgv.BeginEdit(true);
                    return true;
                }
                #endregion
                #region DGV CELL LOOSER
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_LOOSER_ID)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[VHand.COLUMN_HAND_SCORE];
                    dgv.BeginEdit(true);
                    return true;
                }
                #endregion
                #region DGV CELL HAND SCORE
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_HAND_SCORE)
                {
                    FocusNextRowWinnerCellOrStub();
                    return true;
                }
                #endregion
                #region DGV CELL EAST PENALTY
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_EAST_PENALTY)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[VHand.COLUMN_PLAYER_SOUTH_PENALTY];
                    dgv.BeginEdit(true);
                    return true;
                }
                #endregion
                #region DGV CELL IS CHICKEN HAND
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_IS_CHICKEN_HAND)
                {
                    FocusNextRowWinnerCellOrStub();
                    return true;
                }
                #endregion
                #region DGV CELL SOUTH PENALTY
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_SOUTH_PENALTY)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[VHand.COLUMN_PLAYER_WEST_PENALTY];
                    dgv.BeginEdit(true);
                    return true;
                }
                #endregion
                #region DGV CELL WEST PENALTY
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_WEST_PENALTY)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[VHand.COLUMN_PLAYER_NORTH_PENALTY];
                    dgv.BeginEdit(true);
                    return true;
                }
                #endregion
                #region DGV CELL NORTH PENALTY
                else if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_NORTH_PENALTY)
                {
                    FocusNextRowWinnerCellOrStub();
                    return true;
                }
                #endregion
            }
            return false;
        }

        private void FocusNextRowWinnerCellOrStub()
        {
            if (dgv.CurrentCell.RowIndex == dgv.RowCount - 1)
                lblStub.Focus();
            else
            {
                dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex + 1].Cells[VHand.COLUMN_PLAYER_WINNER_ID];
                dgv.BeginEdit(true);
            }
        }

        private bool TabPressedBehaviour()
        {
            if (comboNorthPlayer.Focused)
            {
                if (cbUseTotalsOnly.Checked)
                    tbEastPlayerTotalScore.Focus();
                else if (dgv.Visible)
                {
                    dgv.Focus();
                    dgv.CurrentCell = dgv.Rows[0].Cells[VHand.COLUMN_PLAYER_WINNER_ID];
                }
                else
                    lblStub.Focus();

                return true;
            }
            else if (tbNorthPlayerTotalScore.Focused)
            {
                if (dgv.Visible)
                {
                    dgv.Focus();
                    dgv.CurrentCell = dgv.Rows[0].Cells[VHand.COLUMN_PLAYER_WINNER_ID];
                }
                else
                    lblStub.Focus();

                return true;
            }
            else
                return false;
        }

        private bool SpacePressedBehaviour()
        {
            if (dgv.Visible && dgv.CurrentCell != null && dgv.CurrentCell.RowIndex >= 0
                && dgv.CurrentCell.OwningColumn.Name.Equals(VHand.COLUMN_IS_CHICKEN_HAND))
            {
                CellIsChickenHandChanged(dgv.CurrentCell.RowIndex, dgv.CurrentCell.ColumnIndex);
                return true;
            }
            return false;
        }

        private bool SuprPressedBehaviour()
        {
            if (dgv.Visible && dgv.CurrentCell != null && dgv.CurrentCell.RowIndex >= 0)
            {
                #region DGV CELL WINNER
                if (dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_WINNER_ID ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_LOOSER_ID ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_HAND_SCORE ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_EAST_PENALTY ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_IS_CHICKEN_HAND ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_SOUTH_PENALTY ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_WEST_PENALTY ||
                    dgv.CurrentCell.OwningColumn.Name == VHand.COLUMN_PLAYER_NORTH_PENALTY)
                {
                    dgv.BeginEdit(true);
                    dgv.CurrentCell.Value = string.Empty;
                    EnterPressedBehaviour();
                    return true;
                }
                #endregion
            }
            return false;
        }            

        private void CellIsChickenHandChanged(int rowIndex, int colIndex)
        {
            ShowWaitCursor();
            DataGridViewCheckBoxCell checkCell = dgv.Rows[rowIndex].Cells[colIndex] as DataGridViewCheckBoxCell;            
            checkCell.Value = _controller.IsChickenHandChanged(GetSelectedHandId(rowIndex));
            dgv.BeginEdit(true);
            dgv.RefreshEdit();
            ShowDefaultCursor();
        }

        private int GetSelectedHandId(int rowIndex)
        {
            return (int)dgv.Rows[rowIndex].Cells[VHand.COLUMN_HAND_ID].Value;
        }

        private void SetEastPlayerHandScoreCell(int handId, string value)
        {
            dgv.Rows[handId - 1].Cells[DGVHand.COLUMN_PLAYER_EAST_SCORE].Value = value;
        }

        private void SetSouthPlayerHandScoreCell(int handId, string value)
        {
            dgv.Rows[handId - 1].Cells[DGVHand.COLUMN_PLAYER_SOUTH_SCORE].Value = value;
        }

        private void SetWestPlayerHandScoreCell(int handId, string value)
        {
            dgv.Rows[handId - 1].Cells[DGVHand.COLUMN_PLAYER_WEST_SCORE].Value = value;
        }

        private void SetNorthPlayerHandScoreCell(int handId, string value) 
        {
            dgv.Rows[handId - 1].Cells[DGVHand.COLUMN_PLAYER_NORTH_SCORE].Value = value;
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
