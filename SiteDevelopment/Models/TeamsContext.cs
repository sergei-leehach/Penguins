using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class SiteEntities : DbContext
    {
        public SiteEntities()
            : base("name=SiteEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<Standings> Standings { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
    }
}