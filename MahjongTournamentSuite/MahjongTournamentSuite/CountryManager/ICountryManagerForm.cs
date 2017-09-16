using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Model;
using MahjongTournamentSuite.Model;

namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerForm
    {
        void FillDGV(List<DGVCountry> dgvCountries);

        void DGVCancelEdit();
    }
}
