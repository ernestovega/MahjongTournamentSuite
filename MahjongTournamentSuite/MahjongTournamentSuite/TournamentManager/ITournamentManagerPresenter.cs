namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerPresenter
    {
        void LoadTournament(int tournamentId);

        void OnFormResized();
        
        void ShowRankingsClicked();

        void ButtonRoundClicked(int roundId);

        void ButtonTableClicked(int tableId);
        
        void ExportTournamentToExcelClicked();

        void ExportRankingsToHTMLClicked();
    }
}
