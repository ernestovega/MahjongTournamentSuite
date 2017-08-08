namespace MahjongTournamentSuite.TableManager
{
    interface ITableManagerPresenter
    {
        void LoadTable(int tournamentId, int roundId, int tableId);

        void NameEastPlayerChanged(string selectedValue);

        void NameSouthPlayerChanged(string selectedValue);

        void NameWestPlayerChanged(string selectedValue);

        void NameNorthPlayerChanged(string selectedValue);

        void playerWinnerIdChanged(int handId, int newPlayerWinnerId);

        void playerLooserIdChanged(int handId, int cellValue);

        void PointsChanged(int handId, int cellValue);

        void IsChickenHandChanged(int handId, bool cellValue);
    }
}
