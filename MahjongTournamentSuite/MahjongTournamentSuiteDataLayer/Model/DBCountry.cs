using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBCountry
    {
        #region Constants
        
        public static readonly string COLUMN_COUNTRY_NAME = "CountryName";
        public static readonly string COLUMN_COUNTRY_IMAGE_URL = "CountryHtmlImageUrl";

        #endregion

        #region Properties

        [Key]
        public string CountryName { get; set; }
        public string CountryHtmlImageUrl { get; set; }

        #endregion

        #region Constructors

        public DBCountry() { }

        public DBCountry(string name, string countryHtmlImageUrl)
        {
            CountryName = name;
            CountryHtmlImageUrl = countryHtmlImageUrl;
        }

        #endregion
    }
}
