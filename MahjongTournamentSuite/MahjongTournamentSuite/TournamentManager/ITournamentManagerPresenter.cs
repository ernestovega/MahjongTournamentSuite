namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerPresenter
    {
        void LoadTournament(int tournamentId);

        void OnFormResized();

        void ButtonTeamsClicked();

        void ButtonPlayersClicked();

        void ButtonRoundsClicked();

        void ButtonRoundClicked(int roundId);
    }
}
