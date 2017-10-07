using System.Collections.Generic;
using MahjongTournamentSuite._Data.DataModel;
using MahjongTournamentSuite.ViewModel;

namespace MahjongTournamentSuite.CountryManager
{
    interface ICountryManagerForm
    {
        void FillDGV(List<DGVCountry> dgvCountries);

        void DGVCancelEdit();
    }
}
