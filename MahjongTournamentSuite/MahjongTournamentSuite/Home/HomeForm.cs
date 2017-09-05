using MahjongTournamentSuite.ManageCountries;
using MahjongTournamentSuite.Model;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.Resources;
using MahjongTournamentSuite.TournamentManager;
using MahjongTournamentSuiteDataLayer.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    public partial class HomeForm : Form, IHomeForm
    {
        #region Constants
        
        private static readonly string COLUMN_ID = "TournamentId";
        private static readonly string COLUMN_DATE = "CreationDate";
        private static readonly string COLUMN_NAME = "TournamentName";
        private static readonly string COLUMN_PLAYERS = "NumPlayers";
        private static readonly string COLUMN_ROUNDS = "NumRounds";
        private static readonly string COLUMN_IS_TEAMS = "IsTeams";
        private static readonly string COLUMN_IS_TEAMS_IMAGES = "Teams";

        #endregion

        #region Fields

        private IHomePresenter _presenter;
        private int _numTournaments = 0;

        #endregion

        #region Constructor

        public HomeForm()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Events

        private void HomeForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CenterMainButtons();
            _presenter = Injector.provideHomePresenter(this);
            _presenter.LoadTournaments();
            Cursor = Cursors.Default;
        }

        private void HomeForm_Resize(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (_presenter != null)
                _presenter.OnFormResized();
            Cursor = Cursors.Default;

        }

        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }

        private void imgLogoMM_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.mahjongmadrid.com/");
            Process.Start(sInfo);
            Cursor = Cursors.Default;
        }

        private void imgLogoEMA_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ProcessStartInfo sInfo = new ProcessStartInfo("http://mahjong-europe.org/portal/");
            Process.Start(sInfo);
            Cursor = Cursors.Default;
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            new ManageCountriesForm().ShowDialog();
            Cursor = Cursors.Default;
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            NewTournamentForm newTournamentForm = new NewTournamentForm();
            newTournamentForm.FormClosed += new FormClosedEventHandler(NewTournamentForm_FormClosed);
            newTournamentForm.ShowDialog();
            Cursor = Cursors.Default;
        }

        void NewTournamentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _presenter.LoadTournaments();
        }

        public void btnResume_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int tournamentId = GetCurrentTournamentId();
            new TournamentManagerForm(tournamentId).Show();
            Close();
            Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int tournamentId = GetCurrentTournamentId();
            _presenter.DeleteClicked(tournamentId);
            Cursor = Cursors.Default;
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
            Cursor = Cursors.Default;
        }

        private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            /* BUG FIX: Cuando arrancamos la aplicación y no hay campeonatos, al crear uno, se muestra una
             * segunda fila vacía. No entiendo por qué es, pero con esto parace que se oculta sin efectos colaterales. */
            if (dgv.RowCount > _numTournaments)
                dgv.Rows[dgv.RowCount - 1].Visible = false;
        }
        
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.CurrentCell != null &&
                dgv.CurrentCell.RowIndex == e.RowIndex &&
                dgv.CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                e.CellStyle.SelectionBackColor =
                    dgv.CurrentCell.ReadOnly ?
                    Resources.MyConstants.GREEN_MM_DARKEST :
                    Resources.MyConstants.GREEN_MM_DARKER;
            }
            if (dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_IS_TEAMS_IMAGES))
            {
                DataGridViewCheckBoxCell cellIsTeams = 
                    (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[COLUMN_IS_TEAMS];
                if (cellIsTeams.Value != null)
                {
                    if ((bool)cellIsTeams.Value)
                    {
                        if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex == e.RowIndex)
                            e.Value = Properties.Resources.yes_white;
                        else
                            e.Value = Properties.Resources.yes;
                    }
                    else
                    {
                        if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex == e.RowIndex)
                            e.Value = Properties.Resources.no_white;
                        else
                            e.Value = Properties.Resources.no;
                    }

                    dgv.NotifyCurrentCellDirty(true);
                }
            }
        }

        private void dgvTournaments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_NAME))
                dgv.BeginEdit(true);
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex >= 0)
                btnResume_Click(null, null);
        }

        private void dgvTournaments_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv.Columns[e.ColumnIndex].Name.Equals(COLUMN_NAME))
            {
                int tournamentId = GetSelectedTournamentId(e.RowIndex);
                string previousValue = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    _presenter.NameChanged(tournamentId, newValue);
                else
                    dgv.CancelEdit();
            }
        }

        #endregion

        #region IHomeForm implementation

        public void CenterMainButtons()
        {
            panelMainButtons.Location =
                new Point((Width - panelMainButtons.Width) / 2, panelMainButtons.Location.Y);
        }

        public void FillDGVTournaments(List<DBTournament> tournaments)
        {
            _numTournaments = tournaments.Count;
            SortableBindingList<DBTournament> sortableTournaments = 
                new SortableBindingList<DBTournament>(tournaments);
            dgv.DataSource = sortableTournaments;

            //IsTeams images column creation
            if (!dgv.Columns.Contains(COLUMN_IS_TEAMS_IMAGES))
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.Name = COLUMN_IS_TEAMS_IMAGES;
                dgv.Columns.Add(imgColumn);
            }
            //Visible
            dgv.Columns[COLUMN_ID].Visible = false;
            dgv.Columns[COLUMN_IS_TEAMS].Visible = false;
            //ReadOnly
            dgv.Columns[COLUMN_DATE].ReadOnly = true;
            dgv.Columns[COLUMN_PLAYERS].ReadOnly = true;
            dgv.Columns[COLUMN_ROUNDS].ReadOnly = true;
            dgv.Columns[COLUMN_IS_TEAMS_IMAGES].ReadOnly = true;
            //Readonly Columns BackColor
            dgv.Columns[COLUMN_DATE].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_PLAYERS].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_ROUNDS].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgv.Columns[COLUMN_IS_TEAMS_IMAGES].DefaultCellStyle.BackColor = SystemColors.ControlLight;
            //Readonly Columns ForeColor
            dgv.Columns[COLUMN_DATE].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_PLAYERS].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_ROUNDS].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.Columns[COLUMN_IS_TEAMS_IMAGES].DefaultCellStyle.ForeColor = SystemColors.GrayText;
            //HeaderText
            dgv.Columns[COLUMN_DATE].HeaderText = "Creation date";
            dgv.Columns[COLUMN_NAME].HeaderText = "Tournament name";
            dgv.Columns[COLUMN_PLAYERS].HeaderText = "Players";
            dgv.Columns[COLUMN_ROUNDS].HeaderText = "Rounds";
            dgv.Columns[COLUMN_IS_TEAMS_IMAGES].HeaderText = "Teams";
            //AutoSizeMode
            dgv.Columns[COLUMN_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //SortMode
            foreach (DataGridViewColumn column in dgv.Columns)
                column.SortMode = DataGridViewColumnSortMode.Automatic;
        }

        public int GetCurrentTournamentId()
        {
            DataGridViewRow row = GetCurrentSelectedRow();
            if (row != null)
                return (int)row.Cells[COLUMN_ID].Value;
            else
                return -1;
        }

        public string GetCurrentTournamentName()
        {
            DataGridViewRow row = GetCurrentSelectedRow();
            if (row != null)
            {
                return (string)row.Cells[COLUMN_NAME].Value;
            }
            return "";
        }

        public string RequestNewTournamentName()
        {
            return Interaction.InputBox("Change the name of the tournament", "Edit tournament name", GetCurrentTournamentName());
        }

        public bool RequestDeleteTournamentConfirmation()
        {
            return MessageBox.Show(string.Format("\"{0}\" {1}", GetCurrentTournamentName(), "will be removed permanently"), "Delete Tournament", MessageBoxButtons.YesNo)
                            == DialogResult.Yes;
        }

        public void EnableResumeAndDeleteButton()
        {
            btnResume.Enabled = true;
            btnDelete.Enabled = true;
        }

        public void DisableResumeAndDeleteButton()
        {
            btnResume.Enabled = false;
            btnDelete.Enabled = false;
        }

        #endregion

        #region Private

        private DataGridViewRow GetCurrentSelectedRow()
        {
            DataGridViewSelectedRowCollection selectedRows = dgv.SelectedRows;
            if (selectedRows != null && selectedRows.Count > 0)
            {
                return dgv.SelectedRows[0];
            }
            return null;
        }

        private int GetSelectedTournamentId(int rowIndex)
        {
            return (int)dgv.Rows[rowIndex].Cells[COLUMN_ID].Value;
        }

        #endregion
    }
}
