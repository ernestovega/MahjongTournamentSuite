﻿namespace MahjongTournamentSuite.HTMLViewer
{
    interface IHTMLViewerForm
    {
        void SetPlayersRankingHTMLText(string playersRanking);

        void SetTeamsRankingHTMLText(string teamsRanking);

        void SetChickenHandsRankingHTMLText(string playersChickenHandsRanking);

        void DisableTeamsControls();
    }
}
