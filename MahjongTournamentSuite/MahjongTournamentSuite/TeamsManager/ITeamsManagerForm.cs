using System.Collections.Generic;
using MahjongTournamentSuite._Data.DataModel;

namespace MahjongTournamentSuite.TeamsManager
{
    interface ITeamsManagerForm
    {
        void FillDGV(List<VTeam> _teams);

        void ShowMessageTeamNameInUse(string newName, int ownerTeamId);

        void DGVCancelEdit();
    }
}
