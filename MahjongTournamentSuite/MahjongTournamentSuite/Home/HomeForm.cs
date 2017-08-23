using MahjongTournamentSuite.Model;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.TournamentManager;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Cursor = Cursors.WaitCursor;
            InitializeComponent();
            _presenter = Injector.provideHomePresenter(this);
            _presenter.LoadTournaments();
            Cursor = Cursors.Default;
        }

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

        public void btnNew_Click(object sender, EventArgs e)
        {
            NewTournamentForm newTournamentForm = new NewTournamentForm();
            newTournamentForm.FormClosed += new FormClosedEventHandler(NewTournamentForm_FormClosed);
            newTournamentForm.ShowDialog();
        }

        void NewTournamentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _presenter.LoadTournaments();
        }

        public void btnResume_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int tournamentId = GetCurrentTournamentId();
            if (tournamentId > -1)
            {
                new TournamentManagerForm(tournamentId).Show();
                Close();
            }
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
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
        }

        private void dgvTournaments_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            /* BUG FIX: Cuando arrancamos la aplicación y no hay campeonatos, y al crear uno se muestra 
             * una segunda fila vacía. No entiendo por qué es pero con esto parace que se oculta sin efectos colaterales. */
            if (dgvTournaments.RowCount > _numTournaments)
                dgvTournaments.Rows[dgvTournaments.RowCount - 1].Visible = false;
        }

        private void dgvTournaments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTournaments.Columns[e.ColumnIndex].Name.Equals(COLUMN_IS_TEAMS_IMAGES))
            {
                DataGridViewCheckBoxCell cellIsTeams = 
                    (DataGridViewCheckBoxCell)dgvTournaments.Rows[e.RowIndex].Cells[COLUMN_IS_TEAMS];
                if (cellIsTeams.Value != null)
                {
                    if ((bool)cellIsTeams.Value)
                    {
                        if (dgvTournaments.CurrentCell != null && dgvTournaments.CurrentCell.RowIndex == e.RowIndex)
                            e.Value = Properties.Resources.yes_white;
                        else
                            e.Value = Properties.Resources.yes;
                    }
                    else
                    {
                        if (dgvTournaments.CurrentCell != null && dgvTournaments.CurrentCell.RowIndex == e.RowIndex)
                            e.Value = Properties.Resources.no_white;
                        else
                            e.Value = Properties.Resources.no;
                    }

                    dgvTournaments.NotifyCurrentCellDirty(true);
                }
            }
        }

        private void dgvTournaments_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTournaments.Columns[e.ColumnIndex].Name.Equals(COLUMN_NAME))
                dgvTournaments.BeginEdit(true);
        }

        private void dgvTournaments_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTournaments.Columns[e.ColumnIndex].Name.Equals(COLUMN_NAME))
            {
                int tournamentId = GetSelectedTournamentId(e.RowIndex);
                string previousValue = (string)dgvTournaments.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    _presenter.NameChanged(tournamentId, newValue);
                else
                    dgvTournaments.CancelEdit();
            }
        }

        #endregion

        #region IHomeForm implementation

        public void FillDataGridTournaments(List<DBTournament> tournaments)
        {
            _numTournaments = tournaments.Count;
            SortableBindingList<DBTournament> sortableTournaments = 
                new SortableBindingList<DBTournament>(tournaments);
            dgvTournaments.DataSource = sortableTournaments;

            //IsTeams images column creation
            if (!dgvTournaments.Columns.Contains(COLUMN_IS_TEAMS_IMAGES))
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.Name = COLUMN_IS_TEAMS_IMAGES;
                dgvTournaments.Columns.Add(imgColumn);
            }
            //Visible
            dgvTournaments.Columns[COLUMN_ID].Visible = false;
            dgvTournaments.Columns[COLUMN_IS_TEAMS].Visible = false;
            //ReadOnly
            dgvTournaments.Columns[COLUMN_DATE].ReadOnly = true;
            dgvTournaments.Columns[COLUMN_PLAYERS].ReadOnly = true;
            dgvTournaments.Columns[COLUMN_ROUNDS].ReadOnly = true;
            dgvTournaments.Columns[COLUMN_IS_TEAMS_IMAGES].ReadOnly = true;
            //HeaderText
            dgvTournaments.Columns[COLUMN_DATE].HeaderText = "Creation date";
            dgvTournaments.Columns[COLUMN_NAME].HeaderText = "Tournament name";
            dgvTournaments.Columns[COLUMN_PLAYERS].HeaderText = "Players";
            dgvTournaments.Columns[COLUMN_ROUNDS].HeaderText = "Rounds";
            dgvTournaments.Columns[COLUMN_IS_TEAMS_IMAGES].HeaderText = "Teams";
            //AutoSizeMode
            dgvTournaments.Columns[COLUMN_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //SortMode
            foreach (DataGridViewColumn column in dgvTournaments.Columns)
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
            DataGridViewSelectedRowCollection selectedRows = dgvTournaments.SelectedRows;
            if (selectedRows != null && selectedRows.Count > 0)
            {
                return dgvTournaments.SelectedRows[0];
            }
            return null;
        }

        private int GetSelectedTournamentId(int rowIndex)
        {
            return (int)dgvTournaments.Rows[rowIndex].Cells[COLUMN_ID].Value;
        }

        #endregion
    }
}
