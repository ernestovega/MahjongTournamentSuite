using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahjongTournamentSuite.NewTournament
{
    class NewTournamentPresenter : INewTournamentPresenter
    {
        private INewTournamentForm _newTournamentForm;

        public NewTournamentPresenter(INewTournamentForm newTournamentForm)
        {
            this._newTournamentForm = newTournamentForm;
        }
    }
}
