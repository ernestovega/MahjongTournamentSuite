namespace MahjongTournamentSuite
{
    interface IHomePresenter
    {
        void LoadTournaments();

        void DeleteClicked();

        void NameChanged(int tournamentId, string sCellValue);

        string GetTournamentName(int tournamentId);
    }
}
