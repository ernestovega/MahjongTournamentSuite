﻿namespace MahjongTournamentSuite.NewTournament
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

        int GetNumTries();

        void EnableViews();

        void DisableViews();

        void SetTriesCounterLabel(int tries);

        void ShowEnterTournamentNameMessage();

        void ShowMessageExistingTournamentName();

        bool ShowWrongPlayersNumberMessage(int wrongNumPlayers, int goodNumPlayers);

        void ShowReachedTriesMessage(int numTriesMax);

        void ShowSomethingWentWrongMessage();

        void ApplicationDoEvents();

        void CloseFormReturningValue(int tournamentId);
    }
}
