using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuite.EmaReport
{
    public class EmaReportPresenter : IEmaReportPresenter
    {
        private IEmaReportForm _form;
        private IDBManager _db;
        private int _tournamentId;
        private List<DBPlayer> _players;

        public EmaReportPresenter(IEmaReportForm form)
        {
            _form = form;
            _db = Injector.provideDBManager();
        }

        public void LoadForm(int tournamentId)
        {
            _tournamentId = tournamentId;
            _players = _db.GetTournamentPlayers(_tournamentId);
            List<DGVPlayerEma> dgvEmaPlayers = new List<DGVPlayerEma>(_players.Count);
            foreach (DBPlayer player in _players)
            {                
                dgvEmaPlayers.Add(
                    new DGVPlayerEma(
                        player.PlayerName,
                        player.PlayerLastName,
                        player.PlayerEmaNumber,
                        player.PlayerCountryName));
            }
        }
    }
}
