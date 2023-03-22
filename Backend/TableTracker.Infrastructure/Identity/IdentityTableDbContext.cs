using System;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using TableTracker.Infrastructure.Identity;

namespace TableTracker.Infrastructure
{
    public class IdentityTableDbContext : IdentityDbContext<TableTrackerIdentityUser, TableTrackerIdentityRole, Guid>
    {
        public IdentityTableDbContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<TableTrackerIdentityUser> TableTrackerIdentityUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TableTrackerIdentityUser>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            builder.Entity<TableTrackerIdentityRole>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
        }
    }
}
