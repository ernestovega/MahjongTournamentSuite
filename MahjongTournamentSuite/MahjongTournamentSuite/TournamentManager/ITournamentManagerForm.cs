using MahjongTournamentSuite.EmaReport;
using MahjongTournamentSuite.ViewModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerForm
    {
        void AddRoundsButtons(int numRounds);
        
        void AddTablesButtons(int numTables);
        
        void RemoveRoundsButtons();

        void RemoveTablesButtons();

        void GoToTeamsManager();

        void GoToPlayersManager();

        void GoToTableManager(int roundId, int tableId);

        void GoToRankings(Rankings rankings);

        void GoToHTMLViewer(HTMLRankings htmlRankings);

        void GoToPlayersTables(int tournamentId);

        void GoToEmaReport(List<DGVPlayerEma> dgvEmaPlayers);

        void SelectRoundButton(int roundId);

        void UnselectRoundButton(int roundId);

        void SelectTableButton(int TableId);

        void UnselectTableButton(int TableId);

        void ShowButtonTeams();

        void HideButtonTeams();

        void PlayKoSound();

        void SetTournamentName(string tournamentName);

        void CenterMainButtons();
    }
}
