namespace MahjongTournamentSuite.TableManager
{
    interface ITableManagerPresenter
    {
        void LoadTable(int tournamentId, int roundId, int tableId);

        void NameEastPlayerChanged(string selectedValue);

        void NameSouthPlayerChanged(string selectedValue);

        void NameWestPlayerChanged(string selectedValue);

        void NameNorthPlayerChanged(string selectedValue);
    }
}
