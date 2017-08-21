﻿using System.Collections.Generic;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerForm
    {
        void AddRoundsSubButtons(int numRounds);

        void FillDGVWithTeams(List<DBTeam> teams);

        void FillDGVWithPlayers(List<DGVPlayer> players, bool isTeams);

        void AddRoundTablesButtons(int roundId, int numTables);

        void GoToTableManager(int tournamentId, int roundId, int tableId);

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

        void ShowDGV();

        void HideDGV();

        void EmptyPanelRoundsButtons();

        void EmptyPanelRoundTablesButtons();

        void ShowRoundsButtonsAndTablesPanel();

        void HideRoundsButtonsAndTablesPanel();

        void DGVCancelEdit();

        void ShowMessageTeamNameInUse(string usedName, int ownerTeamId);

        void ShowMessagePlayerNameInUse(string newName, int ownerPlayerId);
    }
}
