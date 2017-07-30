using System.ComponentModel;

namespace MahjongTournamentSuite.NewTournament
{
    interface INewTournamentPresenter
    {
        void StartClicked();

        void BackgroundWorkerDoWork(BackgroundWorker backgroundWorker, DoWorkEventArgs e);

        void RunWorkerCompleted();
    }
}
