using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahjongTournamentSuite.ViewModel
{
    public class PlayerTables
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public List<DGVPlayerTable> DgvPlayerTables { get; set; }

        public PlayerTables(int playerId, string playerName, List<DGVPlayerTable> dgvPlayerTables)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            DgvPlayerTables = dgvPlayerTables;
        }

    }
}
