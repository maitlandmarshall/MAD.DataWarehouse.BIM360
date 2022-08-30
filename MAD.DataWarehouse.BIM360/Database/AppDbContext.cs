using MAD.DataWarehouse.BIM360.Api.Accounts;
using MAD.DataWarehouse.BIM360.Api.Project;
using Microsoft.EntityFrameworkCore;

namespace MAD.DataWarehouse.BIM360.Database
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hub>(cfg =>
            {
                cfg.HasKey(y => y.Id);
            });

            modelBuilder.Entity<Project>(cfg =>
            {
                cfg.HasKey(y => y.Id);
                cfg.HasIndex(y => y.AccountId);
            });
        }
    }
}
