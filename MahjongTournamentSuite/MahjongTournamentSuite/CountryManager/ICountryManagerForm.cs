using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerForm
    {
        void FillDGV(List<DBCountry> countries);

        void DGVCancelEdit();
    }
}
