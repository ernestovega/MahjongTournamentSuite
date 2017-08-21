using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite.Model
{
    public class DBCountry
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public DBCountry() { }

        public DBCountry(int id, string name)
        {
            CountryID = id;
            CountryName = name;
        }
    }
}
