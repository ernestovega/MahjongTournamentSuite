using System.Collections.Generic;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingForm
    {
        void SetNumRowsPerScreen(int numRowsPerScreen);

        void FillDGVPlayersFromThread(List<PlayerRanking> playersRankingsRange, bool isTeams);

        void FillDGVTeamsFromThread(List<TeamRanking> teamsRankingsRange);

        void FillDGVPlayersChickenHandsFromThread(List<ChickenHandRanking> playersChickenHandsRankingsRange);

        void CloseForm();

        void SetSecondsLabel(string seconds);

        void UpdateProgressFromThread(string leftTime);

        void ShowButtonSecondsDown();

        void HideButtonSecondsDown();

        void ShowButtonPlay();

        void HideButtonPlay();

        void ShowButtonPause();

        void HideButtonPause();
    }
}
