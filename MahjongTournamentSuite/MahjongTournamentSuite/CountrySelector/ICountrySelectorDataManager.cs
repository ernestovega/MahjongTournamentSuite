using System.Collections.Generic;

namespace MahjongTournamentSuite.CountrySelector
{
    public interface ICountrySelectorDataManager
    {
        List<string> GetCountriesNamesWichHaveImageUrl();
    }
}