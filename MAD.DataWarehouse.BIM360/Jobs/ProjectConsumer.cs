using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Accounts;
using MAD.DataWarehouse.BIM360.Database;
using MAD.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    internal class ProjectConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly IAccountsClient accountsClient;
        private readonly IBackgroundJobClient backgroundJobClient;

        public ProjectConsumer(
            IDbContextFactory<AppDbContext> dbContextFactory,
            IAccountsClient accountsClient,
            IBackgroundJobClient backgroundJobClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.accountsClient = accountsClient;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task ConsumeProjects(string hubId, int offset)
        {
            const int limit = 100;
            using var db = await this.dbContextFactory.CreateDbContextAsync();

            // Note that for BIM 360 Docs, a hub ID corresponds to an account ID in the BIM 360 API.
            // To convert an account ID into a hub ID you need to add a “b.” prefix. For example, an account ID of c8b0c73d-3ae9 translates to a hub ID of b.c8b0c73d-3ae9.
            // https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-hub_id-projects-GET/
            var accountId = hubId.Substring(2);
            var projects = await this.accountsClient.Projects(accountId, limit: limit, offset: offset);

            foreach (var p in projects)
            {
                db.Upsert(p);
            }

            await db.SaveChangesAsync();

            foreach (var p in projects)
            {
                this.backgroundJobClient.Enqueue<FolderConsumer>(y => y.ConsumeFolders(hubId, p.Id));
            }

            if (projects.Count() >= limit)
            {
                this.backgroundJobClient.Enqueue<ProjectConsumer>(y => y.ConsumeProjects(hubId, offset + limit));
            }
        }
    }
}
