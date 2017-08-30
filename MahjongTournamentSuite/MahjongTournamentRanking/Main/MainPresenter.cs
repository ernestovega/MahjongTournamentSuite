using MahjongTournamentSuiteDataLayer.Data;

namespace MahjongTournamentRanking.Main
{
    class MainPresenter : IMainPresenter
    {
        private IDBManager _db;

        public MainPresenter()
        {
            _db = new DBManager();
        }

    }
}
