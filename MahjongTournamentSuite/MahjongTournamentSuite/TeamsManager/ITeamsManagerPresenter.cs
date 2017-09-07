namespace MahjongTournamentSuite.TeamsManager
{
    interface ITeamsManagerPresenter
    {
        void LoadForm(int tournamentId);

        void TeamNameChanged(int teamId, string newValue);
    }
}
