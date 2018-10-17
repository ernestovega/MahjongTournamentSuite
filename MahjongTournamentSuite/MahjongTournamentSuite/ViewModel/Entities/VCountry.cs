using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class VCountry
    {
        #region Constants
        
        public static readonly string COLUMN_COUNTRY_NAME = "CountryName";
        public static readonly string COLUMN_COUNTRY_SHORT_NAME = "CountryShortName";
        public static readonly string COLUMN_COUNTRY_IMAGE_URL = "CountryHtmlImageUrl";

        #endregion

        #region Properties
        
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
        public string CountryHtmlImageUrl { get; set; }

        #endregion

        #region Constructors

        public VCountry() { }

        public VCountry(string name, string shortName, string countryHtmlImageUrl)
        {
            CountryName = name;
            CountryShortName = shortName;
            CountryHtmlImageUrl = countryHtmlImageUrl;
        }

        #endregion
    }
}
