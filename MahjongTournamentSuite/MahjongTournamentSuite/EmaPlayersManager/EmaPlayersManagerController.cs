using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;
using MahjongTournamentSuite.Resources.flags;
using MahjongTournamentSuite.EmaReport;
using System;

namespace MahjongTournamentSuite.EmaPlayersManager
{
    class EmaPlayersManagerController : IEmaPlayersManagerController
    {
        #region Fields

        private IEmaPlayersManagerForm _form;
        private IEmaPlayersManagerDataManager _data;
        private List<VEmaPlayer> _emaPlayers;
        private List<VCountry> _countries;

        #endregion

        #region Constructor

        public EmaPlayersManagerController(IEmaPlayersManagerForm emaPlayersManagerForm)
        {
            _form = emaPlayersManagerForm;
            _data = Injector.provideDataManager();
        }

        #endregion

        #region ICountryManagerController implementation

        public void LoadForm()
        {
            _emaPlayers = _data.GetEmaPlayers();
            _countries = _data.GetCountries();

            List<DGVEmaPlayer> dgvEmaPlayers = new List<DGVEmaPlayer>(_emaPlayers.Count);
            foreach (VEmaPlayer emaPlayer in _emaPlayers)
            {
                dgvEmaPlayers.Add(new DGVEmaPlayer(emaPlayer, "0", "0",
                    CountryFlags.GetFlagImage(emaPlayer.EmaPlayerCountryName)));
            }

            _form.FillDGV(dgvEmaPlayers);
        }

        public void EmaPlayerEmaNumberChanged(string oldEmaNumber, string newEmaNumber)
        {
            string ownerPlayerEmaNumberEmaNumber = GetOwnerPlayerEmaNumberEmaNumber(newEmaNumber);
            if (ownerPlayerEmaNumberEmaNumber.Equals(string.Empty))
            {
                _data.UpdateEmaPlayerEmaNumber(oldEmaNumber, newEmaNumber);
                return;
            }
            _form.PlayKoSound();
            _form.DGVCancelEdit();
            _form.ShowMessagePlayerEmaNumberInUse(newEmaNumber);
        }

        private string GetOwnerPlayerEmaNumberEmaNumber(string newEmaNumber)
        {
            VEmaPlayer ownerEmaPlayer = _emaPlayers.Find(x => x.EmaPlayerEmaNumber.Equals(newEmaNumber,
                StringComparison.InvariantCulture));
            if (ownerEmaPlayer == null)
                return string.Empty;
            else
                return ownerEmaPlayer.EmaPlayerEmaNumber;
        }

        public void EmaPlayerLastNameChanged(string emaPlayerEmaNumber, string newLastName)
        {
            _data.UpdateEmaPlayerLastName(emaPlayerEmaNumber, newLastName);
        }

        public void EmaPlayerNameChanged(string emaPlayerEmaNumber, string newName)
        {
            _data.UpdateEmaPlayerName(emaPlayerEmaNumber, newName);
        }

        public void EmaPlayerCountryChanged(string emaPlayerEmaNumber, string newCountryName)
        {
            _data.UpdateEmaPlayerCountry(emaPlayerEmaNumber, newCountryName);
        }

        #endregion

        #region Private



        #endregion
    }
}
