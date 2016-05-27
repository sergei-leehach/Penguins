using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class SiteContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Standings> Standings { get; set; }
        public DbSet<Team> Teams { get; set; }

        public SiteContext() : base("name=SiteContext")
        {
            Database.SetInitializer(new RoleInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Tag>()
        //        .HasOptional(x => x.Bundle)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Bundle>()
        //        .HasOptional(x => x.News)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);
        //}
    }

    public class RoleInitializer : DropCreateDatabaseAlways<SiteContext>
    {
        protected override void Seed(SiteContext context)
        {
            IList<Role> defaultRoles = new List<Role>();

            defaultRoles.Add(new Role() { Id = 1, Name = "admin" });
            defaultRoles.Add(new Role() { Id = 2, Name = "user" });

            foreach (var role in defaultRoles)
            {
                context.Roles.Add(role);
            }
            base.Seed(context);
        }
    }
}