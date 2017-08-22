using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahjongTournamentSuite.TeamSelector
{
    interface ITeamSelectorPresenter
    {
        void LoadTeams(int tournamentId);
    }
}
