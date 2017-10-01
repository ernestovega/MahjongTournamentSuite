using System.Collections.Generic;
using MahjongTournamentSuiteDataLayer.Data;
using MahjongTournamentSuiteDataLayer.Model;

namespace MahjongTournamentSuite.EmaReport
{
    public class EmaReportPresenter : IEmaReportPresenter
    {
        private IEmaReportForm _form;
        List<DGVPlayerEma> _dgvEmaPlayers;

        public EmaReportPresenter(IEmaReportForm form)
        {
            _form = form;
        }

        public void LoadForm(List<DGVPlayerEma> dgvEmaPlayers)
        {
            _dgvEmaPlayers = dgvEmaPlayers;
            _form.LoadDgv(_dgvEmaPlayers);
        }
    }
}
