namespace MahjongTournamentSuite
{
    interface IHomeController
    {
        void LoadTournaments();

        void DeleteClicked(int tournamentId);

        void NameChanged(int tournamentId, string sCellValue);

        string GetTournamentName(int tournamentId);
    }
}
