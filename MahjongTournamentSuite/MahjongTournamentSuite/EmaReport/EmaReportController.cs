using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaReport
{
    public class EmaReportController : IEmaReportController
    {
        private IEmaReportForm _form;
        List<DGVEmaPlayer> _dgvEmaPlayers;

        public EmaReportController(IEmaReportForm form)
        {
            _form = form;
        }

        public void LoadForm(List<DGVEmaPlayer> dgvEmaPlayers)
        {
            _dgvEmaPlayers = dgvEmaPlayers;
            _form.LoadDgv(_dgvEmaPlayers);
        }
    }
}
