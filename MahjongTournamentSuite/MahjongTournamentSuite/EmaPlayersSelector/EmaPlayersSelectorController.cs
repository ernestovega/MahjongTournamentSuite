using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    class EmaPlayersSelectorController : IEmaPlayersSelectorController
    {
        #region Fields

        private IEmaPlayersSelectorForm _form;
        private IEmaPlayersSelectorDataManager _data;
        private List<string> _filteredAvailableEmaPlayersNames;
        private string filter;
        private int _tournamentId;

        #endregion

        #region Constructor

        public EmaPlayersSelectorController(IEmaPlayersSelectorForm emaPlayersSelectorForm)
        {
            _form = emaPlayersSelectorForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountrySelectorController implementation

        public void LoadForm(int tournamentId)
        {
            _tournamentId = tournamentId;
            _filteredAvailableEmaPlayersNames = _data.GetAvailableEmaPlayersNames(tournamentId);
            _filteredAvailableEmaPlayersNames.Insert(0, "");

            _form.FillLbEmaPlayersNames(_filteredAvailableEmaPlayersNames);
        }

        public void FilterList(string text)
        {
            filter = text;
            _filteredAvailableEmaPlayersNames = _data.GetAvailableEmaPlayersNames(_tournamentId);
            _filteredAvailableEmaPlayersNames = _filteredAvailableEmaPlayersNames.FindAll(
                x => x.ToLower().Contains(filter.ToLower()));
            _filteredAvailableEmaPlayersNames.Insert(0, "");
            _form.FillLbEmaPlayersNames(_filteredAvailableEmaPlayersNames);
        }

        #endregion
    }
}
