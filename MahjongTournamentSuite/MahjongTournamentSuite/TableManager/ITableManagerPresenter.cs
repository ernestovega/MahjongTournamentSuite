using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TableManager
{
    interface ITableManagerPresenter
    {
        void LoadForm(int tournamentId, int roundId, int tableId);

        void NameEastPlayerChanged(int selectedPlayerId);

        void NameSouthPlayerChanged(int selectedPlayerId);

        void NameWestPlayerChanged(int selectedPlayerId);

        void NameNorthPlayerChanged(int selectedPlayerId);

        void playerWinnerIdChanged(int handId, int newPlayerWinnerId);

        void playerLooserIdChanged(int handId, int cellValue);

        void HandScoreChanged(int handId, int cellValue);

        void IsChickenHandChanged(int handId, bool cellValue);
    }
}
