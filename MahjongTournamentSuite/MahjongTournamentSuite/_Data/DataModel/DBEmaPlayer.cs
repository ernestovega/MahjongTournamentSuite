using System.ComponentModel.DataAnnotations;

namespace MahjongTournamentSuite._Data.DataModel
{
    public class DBEmaPlayer
    {
        [Key]
        public string EmaPlayerEmaNumber { get; set; }
        public string EmaPlayerLastName { get; set; }
        public string EmaPlayerName { get; set; }
        public string EmaPlayerCountryName { get; set; }

        public DBEmaPlayer() { }

        public DBEmaPlayer(string emaNumber, string lastName, string name, string countryName)
        {
            EmaPlayerEmaNumber = emaNumber;
            EmaPlayerLastName = lastName;
            EmaPlayerName = name;
            EmaPlayerCountryName = countryName;
        }
    }
}
