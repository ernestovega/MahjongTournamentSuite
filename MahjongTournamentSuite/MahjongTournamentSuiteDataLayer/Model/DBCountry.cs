using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBCountry
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public DBCountry() { }

        public DBCountry(int countryId, string name)
        {
            CountryId = countryId;
            CountryName = name;
        }
    }
}
