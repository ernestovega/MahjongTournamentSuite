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

        bool PlayerWinnerIdChanged(int handId, string newPlayerWinnerId);

        bool PlayerLooserIdChanged(int handId, string cellValue);

        bool HandScoreChanged(int handId, string cellValue);

        void IsChickenHandChanged(int handId, bool cellValue);
    }
}
