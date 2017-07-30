using MahjongTournamentSuite.NewTournament;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    public partial class HomeForm : Form, IHomeForm
    {
        #region CONSTANTS
        
        public static readonly string COLUMN_ID = "idDataGridViewTextBoxColumn";
        //public static readonly string COLUMN_DATE = "creationDateDataGridViewTextBoxColumn";
        public static readonly string COLUMN_NAME = "nameDataGridViewTextBoxColumn";
        //public static readonly string COLUMN_PLAYERS = "numPlayersDataGridViewTextBoxColumn";
        //public static readonly string COLUMN_ROUNDS = "numRoundsDataGridViewTextBoxColumn";

        #endregion

        #region Fields

        private IHomePresenter _presenter;

        #endregion

        #region Constructor

        public HomeForm()
        {
            InitializeComponent();
            _presenter = Injector.provideHomePresenter(this);
        }

        #endregion

        #region System events

        private void HomeForm_Load(object sender, EventArgs e)
        {
            ReloadDataGridTournaments();
        }

        #endregion

        #region Click events

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
            new NewTournamentForm().Show();
            Close();
        }

        public void btnResume_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnEditName_Click(object sender, EventArgs e)
        {
            _presenter.EditNameClicked();
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

        public void ReloadDataGridTournaments()
        {
            tournamentsTableAdapter.Fill(_MahjongTournamentSuite_Data_DBContext_TournamentSuiteDBDataSet.Tournaments);
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

        public int GetCurrentTournamentId()
        {
            DataGridViewRow row = GetCurrentSelectedRow();
            if (row != null)
            {
                return (int)row.Cells[COLUMN_ID].Value;
            }
            return -1;
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
