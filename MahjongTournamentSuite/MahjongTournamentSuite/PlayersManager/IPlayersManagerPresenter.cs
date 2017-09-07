namespace MahjongTournamentSuite.PlayersManager
{
    interface IPlayersManagerPresenter
    {
        void LoadForm(int tournamentId);

        void PlayerNameChanged(int playerId, string newPlayerName);

        int SaveNewPlayerTeam(int playerId, string newTeamName);

        void PlayerTeamChanged();

        int SaveNewPlayerCountry(int playerId, string newCountryName);
    }
}
