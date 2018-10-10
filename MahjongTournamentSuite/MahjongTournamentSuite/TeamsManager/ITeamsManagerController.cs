namespace MahjongTournamentSuite.TeamsManager
{
    interface ITeamsManagerController
    {
        void LoadForm(int tournamentId);

        void TeamNameChanged(int teamId, string newValue); 

        void LoadTeamPlayers(int teamId);

        void AssignTeamPlayer(int _tournamentId, int returnedItem, int teamId);

        void UnassignTeamPlayer(int _tournamentId, int teamPlayerId);
    }
}
