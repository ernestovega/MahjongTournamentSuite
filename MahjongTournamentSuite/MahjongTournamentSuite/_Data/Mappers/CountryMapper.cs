using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite._Data.Mappers
{
    public class CountryMapper
    {
        public static List<VCountry> GetViewModel(List<DBCountry> dbCountrys)
        {
            List<VCountry> vCountrys = new List<VCountry>(dbCountrys.Count);
            foreach(DBCountry dbCountry in dbCountrys)
                vCountrys.Add(GetViewModel(dbCountry));

            return vCountrys;
        }

        public static VCountry GetViewModel(DBCountry dbCountry)
        {
            return new VCountry(
                dbCountry.CountryName,
                dbCountry.CountryHtmlImageUrl);
        }

        public static List<DBCountry> GetDataModel(List<VCountry> vCountrys)
        {
            List<DBCountry> dbCountrys = new List<DBCountry>(vCountrys.Count);
            foreach (VCountry vCountry in vCountrys)
                dbCountrys.Add(GetDataModel(vCountry));

            return dbCountrys;
        }

        public static DBCountry GetDataModel(VCountry vCountry)
        {
            return new DBCountry(
                vCountry.CountryName,
                vCountry.CountryHtmlImageUrl);
        }
    }
}
