using MahjongTournamentSuite._Data.DataModel;
using System.Collections.Generic;

namespace MahjongTournamentSuite.EmaPlayersManager
{
    public interface IEmaPlayersManagerDataManager
    {
        List<VEmaPlayer> GetEmaPlayers();

        List<VCountry> GetCountries();

        void UpdateEmaPlayerEmaNumber(string oldPlayerEmaNumber, string newEmaPlayerEmaNumber);

        void UpdateEmaPlayerName(string playerEmaNumber, string newName);

        void UpdateEmaPlayerLastName(string playerEmaNumber, string newLastName);

        void UpdateEmaPlayerCountry(string playerEmaNumber, string newCountryName);
    }
}