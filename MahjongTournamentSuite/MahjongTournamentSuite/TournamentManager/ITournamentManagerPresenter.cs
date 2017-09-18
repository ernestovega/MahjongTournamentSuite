namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerPresenter
    {
        void LoadTournament(int tournamentId);

        void OnFormResized();
        
        void ShowRankingsClicked();

        void ButtonTeamsClicked();

        void TeamsManagerFormClosed();

        void ButtonPlayersClicked();

        void PlayersManagerFormClosed(bool isWrongTeams);

        void ButtonRoundClicked(int roundId);

        void ButtonTableClicked(int tableId);

        void ExportRankingsToHTMLClicked();

        bool IsRoundCompleted(bool roundId);

        bool IsTableCompleted(int tableId);
    }
}
