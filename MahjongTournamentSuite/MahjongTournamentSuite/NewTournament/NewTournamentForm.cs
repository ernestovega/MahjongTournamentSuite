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
            _presenter = Injector.provideNewTournamentPresenter(this);
        }

        #endregion

        #region Events

        private void btnReturn_Click(object sender, EventArgs e)
        {
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
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _presenter.RunWorkerCompleted();
        }

        #endregion

        #region INewTournamentForm implementation

        public bool isBackgroundWorkerBusy()
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

        public int getNumPlayers()
        {
            return decimal.ToInt32(numUpDownPlayers.Value);
        }

        public int getNumRounds()
        {
            return decimal.ToInt32(numUpDownRounds.Value);
        }

        public bool IsTeamsChecked()
        {
            return cbTeams.Checked;
        }

        public string getTournamentName()
        {
            return tbTournamentName.Text;
        }

        public int getNumTries()
        {
            return decimal.ToInt32(numUpDownTriesMax.Value);
        }

        public void EnableViews()
        {
            progressBar.Hide();
            progressBar.Visible = false;
            numUpDownPlayers.Enabled = true;
            numUpDownRounds.Enabled = true;
            numUpDownTriesMax.Enabled = true;
            cbTeams.Enabled = true;
            tbTournamentName.Enabled = true;
            btnStart.BackColor = greenEnabled;
            btnStart.FlatAppearance.MouseOverBackColor = greenEnabledHover;
            btnStart.Text = "Start";
            Cursor.Current = Cursors.Default;
        }

        public void DisableViews()
        {
            numUpDownPlayers.Enabled = false;
            numUpDownRounds.Enabled = false;
            numUpDownTriesMax.Enabled = false;
            cbTeams.Enabled = false;
            tbTournamentName.Enabled = false;
            btnStart.Text = "Stop";
            btnStart.BackColor = grayDisabled;
            btnStart.FlatAppearance.MouseOverBackColor = redDisabledHover;
            Cursor.Current = Cursors.WaitCursor;
            progressBar.Visible = true;
            progressBar.Show();
        }

        public void ResetProgressBar(int numTriesMax)
        {
            progressBar.Maximum = numTriesMax;
            progressBar.Value = 0;
        }

        public void ShowReachedTriesMessage(int numTriesMax)
        {
            MessageBox.Show(this, string.Format("Can't calculate tournament after {0} tries.\nIf you want to try again, select more tries.", numTriesMax));
        }

        public void ApplicationDoEvents()
        {
            Application.DoEvents();
        }

        #endregion

        #region Private

        #endregion
    }
}
