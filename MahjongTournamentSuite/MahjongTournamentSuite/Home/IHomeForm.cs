using System.Collections.Generic;
using System.Windows.Forms;

namespace MahjongTournamentSuite.Home
{
    internal interface IHomeForm
    {
        void LoadList(List<ListViewItem> tournamentItems);
    }
}