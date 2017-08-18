using MahjongTournamentSuite.Home;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuite.NewTournament
{
    public partial class NewTournamentForm : Form, INewTournamentForm
    {
        #region Constants

        internal static readonly string TOURNAMENT_NAME_TEMPLATE_TEXT = "Enter here Tournament name";

        private static readonly Color greenEnabled = Color.FromArgb(0, 177, 106);
        private static readonly Color greenEnabledHover = Color.FromArgb(0, 127, 56);
        private static readonly Color grayDisabled = Color.FromArgb(65, 65, 65);
        private static readonly Color redDisabledHover = Color.FromArgb(224, 0, 0);

        #endregion

        #region Fields

        private INewTournamentPresenter _presenter;

        #endregion

        #region Constructor

        public NewTournamentForm()
        {
            InitializeComponent();
            tbTournamentName.Text = TOURNAMENT_NAME_TEMPLATE_TEXT;
            _presenter = Injector.provideNewTournamentPresenter(this);
        }

        #endregion

        #region Events

        private void btnReturn_Click(object sender, EventArgs e)
        {
            new HomeForm().Show();
            Close();
        }

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

        private void btnStart_Click(object sender, EventArgs e)
        {
            _presenter.StartClicked();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _presenter.BackgroundWorkerDoWork(sender as BackgroundWorker, e);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetTriesCounterLabel(e.ProgressPercentage);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _presenter.RunWorkerCompleted();
        }

        #endregion

        #region INewTournamentForm implementation

        public bool IsBackgroundWorkerBusy()
        {
            return backgroundWorker.IsBusy;
        }

        public void RunBackgroundWorker()
        {
            backgroundWorker.RunWorkerAsync();
        }

        public void CancelBackgroundWorker()
        {
            backgroundWorker.CancelAsync();
        }

        public int GetNumPlayers()
        {
            return decimal.ToInt32(numUpDownPlayers.Value);
        }

        public void SetNumUpDownPlayers(int numPlayers)
        {
            numUpDownPlayers.Value = numPlayers;
        }

        public int GetNumRounds()
        {
            return decimal.ToInt32(numUpDownRounds.Value);
        }

        public bool IsTeamsChecked()
        {
            return cbTeams.Checked;
        }

        public string GetTournamentName()
        {
            return tbTournamentName.Text;
        }

        public int GetNumTries()
        {
            return decimal.ToInt32(numUpDownTriesMax.Value);
        }

        public void EnableViews()
        {
            btnStart.BackColor = greenEnabled;
            btnStart.FlatAppearance.MouseOverBackColor = greenEnabledHover;
            btnStart.Text = "Start";
            panelLoading.Visible = false;
            panelOptions.Visible = true;
            Cursor.Current = Cursors.Default;
        }

        public void DisableViews()
        {
            btnStart.Text = "Stop";
            btnStart.BackColor = grayDisabled;
            btnStart.FlatAppearance.MouseOverBackColor = redDisabledHover;
            panelOptions.Visible = false;
            panelLoading.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void ShowReachedTriesMessage(int numTriesMax)
        {
            MessageBox.Show(this, string.Format("Can't calculate tournament after {0} tries.\nIf you want to try again, select more tries.", numTriesMax));
        }

        public void ShowEnterTournamentNameMessage()
        {
            MessageBox.Show(this, "Please enter a name for the tournament.");
        }

        public bool ShowWrongPlayersNumberMessage(int wrongNumPlayers, int goodNumPlayers)
        {
            DialogResult dialogResult = MessageBox.Show(this, 
                string.Format("{0} is not multiple of 4.\nDo you want to change it to {1}?", wrongNumPlayers, goodNumPlayers),
                "Wrong number of players", MessageBoxButtons.OKCancel);
            return dialogResult == DialogResult.OK;
        }

        public void ShowSomethingWentWrongMessage()
        {
            MessageBox.Show(this, "Ups! Something went wrong. Please try again.");
        }

        public void ApplicationDoEvents()
        {
            Application.DoEvents();
        }

        public void SetTriesCounterLabel(int tries)
        {
            if (tries == 0)
                lblLoadingMessage.Text = "Saving data...";
            else
                lblLoadingMessage.Text = string.Format("Tries: {0}", tries);
        }

        public void CloseForm()
        {
            Close();
        }

        #endregion
    }
}
