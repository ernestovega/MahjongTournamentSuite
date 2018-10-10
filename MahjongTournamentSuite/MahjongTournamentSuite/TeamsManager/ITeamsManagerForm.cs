using System.Collections.Generic;
using MahjongTournamentSuite._Data.DataModel;

namespace MahjongTournamentSuite.TeamsManager
{
    interface ITeamsManagerForm
    {
        void FillDGVTeams(List<VTeam> teams);

        void FillDGVTeamPlayers(List<DGVTeamPlayer> dgvTeamPlayers);

        void ShowMessageTeamNameInUse(string newName, int ownerTeamId);

        void DGVCancelEdit();
    }
}
