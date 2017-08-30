using MahjongTournamentSuitePresentationLayer.Home;
using MahjongTournamentSuitePresentationLayer.Resources;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentSuitePresentationLayer.NewTournament
{
    public partial class NewTournamentForm : Form, INewTournamentForm
    {
        #region Constants

        internal static readonly string TOURNAMENT_NAME_TEMPLATE_TEXT = "Tournament name";

        #endregion

        #region Fields

        private INewTournamentPresenter _presenter;

        #endregion

        #region Constructor

        public NewTournamentForm()
        {
            InitializeComponent();
            Cursor = Cursors.WaitCursor;
            tbTournamentName.Text = TOURNAMENT_NAME_TEMPLATE_TEXT;
            _presenter = Injector.provideNewTournamentPresenter(this);
            ActiveControl = tbTournamentName;
            tbTournamentName.SelectAll();
            Cursor = Cursors.Default;
        }

        #endregion

        #region Events

        private void btnStart_Click(object sender, EventArgs e)
        {
            _presenter.StartClicked(tbTournamentName.Text);
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
            _presenter.RunWorkerCompleted(e.Cancelled);
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

        public int GetNumTries()
        {
            return decimal.ToInt32(numUpDownTriesMax.Value);
        }

        public void EnableViews()
        {
            btnStart.BackColor = CustomColors.GREEN_MM;
            btnStart.FlatAppearance.MouseOverBackColor = CustomColors.GREEN_MM_DARKER;
            btnStart.Text = "Start";
            panelLoading.Visible = false;
            panelOptions.Visible = true;
            btnStart.Visible = false;
            Cursor = Cursors.Default;
        }

        public void DisableViews()
        {
            btnStart.Text = "Stop";
            btnStart.BackColor = CustomColors.GRAY_DISABLED;
            btnStart.FlatAppearance.MouseOverBackColor = CustomColors.RED_CANCEL;
            panelOptions.Visible = false;
            panelLoading.Visible = true;
            Cursor = Cursors.WaitCursor;
        }

        public void ShowEnterTournamentNameMessage()
        {
            MessageBox.Show(this, "Please enter a name for the tournament.");
        }

        public void ShowMessageExistingTournamentName()
        {
            MessageBox.Show(this, "There is another Tournament with the same name yet.");
        }

        public bool ShowWrongPlayersNumberMessage(int wrongNumPlayers, 
            int goodNumPlayers)
        {
            DialogResult dialogResult = MessageBox.Show(this, 
                string.Format("{0} is not multiple of 4.\nDo you want to change it to {1}?", wrongNumPlayers, goodNumPlayers),
                "Wrong number of players", MessageBoxButtons.OKCancel);
            return dialogResult == DialogResult.OK;
        }

        public void ShowReachedTriesMessage(int numTriesMax)
        {
            MessageBox.Show(this, string.Format("Can't calculate tournament after {0} tries.\nIf you want to try again, select more tries.", numTriesMax));
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
            {
                btnStart.Visible = false;
                lblLoadingMessage.Text = "Saving data...";
            }
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
