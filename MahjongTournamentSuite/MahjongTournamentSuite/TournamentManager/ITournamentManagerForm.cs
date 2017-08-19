using System.Collections.Generic;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerForm
    {
        void AddRoundsSubButtons(int numRounds);

        void FillDGVWithTeams(List<DBTeam> teams);

        void FillDGVWithPlayers(List<DBPlayer> players, bool isTeams);

        void AddRoundTablesButtons(int roundId, int numTables);

        void ShowButtonTeams();

        void HideButtonTeams();

        void ShowDGV();

        void HideDGV();

        void EmptyPanelRoundTablesButtons();

        void ShowRoundsButtonsAndTablesPanel();

        void HideRoundsButtonsAndTablesPanel();
        void EmptyPanelRoundsButtons();
    }
}
