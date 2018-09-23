using System.Collections.Generic;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.Ranking
{
    interface IRankingForm
    {
        void SetNumRowsPerScreen(int numRowsPerScreen);

        void FillDGVPlayersFromThread(List<PlayerRanking> playersRankingsRange, bool isTeams);

        void FillDGVTeamsFromThread(List<TeamRanking> teamsRankingsRange);

        void FillDGVPlayersChickenHandsFromThread(List<ChickenHandRanking> playersChickenHandsRankingsRange);

        void CloseForm();

        void SetNumSecondsLabel(string numSeconds);

        void SetNumRowsLabel(string numRows);

        void UpdateProgressFromThread(string leftTime);

        void ShowButtonSecondsDown();

        void HideButtonSecondsDown();

        void ShowButtonRowsUp();

        void HideButtonRowsUp();

        void ShowButtonRowsDown();

        void HideButtonRowsDown();

        void ShowButtonPlay();

        void HideButtonPlay();

        void ShowButtonPause();

        void HideButtonPause();
    }
}
