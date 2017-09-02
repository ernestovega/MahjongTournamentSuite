namespace MahjongTournamentSuite.Ranking
{
    interface IRankingPresenter
    {
        void LoadData(int tournamentId);

        void StartShowRankingThread();

        void StopShowRankingThread();

        void AbortShowRankingThreadIfAlive();
    }
}
