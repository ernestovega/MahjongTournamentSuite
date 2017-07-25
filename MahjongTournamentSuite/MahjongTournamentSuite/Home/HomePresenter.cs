using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MahjongTournamentSuite.Home
{
    class HomePresenter : IHomePresenter
    {
        #region Fields

        private IHomeForm _form;

        #endregion

        #region Constructor

        public HomePresenter(IHomeForm homeForm)
        {
            _form = homeForm;
        }

        #endregion

        #region IHomePresenter implementation

        #endregion
    }
}
