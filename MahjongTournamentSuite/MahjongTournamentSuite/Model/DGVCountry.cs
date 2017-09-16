using MahjongTournamentSuiteDataLayer.Model;
using System.Drawing;

namespace MahjongTournamentSuite.Model
{
    public class DGVCountry : DBCountry
    {
        public static readonly string COLUMN_COUNTRY_COUNTRY_FLAG = "CountryFlag";
        
        public Image CountryFlag { get; set; }

        public DGVCountry() {}

        public DGVCountry(DBCountry dbCountry, Image countryFlag) : 
            base(dbCountry.CountryName, dbCountry.CountryHtmlImageUrl)
        {
            CountryFlag = countryFlag;
        }
    }
}
