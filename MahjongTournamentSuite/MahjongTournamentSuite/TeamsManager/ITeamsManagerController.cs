namespace MahjongTournamentSuite.TeamsManager
{
    interface ITeamsManagerController
    {
        void LoadForm(int tournamentId);

        void TeamNameChanged(int teamId, string newValue);
    }
}
