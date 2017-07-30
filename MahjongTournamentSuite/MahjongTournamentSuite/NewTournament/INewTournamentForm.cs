using System.ComponentModel;

namespace MahjongTournamentSuite.NewTournament
{
    interface INewTournamentForm
    {
        bool isBackgroundWorkerBusy();

        void RunBackgroundWorker();

        void CancelBackgroundWorker();

        int getNumPlayers();

        int getNumRounds();

        bool IsTeamsChecked();

        string getTournamentName();

        int getNumTries();

        void EnableViews();

        void DisableViews();

        void ResetProgressBar(int numTriesMax);

        void ShowReachedTriesMessage(int numTriesMax);

        void ApplicationDoEvents();
    }
}
