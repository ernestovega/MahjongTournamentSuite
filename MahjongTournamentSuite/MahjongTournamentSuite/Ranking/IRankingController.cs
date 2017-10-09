using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingController
    {
        void LoadData(Rankings rankings);

        void StopShowRankingThread();

        void IncrementShowingTime();

        void DecrementShowingTime();

        void IncrementShowingRows();

        void DecrementShowingRows();

        void PlayClicked();

        void PauseClicked();
    }
}
