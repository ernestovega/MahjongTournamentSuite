﻿using System.Collections.Generic;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.TournamentManager
{
    interface ITournamentManagerForm
    {
        void AddButtonTeams();

        void AddPlayersButton();

        void AddRoundsButtons(int numRounds);

        void FillDGVWithTeams(List<DBTeam> teams);

        void FillDGVWithPlayers(List<DBPlayer> players);

        void FillPanelTournamentWithRoundButtons(int roundId, int numTables);

        void ShowDGV();

        void HideDGV();

        void EmptyPanelRoundButtons();

        void ShowRoundsButtonsAndPanel();

        void HideRoundsButtonsAndPanel();
    }
}
