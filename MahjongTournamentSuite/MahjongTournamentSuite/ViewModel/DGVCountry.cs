using MahjongTournamentSuite._Data.DataModel;
using System.Drawing;

namespace MahjongTournamentSuite.ViewModel
{
    public class DGVCountry : VCountry
    {
        public static readonly string COLUMN_COUNTRY_COUNTRY_FLAG = "CountryFlag";
        
        public Image CountryFlag { get; set; }

        public DGVCountry() {}

        public DGVCountry(VCountry dbCountry, Image countryFlag) : 
            base(dbCountry.CountryName, dbCountry.CountryHtmlImageUrl)
        {
            CountryFlag = countryFlag;
        }
    }
}
