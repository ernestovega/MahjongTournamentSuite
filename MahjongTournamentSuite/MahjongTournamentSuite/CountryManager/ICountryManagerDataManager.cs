using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.CountryManager
{
    public interface ICountryManagerDataManager
    {
        List<VCountry> GetCountries(); 

        void UpdateCountryImageURL(string countryName, string newValue);

        void UpdateCountryShortName(string countryName, string newValue);
    }
}
