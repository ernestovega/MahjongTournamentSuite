﻿using MahjongTournamentSuite.Model;
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
            public DbSet<DBTournament> Tournaments { get; set; }

            public DbSet<DBTeam> Teams { get; set; }

            public DbSet<DBCountry> Countries { get; set; }

            public DbSet<DBPlayer> Players { get; set; }

            public DbSet<DBTable> Tables { get; set; }

            public DbSet<DBHand> Hands { get; set; }
        }
    }
}
