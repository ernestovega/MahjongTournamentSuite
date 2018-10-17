using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBCountry
    {
        [Key]
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
        public string CountryHtmlImageUrl { get; set; }



        public DBCountry() { }

        public DBCountry(string name, string shortName, string countryHtmlImageUrl)
        {
            CountryName = name;
            CountryShortName = shortName;
            CountryHtmlImageUrl = countryHtmlImageUrl;
        }
    }
}
