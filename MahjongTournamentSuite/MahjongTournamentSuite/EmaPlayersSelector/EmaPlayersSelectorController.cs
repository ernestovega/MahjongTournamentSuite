using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaPlayersSelector
{
    class EmaPlayersSelectorController : IEmaPlayersSelectorController
    {
        #region Fields

        private IEmaPlayersSelectorForm _form;
        private IEmaPlayersSelectorDataManager _data;
        private List<string> _emaPlayersNames;

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
            _emaPlayersNames = new List<string>();
            _emaPlayersNames.Add("");
            _emaPlayersNames.AddRange(_data.GetAvailableEmaPlayersNames(tournamentId));
            _form.FillLbEmaPlayersNames(_emaPlayersNames);
        }

        #endregion
    }
}
