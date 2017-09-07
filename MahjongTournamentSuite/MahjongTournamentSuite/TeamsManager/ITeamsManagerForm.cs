using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuite.TeamsManager
{
    interface ITeamsManagerForm
    {
        void FillDGV(List<DBTeam> _teams);

        void ShowMessageTeamNameInUse(string newName, int ownerTeamId);

        void DGVCancelEdit();
    }
}
