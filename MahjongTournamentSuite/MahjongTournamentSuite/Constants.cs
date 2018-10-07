using System.Drawing;

namespace MahjongTournamentSuite.Resources
{
    public static class Constants
    {
        #region Colors

        public static readonly Color GREEN_MM = Color.FromArgb(0, 177, 106);
        public static readonly Color GREEN_MM_DARK = Color.FromArgb(0, 147, 76);
        public static readonly Color GREEN_MM_DARKER = Color.FromArgb(0, 117, 46);
        public static readonly Color GREEN_MM_DARKEST = Color.FromArgb(0, 97, 26);
        public static readonly Color GRAY_DISABLED = Color.FromArgb(65, 65, 65);
        public static readonly Color RED_CANCEL = Color.FromArgb(224, 0, 0);

        #endregion

        #region HTML TAGS
        public static readonly string HTML_PLAYERS_TABLE_TITLE = "</br></br><span class=\"titulo-enlaces cf size-large\" width=\"1024\"><h2 style=\"width: 1024px\">      <img src=\"http://www.mahjongmadrid.com/wp-content/uploads/2017/09/players_32_2.png\" style=\"margin-right:10px;\"/>Players</h2></span></br></br>";
        public static readonly string HTML_TEAMS_TABLE_TITLE = "</br></br><span class=\"titulo-enlaces cf size-large\" width=\"700\"> <h2 style=\"width: 700px\">         <img src=\"http://www.mahjongmadrid.com/wp-content/uploads/2017/09/teams_32_2.png\" style=\"margin-right:10px;\"/>Teams</h2></span></br></br>";
        public static readonly string HTML_CHICKEN_HANDS_TABLE_TITLE = "</br></br><span class=\"titulo-enlaces cf size-large\" width=\"850\"> <h2 style=\"width: 850px\"> <img src=\"http://www.mahjongmadrid.com/wp-content/uploads/2017/09/chicken_32_2.png\" style=\"margin-right:10px;\"/>Chicken Hands</h2></span></br></br>";
        public static readonly string HTML_BEST_HANDS_TABLE_TITLE = "</br></br><span class=\"titulo-enlaces cf size-large\" width=\"850\"> <h2 style=\"width: 850px\"> <img src=\"http://www.mahjongmadrid.com/wp-content/uploads/2017/09/best_hand_32_2.png\" style=\"margin-right:10px;\"/>Best Hands</h2></span></br></br>";

        public static readonly string HTML_OPEN_TABLE_PLAYERS = "<table dir=\"ltr\" class=\"alignnone size-large\" style=\"text-align: center;\" width=\"1024\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">";
        public static readonly string HTML_OPEN_TABLE_TEAMS = "<table dir=\"ltr\" class=\"alignnone size-large\" style=\"text-align: center;\" width=\"700\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">";
        public static readonly string HTML_OPEN_TABLE_CHICKEN_HANDS = "<table dir=\"ltr\" class=\"alignnone size-large\" style=\"text-align: center;\" width=\"850\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">";
        public static readonly string HTML_OPEN_TABLE_BEST_HANDS = "<table dir=\"ltr\" class=\"alignnone size-large\" style=\"text-align: center;\" width=\"850\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\">";
        public static readonly string HTML_OPEN_TBODY = "<tbody>";
        public static readonly string HTML_OPEN_COLGROUP = "<colgroup>";
        public static readonly string HTML_PLAYERS_HEADERS_TR = "<tr>"
			+ "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">#</td>"
			+ "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Name</td>"
			+ "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Points</td>"
			+ "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Score</td>"
			+ "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Team</td>"
			+ "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Country</td>"
		    + "</tr>";
        public static readonly string HTML_TEAMS_HEADERS_TR = "<tr>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">#</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Name</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Points</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Score</td>"
            + "</tr>";
        public static readonly string HTML_CHICKEN_HANDS_HEADERS_TR = "<tr>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">#</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Name</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Chicken hands</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Points</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Score</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Country</td>"
            + "</tr>";
        public static readonly string HTML_BEST_HANDS_HEADERS_TR = "<tr>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">#</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Name</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Points</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Score</td>"
            + "<th style=\"text-align: center; min-height: 42px; font-weight: bold;\">Country</td>"
            + "</tr>";
        public static readonly string HTML_OPEN_TR_HEADER_BOTTOM_SEPARATOR = "<tr style=\"height: 16px;\"/>";
        public static readonly string HTML_OPEN_TR = "<tr style=\"min-height: 42px;\">";
        public static readonly string HTML_OPEN_TR_TEAMS = "<tr style=\"height: 42px;\">";
        public static readonly string HTML_OPEN_TD = "<td style=\"text-align: center; min-height: 42px; font-size: 18pt;\">";
        public static readonly string HTML_OPEN_TD_BOLD = "<td style=\"text-align: center; min-height: 42px; font-size: 18pt; font-weight: bold;\">";
        public static readonly string HTML_COL_ORDER = "<col width=\"60\"/>";
        public static readonly string HTML_COL_NAME = "<col width=\"374\"/>";
        public static readonly string HTML_COL_POINTS = "<col width=\"80\"/>";
        public static readonly string HTML_COL_SCORE = "<col width=\"80\"/>";
        public static readonly string HTML_COL_TEAM = "<col width=\"350\"/>";
        public static readonly string HTML_COL_COUNTRY = "<col width=\"80\"/>";
        public static readonly string HTML_CLOSE_COLGROUP = "</colgroup>";
        public static readonly string HTML_CLOSE_TR = "</tr>";
        public static readonly string HTML_CLOSE_TD = "</td>";
        public static readonly string HTML_CLOSE_TBODY = "</tbody>";
        public static readonly string HTML_CLOSE_TABLE = "</table>";

        #endregion
    }
}
