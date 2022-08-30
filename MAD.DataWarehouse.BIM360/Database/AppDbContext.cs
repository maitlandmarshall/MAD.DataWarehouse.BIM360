using Microsoft.EntityFrameworkCore;

namespace MAD.DataWarehouse.BIM360.Database
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
