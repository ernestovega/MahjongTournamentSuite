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
        
        public static readonly string COLUMN_ID = "Id";
        public static readonly string COLUMN_DATE = "CreationDate";
        public static readonly string COLUMN_NAME = "Name";
        public static readonly string COLUMN_PLAYERS = "NumPlayers";
        public static readonly string COLUMN_ROUNDS = "NumRounds";
        public static readonly string COLUMN_IS_TEAMS = "IsTeams";
        private readonly int COLUMN_NAME_INDEX = 1;

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
            dataGridView.DataSource = tournaments;

            //DisplayIndex
            dataGridView.Columns[COLUMN_DATE].DisplayIndex = 0;
            dataGridView.Columns[COLUMN_NAME].DisplayIndex = 1;
            dataGridView.Columns[COLUMN_PLAYERS].DisplayIndex = 2;
            dataGridView.Columns[COLUMN_ROUNDS].DisplayIndex = 3;
            dataGridView.Columns[COLUMN_IS_TEAMS].DisplayIndex = 4;
            //Visible
            dataGridView.Columns[COLUMN_ID].Visible = false;
            //ReadOnly
            dataGridView.Columns[COLUMN_DATE].ReadOnly = true;
            dataGridView.Columns[COLUMN_PLAYERS].ReadOnly = true;
            dataGridView.Columns[COLUMN_ROUNDS].ReadOnly = true;
            dataGridView.Columns[COLUMN_IS_TEAMS].ReadOnly = true;
            //AutoSizeMode
            dataGridView.Columns[COLUMN_DATE].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[COLUMN_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[COLUMN_PLAYERS].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[COLUMN_ROUNDS].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[COLUMN_IS_TEAMS].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //HeaderText
            dataGridView.Columns[COLUMN_DATE].HeaderText = "Creation date";
            dataGridView.Columns[COLUMN_NAME].HeaderText = "Tournament name";
            dataGridView.Columns[COLUMN_PLAYERS].HeaderText = "Players";
            dataGridView.Columns[COLUMN_ROUNDS].HeaderText = "Rounds";
            dataGridView.Columns[COLUMN_IS_TEAMS].HeaderText = "Teams";
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
