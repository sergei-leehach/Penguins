using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Models
{
    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

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
}