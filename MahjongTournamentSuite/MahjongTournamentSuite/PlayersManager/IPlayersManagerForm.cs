using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.PlayersManager
{
    interface IPlayersManagerForm
    {
        void FillDGV(List<DGVPlayer> players, bool isTeams);

        void DGVCancelEdit();

        void ShowMessagePlayerNameInUse(string newName, int ownerPlayerId);

        void ShowMessageTeamError();
        void MarkWrongTeamsPlayers(List<WrongTeam> wrongTeams);
        void ShowWrongNumberOfPlayersPerTeamMessage(List<WrongTeam> wrongTeams);
        void CleanWrongTeamsPlayers();
    }
}
