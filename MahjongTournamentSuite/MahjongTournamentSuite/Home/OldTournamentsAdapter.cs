using MahjongTournamentSuite.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    class OldTournamentsAdapter
    {
        public static List<ListViewItem> getItemsFromTournaments(List<Tournament> tournaments)
        {
            var listItems = new List<ListViewItem>();
            foreach (var tournament in tournaments)
            {
                var sId = tournament.Id.ToString();
                var sName = tournament.Name;
                var sCreationDate = tournament.CreationDate.ToLocalTime().ToString();
                listItems.Add(new ListViewItem(new string[] { sId, sName, sCreationDate }));
            }
            return listItems;
        }
    }
}
