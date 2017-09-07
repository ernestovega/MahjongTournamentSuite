using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBCountry
    {
        #region Constants

        public static readonly string COLUMN_COUNTRY_ID = "CountryId";
        public static readonly string COLUMN_COUNTRY_NAME = "CountryName";
        public static readonly string COLUMN_COUNTRY_IMAGE_URL = "CountryImageUrl";

        #endregion

        #region Properties

        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryImageUrl { get; set; }

        #endregion

        #region Constructors

        public DBCountry() { }

        public DBCountry(int countryId, string name, string countryImageUrl)
        {
            CountryId = countryId;
            CountryName = name;
            CountryImageUrl = countryImageUrl;
        }

        #endregion
    }
}
