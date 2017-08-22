using System.ComponentModel;

namespace MahjongTournamentSuite.NewTournament
{
    interface INewTournamentPresenter
    {
        void StartClicked(string tournamentName);

        void BackgroundWorkerDoWork(BackgroundWorker backgroundWorker, DoWorkEventArgs e);

        void RunWorkerCompleted(bool isCancelled);
    }
}
