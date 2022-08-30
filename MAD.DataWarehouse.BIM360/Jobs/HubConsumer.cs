using MAD.DataWarehouse.BIM360.Api.Project;
using MAD.DataWarehouse.BIM360.Database;
using MAD.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    internal class HubConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly IProjectClient projectClient;

        public HubConsumer(IDbContextFactory<AppDbContext> dbContextFactory, IProjectClient projectClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.projectClient = projectClient;
        }

        public async Task ConsumeHubs()
        {
            using var db = await this.dbContextFactory.CreateDbContextAsync();
            var response = await this.projectClient.Hubs();

            foreach (var h in response.Data)
            {
                db.Upsert(h);
            }

            await db.SaveChangesAsync();
        }
    }
}
