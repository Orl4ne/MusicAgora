using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity.DAL
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, AccessRight, int>
    {
        public IdentityContext()
        {
        }
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=musicagoraIdentity.db;");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            var roles = new[]
            {
            new AccessRight("Musician"),
            new AccessRight("Librarian"),
            new AccessRight("Chief"),
            new AccessRight("Admin")
            };

            for (int i = 0; i < roles.Length; i++)
            {
                // ROLE ID IS NEVER SUPPOSED TO BE 0
                roles[i].Id = i + 1;
                roles[i].NormalizedName = roles[i].Name.ToUpper();
                modelBuilder.Entity<AccessRight>().HasData(roles[i]);
            }

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AccessRight> AccessRights { get; set; }
    }
}
