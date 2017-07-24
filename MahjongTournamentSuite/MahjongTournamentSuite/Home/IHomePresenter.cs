namespace MahjongTournamentSuite
{
    interface IHomePresenter
    {
        void LoadOldTournamentsList();

        void TimerClicked();

        void NewClicked();

        void ResumeClicked(int tournamentId);
    }
}
