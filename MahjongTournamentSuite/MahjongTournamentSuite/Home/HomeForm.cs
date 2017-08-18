using MahjongTournamentSuite.Model;
using MahjongTournamentSuite.NewTournament;
using MahjongTournamentSuite.TournamentManager;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    public partial class HomeForm : Form, IHomeForm
    {
        #region Constants
        
        private static readonly string COLUMN_ID = "Id";
        private static readonly string COLUMN_DATE = "CreationDate";
        private static readonly string COLUMN_NAME = "Name";
        private static readonly string COLUMN_PLAYERS = "NumPlayers";
        private static readonly string COLUMN_ROUNDS = "NumRounds";
        private static readonly string COLUMN_IS_TEAMS = "IsTeams";
        private static readonly string COLUMN_IS_TEAMS_IMAGES = "Teams";
        private static readonly int COLUMN_NAME_INDEX = 1;

        #endregion

        #region Fields

        private IHomePresenter _presenter;

        #endregion

        #region Constructor

        public HomeForm()
        {
            InitializeComponent();
            _presenter = Injector.provideHomePresenter(this);
            _presenter.LoadTournaments();
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
            int tournamentId = GetCurrentTournamentId();
            if (tournamentId > -1)
            {
                new TournamentManagerForm(tournamentId).Show();
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _presenter.DeleteClicked();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            var mahjongTournamentTimer = new MahjongTournamentTimer.Program();
            Process.Start(mahjongTournamentTimer.returnExecutablePath());
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name.Equals(COLUMN_IS_TEAMS_IMAGES))
            {
                if ((bool)((DataGridViewCheckBoxCell)dataGridView.Rows[e.RowIndex].Cells[COLUMN_IS_TEAMS]).Value)
                    e.Value = Properties.Resources.yes;
                else
                    e.Value = Properties.Resources.no;
            }
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == COLUMN_NAME_INDEX)
                dataGridView.BeginEdit(true);
        }
        
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == COLUMN_NAME_INDEX)
            {
                int tournamentId = GetSelectedTournamentId(e.RowIndex);
                string previousValue = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string newValue = ((string)e.FormattedValue).Trim();
                if (newValue.Length > 0 && !newValue.Equals(previousValue))
                    _presenter.NameChanged(tournamentId, newValue);
                else
                    dataGridView.CancelEdit();
            }
        }

        #endregion

        #region IHomeForm implementation

        public void FillDataGridTournaments(List<DBTournament> tournaments)
        {
            SortableBindingList<DBTournament> sortableTournaments = 
                new SortableBindingList<DBTournament>(tournaments);
            dataGridView.DataSource = sortableTournaments;

            //IsTeams images column creation
            if (!dataGridView.Columns.Contains(COLUMN_IS_TEAMS_IMAGES))
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.Name = COLUMN_IS_TEAMS_IMAGES;
                dataGridView.Columns.Add(imgColumn);
            }
            //Visible
            dataGridView.Columns[COLUMN_ID].Visible = false;
            dataGridView.Columns[COLUMN_IS_TEAMS].Visible = false;
            //ReadOnly
            dataGridView.Columns[COLUMN_DATE].ReadOnly = true;
            dataGridView.Columns[COLUMN_PLAYERS].ReadOnly = true;
            dataGridView.Columns[COLUMN_ROUNDS].ReadOnly = true;
            dataGridView.Columns[COLUMN_IS_TEAMS_IMAGES].ReadOnly = true;
            //HeaderText
            dataGridView.Columns[COLUMN_DATE].HeaderText = "Creation date";
            dataGridView.Columns[COLUMN_NAME].HeaderText = "Tournament name";
            dataGridView.Columns[COLUMN_PLAYERS].HeaderText = "Players";
            dataGridView.Columns[COLUMN_ROUNDS].HeaderText = "Rounds";
            dataGridView.Columns[COLUMN_IS_TEAMS_IMAGES].HeaderText = "Teams";
            //AutoSizeMode
            dataGridView.Columns[COLUMN_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //SortMode
            foreach (DataGridViewColumn column in dataGridView.Columns)
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
            return MessageBox.Show(string.Format("{0} {1}", GetCurrentTournamentName(), "will be removed permanently"), "Delete Tournament", MessageBoxButtons.YesNo)
                            == DialogResult.Yes;
        }

        public void showLoading()
        {
            panelLoading.Visible = true;
        }

        public void hideLoading()
        {
            panelLoading.Visible = false;
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
            DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
            if (selectedRows != null && selectedRows.Count > 0)
            {
                return dataGridView.SelectedRows[0];
            }
            return null;
        }

        private int GetSelectedTournamentId(int rowIndex)
        {
            return (int)dataGridView.Rows[rowIndex].Cells[COLUMN_ID].Value;
        }

        #endregion
    }
}
