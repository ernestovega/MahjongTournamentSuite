namespace MahjongTournamentSuite.Ranking
{
    interface IRankingPresenter
    {
        void LoadDataAndStartShowRankingThread(int tournamentId);

        void StopShowRankingThread();
    }
}
