using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingPresenter
    {
        void LoadData(Rankings rankings);

        void StopShowRankingThread();

        void IncrementShowingTimeInOneSecond();

        void DecrementShowingTimeInOneSecond();

        void PlayClicked();

        void PauseClicked();
    }
}
