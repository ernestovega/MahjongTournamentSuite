using System.Collections.Generic;

namespace MahjongTournamentSuite.ViewModel
{
    public class DGVPlayerCard
    {
        public static readonly string COLUMN_PLAYER_NUMBER = "Number";
        public static readonly string COLUMN_PLAYER_NAME = "Name";
        public static readonly string COLUMN_PLAYER_SURNAME = "Surname";
        public static readonly string COLUMN_PLAYER_TEAM = "Team";
        public static readonly string COLUMN_PLAYER_COUNTRY = "Country";
        public static readonly string COLUMN_PLAYER_TABLE_1 = "Table1";
        public static readonly string COLUMN_PLAYER_TABLE_2 = "Table2";
        public static readonly string COLUMN_PLAYER_TABLE_3 = "Table3";
        public static readonly string COLUMN_PLAYER_TABLE_4 = "Table4";
        public static readonly string COLUMN_PLAYER_TABLE_5 = "Table5";
        public static readonly string COLUMN_PLAYER_TABLE_6 = "Table6";
        public static readonly string COLUMN_PLAYER_TABLE_7 = "Table7";

        public int Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Team { get; set; }
        public string Country { get; set; }
        public int Table1 { get; set; }
        public int Table2 { get; set; }
        public int Table3 { get; set; }
        public int Table4 { get; set; }
        public int Table5 { get; set; }
        public int Table6 { get; set; }
        public int Table7 { get; set; }

        public DGVPlayerCard(
            int number, 
            string name, 
            string surname, 
            string team, string 
            country, 
            int table1,
            int table2,
            int table3,
            int table4,
            int table5,
            int table6,
            int table7
        )
        {
            Number = number;
            Name = name;
            Surname = surname;
            Team = team;
            Country = country;
            Table1 = table1;
            Table2 = table2;
            Table3 = table3;
            Table4 = table4;
            Table5 = table5;
            Table6 = table6;
            Table7 = table7;
        }
    }
}
