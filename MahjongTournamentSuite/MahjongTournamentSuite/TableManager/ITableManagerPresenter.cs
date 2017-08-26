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

        string PlayerWinnerIdChanged(int handId, string previousValue, string newValue);

        string PlayerLooserIdChanged(int handId, string previousValue, string newValue);

        string HandScoreChanged(int handId, string previousValue, string newValue);

        void IsChickenHandChanged(int handId, bool cellValue);

        string PlayerEastPenalytChanged(int handId, string previousValue, string newValue);

        string PlayerSouthPenalytChanged(int handId, string previousValue, string newValue);

        string PlayerWestPenalytChanged(int handId, string previousValue, string newValue);

        string PlayerNorthPenalytChanged(int handId, string previousValue, string newValue);
    }
}
