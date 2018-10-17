using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaReport
{
    public class EmaReportController : IEmaReportController
    {
        private IEmaReportForm _form;
        List<DGVEmaReportPlayer> _dgvEmaReportEmaPlayer;

        public EmaReportController(IEmaReportForm form)
        {
            _form = form;
        }

        public void LoadForm(List<DGVEmaReportPlayer> dgvEmaReportEmaPlayer)
        {
            _dgvEmaReportEmaPlayer = dgvEmaReportEmaPlayer;
            _form.LoadDgv(_dgvEmaReportEmaPlayer);
        }
    }
}
