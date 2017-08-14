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

        void SetTriesCounterLabel(int tries);

        void ShowEnterTournamentNameMessage();

        void ShowReachedTriesMessage(int numTriesMax);

        void ShowSomethingWentWrongMessage();

        void ApplicationDoEvents();

        void OpenTournamentManagerForm(int tournamentId);

        void CloseForm();
    }
}
