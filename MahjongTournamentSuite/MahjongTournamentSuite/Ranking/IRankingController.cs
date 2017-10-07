using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingController
    {
        void LoadData(Rankings rankings);

        void StopShowRankingThread();

        void IncrementShowingTimeInOneSecond();

        void DecrementShowingTimeInOneSecond();

        void PlayClicked();

        void PauseClicked();
    }
}
