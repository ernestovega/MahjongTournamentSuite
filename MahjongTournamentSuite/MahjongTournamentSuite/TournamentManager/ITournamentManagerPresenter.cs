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

        void ButtonRoundTableClicked(int tableId);

        void TeamNameChanged(int tournamentId, int teamId, string newValue);

        void PlayerNameChanged(int tournamentId, int playerId, string newValue);
    }
}
