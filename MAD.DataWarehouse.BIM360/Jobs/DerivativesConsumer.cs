using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Data;
using MAD.DataWarehouse.BIM360.Api.Project;
using MAD.DataWarehouse.BIM360.Database;
using MAD.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    internal class DerivativesConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly IProjectClient projectClient;
        private readonly HttpClient httpClient;
        private readonly IBackgroundJobClient backgroundJobClient;

        public DerivativesConsumer(
            IDbContextFactory<AppDbContext> dbContextFactory,
            IProjectClient projectClient,
            HttpClient httpClient,
            IBackgroundJobClient backgroundJobClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.projectClient = projectClient;
            this.httpClient = httpClient;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task EnqueueDerivativesForConsumer()
        {
            using var db = await this.dbContextFactory.CreateDbContextAsync();

            var versions = db.Set<FolderItem>()
                .Where(x => x.Type == "versions")
                .Where(x => x.Attributes.FileType == "rvt")
                .Where(x => string.IsNullOrEmpty(x.Relationships.Derivatives.Meta.Link.Href) == false)
                .AsAsyncEnumerable();

            await foreach (var v in versions)
            {
                this.backgroundJobClient.Enqueue<DerivativesConsumer>(y => y.ConsumeDerivatives(v.ProjectId, v.Id));
            }
        }

        public async Task ConsumeDerivatives(string projectId, string folderItemId)
        {
            using var db = await this.dbContextFactory.CreateDbContextAsync();
            var version = await db.Set<FolderItem>().FindAsync(projectId, folderItemId);

            if (version is null)
                return;

            var derivatives = await this.httpClient.GetStringAsync(version.Relationships.Derivatives.Meta.Link.Href);
            var result = new FolderItemDerivative
            {
                ProjectId = projectId,
                FolderItemId = folderItemId,
                Data = derivatives
            };

            db.Upsert(result);
            await db.SaveChangesAsync();
        }
    }
}
