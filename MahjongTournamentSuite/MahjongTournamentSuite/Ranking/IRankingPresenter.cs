using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingPresenter
    {
        void LoadData(Rankings rankings);

        void StartShowRankingThread();

        void StopShowRankingThread();

        void AbortShowRankingThreadIfAlive();

        void IncrementShowingTimeInOneSecond();

        void DecrementShowingTimeInOneSecond();
    }
}
