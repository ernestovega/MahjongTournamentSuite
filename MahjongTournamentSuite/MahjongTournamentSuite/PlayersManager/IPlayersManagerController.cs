namespace MahjongTournamentSuite.PlayersManager
{
    interface IPlayersManagerController
    {
        void LoadForm(int tournamentId);

        void PlayerNameChanged(int playerId, string newPlayerName);

        int SaveNewPlayerTeam(int playerId, string newTeamName);

        void CheckWrongPlayersTeams();

        void AssignNewEmaPlayer(int playerId, string playerEmaNumber);

        void UnassignEmaPlayer(int playerId);

        bool IsWrongPlayersTeams();
    }
}
