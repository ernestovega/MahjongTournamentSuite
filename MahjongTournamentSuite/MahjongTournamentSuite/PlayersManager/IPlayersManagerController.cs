using MahjongTournamentSuite._Data.DataModel;

namespace MahjongTournamentSuite.PlayersManager
{
    interface IPlayersManagerController
    {
        void LoadForm(int tournamentId);

        void PlayerNameChanged(int playerId, string newPlayerName);

        int SaveNewPlayerTeam(int playerId, string newTeamName);

        void CheckWrongPlayersTeams();

        VEmaPlayer AssignNewEmaPlayer(int playerId, string playerEmaNumber);

        bool IsWrongPlayersTeams();
    }
}
