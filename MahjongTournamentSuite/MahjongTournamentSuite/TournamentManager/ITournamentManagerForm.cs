using System.Collections.Generic;
using MahjongTournamentSuite.Model;
using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerForm
    {
        void CenterMainButtons();

        void AddRoundsSubButtons(int numRounds);

        void FillDGVWithTeams(List<DBTeam> teams);

        void FillDGVWithPlayers(List<DGVPlayer> players, bool isTeams);

        void MarkWrongTeamsPlayers(List<WrongTeam> wrongTeams);

        void CleanWrongTeamsPlayers();

        void AddRoundTablesButtons(int roundId, int numTables);

        void GoToTableManager(int roundId, int tableId);

        void GoToRankings(Rankings rankings);

        void GoToHTMLViewer(HTMLRankings htmlRankings);

        void SelectTeamsButton();

        void UnselectTeamsButton();

        void SelectPlayersButton();

        void UnselectPlayersButton();

        void SelectRoundsButton();

        void UnselectRoundsButton();

        void SelectRoundButton(int roundId);

        void UnselectRoundButton(int roundId);

        void SelectRoundTableButton(int TableId);

        void UnselectTableButton(int TableId);

        void ShowButtonTeams();

        void HideButtonTeams();

        void ShowButtonPlayers();

        void HideButtonPlayers();

        void ShowButtonRounds();

        void HideButtonRounds();

        void ShowWaitCursor();

        void ShowDefaultCursor();

        void ShowDGV();

        void HideDGV();

        void ShowButtonPlayersBorder();

        void HideButtonPlayersBorder();

        void EmptyPanelRoundsButtons();

        void EmptyPanelRoundTablesButtons();

        void ShowRoundsButtonsAndTablesPanel();

        void HideRoundsButtonsAndTablesPanel();

        void DGVCancelEdit();

        void ShowMessageTeamNameInUse(string usedName, int ownerTeamId);

        void ShowMessagePlayerNameInUse(string newName, int ownerPlayerId);

        void ShowMessageCountryError();

        void ShowMessageTeamError();

        void ShowWrongNumberOfPlayersPerTeamMessage(List<WrongTeam> wrongTeamNames);
    }
}
