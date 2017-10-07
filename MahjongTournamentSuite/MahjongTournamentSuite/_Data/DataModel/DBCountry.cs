using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBCountry
    {
        [Key]
        public string CountryName { get; set; }
        public string CountryHtmlImageUrl { get; set; }
        


        public DBCountry() { }

        public DBCountry(string name, string countryHtmlImageUrl)
        {
            CountryName = name;
            CountryHtmlImageUrl = countryHtmlImageUrl;
        }
    }
}
