namespace MahjongTournamentSuite.TeamSelector
{
    interface ITeamSelectorController
    {
        void LoadForm(int tournamentId);

        void FilterList(string text);
    }
}
