using MahjongTournamentSuite.Model;
using System.Data.Entity;

namespace MahjongTournamentSuite.Data
{
    class DBContext
    {
        /**
         * https://msdn.microsoft.com/en-us/library/jj193542(v=vs.113).aspx
         */

        public class TournamentSuiteDB : DbContext
        {
            public DbSet<Player> Players { get; set; }

            public DbSet<Table> Tables { get; set; }

            public DbSet<Tournament> Tournaments { get; set; }
        }
    }
}
