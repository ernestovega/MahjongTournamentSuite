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

        string TotalScoreEastPlayerChanged(string newScore);

        string TotalScoreSouthPlayerChanged(string newScore);

        string TotalScoreWestPlayerChanged(string newScore);

        string TotalScoreNorthPlayerChanged(string newScore);

        string PlayerWinnerIdChanged(int handId, string newValue);

        string PlayerLooserIdChanged(int handId, string newValue);

        string HandScoreChanged(int handId, string newValue);

        bool IsChickenHandChanged(int handId);

        string PlayerEastPenalytChanged(int handId, string newValue);

        string PlayerSouthPenalytChanged(int handId, string newValue);

        string PlayerWestPenalytChanged(int handId, string newValue);

        string PlayerNorthPenalytChanged(int handId, string newValue);
    }
}
