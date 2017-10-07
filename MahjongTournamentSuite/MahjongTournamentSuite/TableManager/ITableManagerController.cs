namespace MahjongTournamentSuite.TableManager
{
    interface ITableManagerController
    {
        void LoadForm(int tournamentId, int roundId, int tableId);

        void NameEastPlayerChanged(string selectedPlayerId);

        void NameSouthPlayerChanged(string selectedPlayerId);

        void NameWestPlayerChanged(string selectedPlayerId);

        void NameNorthPlayerChanged(string selectedPlayerId);

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

        void IsCompletedCheckedChanged(bool isChecked);
    }
}
