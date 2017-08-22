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

        void TeamNameChanged(int teamId, string newValue);

        void PlayerNameChanged(int playerId, string newPlayerName);

        string PlayerTeamChanged(int playerId, string newTeamName);

        string PlayerCountryChanged(int playerId, string newCountryName);
    }
}
