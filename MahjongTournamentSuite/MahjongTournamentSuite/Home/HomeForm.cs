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

        private static readonly Color greenEnabled = Color.FromArgb(0, 177, 106);
        private static readonly Color greenEnabledHover = Color.FromArgb(0, 127, 56);
        private static readonly Color grayDisabled = Color.FromArgb(65, 65, 65);

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

        #endregion

        #region IHomeForm implementation

        public void FillDataGridTournaments(List<DBTournament> tournaments)
        {
            dataGridTournaments.DataSource = tournaments;

            //DisplayIndex
            dataGridTournaments.Columns[COLUMN_DATE].DisplayIndex = 0;
            dataGridTournaments.Columns[COLUMN_NAME].DisplayIndex = 1;
            dataGridTournaments.Columns[COLUMN_PLAYERS].DisplayIndex = 2;
            dataGridTournaments.Columns[COLUMN_ROUNDS].DisplayIndex = 3;
            //Visible
            dataGridTournaments.Columns[COLUMN_ID].Visible = false;
            //ReadOnly
            dataGridTournaments.Columns[COLUMN_DATE].ReadOnly = true;
            dataGridTournaments.Columns[COLUMN_PLAYERS].ReadOnly = true;
            dataGridTournaments.Columns[COLUMN_ROUNDS].ReadOnly = true;
            //AutoSizeMode
            dataGridTournaments.Columns[COLUMN_DATE].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridTournaments.Columns[COLUMN_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridTournaments.Columns[COLUMN_PLAYERS].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridTournaments.Columns[COLUMN_ROUNDS].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //HeaderText
            dataGridTournaments.Columns[COLUMN_DATE].HeaderText = "Creation date";
            dataGridTournaments.Columns[COLUMN_NAME].HeaderText = "Tournament name";
            dataGridTournaments.Columns[COLUMN_PLAYERS].HeaderText = "Players";
            dataGridTournaments.Columns[COLUMN_ROUNDS].HeaderText = "Rounds";
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

        public void EnableResumeButton()
        {
            btnResume.Enabled = true;
        }

        public void DisableResumeButton()
        {
            btnResume.Enabled = false;
        }

        #endregion

        #region Private

        private DataGridViewRow GetCurrentSelectedRow()
        {
            DataGridViewSelectedRowCollection selectedRows = dataGridTournaments.SelectedRows;
            if (selectedRows != null && selectedRows.Count > 0)
            {
                return dataGridTournaments.SelectedRows[0];
            }
            return null;
        }

        #endregion
    }
}
