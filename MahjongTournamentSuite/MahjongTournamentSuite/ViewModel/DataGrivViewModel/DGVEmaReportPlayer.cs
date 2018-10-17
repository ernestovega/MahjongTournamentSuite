using MahjongTournamentSuite._Data.DataModel;

namespace MahjongTournamentSuite.EmaReport
{
    public class DGVEmaReportPlayer : VEmaPlayer
    {
        public static readonly string COLUMN_EMA_REPORT_PLAYER_PLACE = "EmaReportPlayerPlace";
        public static readonly string COLUMN_EMA_REPORT_PLAYER_TABLE_POINTS = "EmaReportPlayerTablePoints";
        public static readonly string COLUMN_EMA_REPORT_PLAYER_SCORE = "EmaReportPlayerScore";
        public static readonly string COLUMN_EMA_REPORT_PLAYER_COUNTRY_SHORT_NAME = "EmaReportPlayerCountryShortName";
        public static readonly string COLUMN_EMA_REPORT_PLAYER_COUNTRY_EMA_MEMBER = "EmaReportPlayerEmaMember";

        public string EmaReportPlayerPlace { get; set; }
        public string EmaReportPlayerTablePoints { get; set; }
        public string EmaReportPlayerScore { get; set; }
        public string EmaReportPlayerCountryShortName { get; set; }
        public string EmaReportPlayerEmaMember { get; set; }

        public DGVEmaReportPlayer() { }

        public DGVEmaReportPlayer(VEmaPlayer vEmaPlayer,
            string emaReportPlayerPlace,  
            string emaReportPlayerTablePoints, 
            string emaReportPlayerScore,
            string emaReportPlayerCountryShortName,
            string emaReportPlayerEmaMember) :
                base(vEmaPlayer.EmaPlayerEmaNumber, vEmaPlayer.EmaPlayerLastName, 
                    vEmaPlayer.EmaPlayerName, vEmaPlayer.EmaPlayerCountryName)
        {
            EmaReportPlayerTablePoints = emaReportPlayerTablePoints;
            EmaReportPlayerScore = emaReportPlayerScore;
            EmaReportPlayerPlace = emaReportPlayerPlace;
            EmaReportPlayerCountryShortName = emaReportPlayerCountryShortName;
            EmaReportPlayerEmaMember = emaReportPlayerEmaMember;
        }
    }
}