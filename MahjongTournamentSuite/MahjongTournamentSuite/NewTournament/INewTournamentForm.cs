namespace MahjongTournamentSuite.NewTournament
{
    interface INewTournamentForm
    {
        bool IsBackgroundWorkerBusy();

        void RunBackgroundWorker();

        void CancelBackgroundWorker();

        int GetNumPlayers();

        void SetNumUpDownPlayers(int numPlayers);

        int GetNumRounds();

        bool IsTeamsChecked();

        string GetTournamentName();

        int GetNumTries();

        void EnableViews();

        void DisableViews();

        void SetTriesCounterLabel(int tries);

        void ShowEnterTournamentNameMessage();

        bool ShowWrongPlayersNumberMessage(int wrongNumPlayers, int goodNumPlayers);

        void ShowReachedTriesMessage(int numTriesMax);

        void ShowSomethingWentWrongMessage();

        void ApplicationDoEvents();

        void CloseForm();
    }
}
