namespace MahjongTournamentSuite
{
    interface IHomePresenter
    {
        void LoadTournaments();

        void DeleteClicked(int tournamentId);

        void NameChanged(int tournamentId, string sCellValue);

        string GetTournamentName(int tournamentId);
    }
}
