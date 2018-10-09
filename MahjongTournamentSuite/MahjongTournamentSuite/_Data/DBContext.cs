using MahjongTournamentSuite._Data.DataModel;
using System.Data.Entity;

namespace MahjongTournamentSuite._Data
{
    public class DBContext
    {
        /*
         * https://msdn.microsoft.com/en-us/library/jj193542(v=vs.113).aspx
         * 
         * (localdb)\MSSQLLocalDB
         */

        public class TournamentSuiteDB : DbContext
        {
            public DbSet<DBEmaPlayer> EmaPlayers { get; set; }

            public DbSet<DBTournament> Tournaments { get; set; }

            public DbSet<DBTeam> Teams { get; set; }

            public DbSet<DBCountry> Countries { get; set; }

            public DbSet<DBPlayer> Players { get; set; }

            public DbSet<DBTable> Tables { get; set; }

            public DbSet<DBHand> Hands { get; set; }
        }
    }
}
