using System.Collections.Generic;
using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.ViewModel;

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

        void PlayKoSound();
    }
}
