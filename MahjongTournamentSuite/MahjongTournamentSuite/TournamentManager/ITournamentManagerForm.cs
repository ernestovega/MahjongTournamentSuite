using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerForm
    {
        void CenterMainButtons();

        void AddRoundsButtons(int numRounds);
        
        void AddTablesButtons(int roundId, int numTables);
        
        void RemoveRoundsButtons();

        void RemoveTablesButtons();

        void GoToTableManager(int roundId, int tableId);

        void GoToRankings(Rankings rankings);

        void GoToHTMLViewer(HTMLRankings htmlRankings);
        
        void SelectRoundButton(int roundId);

        void UnselectRoundButton(int roundId);

        void SelectTableButton(int TableId);

        void UnselectTableButton(int TableId);

        void ShowButtonTeams();

        void HideButtonTeams();
    }
}
