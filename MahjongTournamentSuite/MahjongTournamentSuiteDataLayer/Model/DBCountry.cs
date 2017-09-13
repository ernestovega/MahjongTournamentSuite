using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MahjongTournamentSuiteDataLayer.Model
{
    public class DBCountry
    {
        #region Constants
        
        public static readonly string COLUMN_COUNTRY_NAME = "CountryName";
        public static readonly string COLUMN_COUNTRY_IMAGE_URL = "CountryImageUrl";

        #endregion

        #region Properties

        [Key]
        public string CountryName { get; set; }
        public string CountryImageUrl { get; set; }

        #endregion

        #region Constructors

        public DBCountry() { }

        public DBCountry(string name, string countryImageUrl)
        {
            CountryName = name;
            CountryImageUrl = countryImageUrl;
        }

        #endregion
    }
}
