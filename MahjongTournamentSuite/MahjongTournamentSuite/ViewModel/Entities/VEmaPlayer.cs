using System;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class VEmaPlayer
    {
        #region Constants
        
        public static readonly string COLUMN_EMA_PLAYER_EMA_NUMBER = "EmaPlayerEmaNumber";
        public static readonly string COLUMN_EMA_PLAYER_LAST_NAME = "EmaPlayerLastName";
        public static readonly string COLUMN_EMA_PLAYER_NAME = "EmaPlayerName";
        public static readonly string COLUMN_EMA_PLAYER_COUNTRY_NAME = "EmaPlayerCountryName";

        #endregion

        #region Properties
        
        public string EmaPlayerEmaNumber { get; set; }
        public string EmaPlayerLastName { get; set; }
        public string EmaPlayerName { get; set; }
        public string EmaPlayerCountryName { get; set; }

        #endregion

        #region Constructors

        public VEmaPlayer() { }

        public VEmaPlayer(string emaPlayerEmaNumber, string emaPlayerLastName,
            string emaPlayerName, string emaPlayerCountryName)
        {
            EmaPlayerEmaNumber = emaPlayerEmaNumber;
            EmaPlayerLastName = emaPlayerLastName;
            EmaPlayerName = emaPlayerName;
            EmaPlayerCountryName = emaPlayerCountryName;
        }

        internal string getFullName()
        {
            return string.Format("{0}, {1}", EmaPlayerLastName, EmaPlayerName);
        }

        #endregion
    }
}
