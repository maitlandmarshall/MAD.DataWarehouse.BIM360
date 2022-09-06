using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Project;
using MAD.DataWarehouse.BIM360.Database;
using MAD.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    internal class FolderConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly IProjectClient projectClient;
        private readonly IBackgroundJobClient backgroundJobClient;

        public FolderConsumer(
            IDbContextFactory<AppDbContext> dbContextFactory,
            IProjectClient projectClient,
            IBackgroundJobClient backgroundJobClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.projectClient = projectClient;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task ConsumeFolders(string hubId, string projectId)
        {
            hubId = this.PrefixWithB(hubId);
            projectId = this.PrefixWithB(projectId);

            using var db = await dbContextFactory.CreateDbContextAsync();
            var topFolders = await projectClient.TopFolders(hubId, projectId);

            foreach (var t in topFolders.Data)
            {
                db.Upsert(t);
            }

            await db.SaveChangesAsync();

            foreach (var t in topFolders.Data)
            {
                backgroundJobClient.Enqueue<FolderConsumer>(y => y.ConsumeFolderContents(hubId, projectId, t.Id));
            }
        }

        public async Task ConsumeFolderContents(string hubId, string projectId, string folderId)
        {
            using var db = await dbContextFactory.CreateDbContextAsync();
            var topFolders = await projectClient.FolderContents(hubId, projectId, folderId);

            foreach (var t in topFolders.Data)
            {
                db.Upsert(t);
            }

            await db.SaveChangesAsync();

            foreach (var t in topFolders.Data)
            {
                if (t.Type != "folders")
                    continue;

                backgroundJobClient.Enqueue<FolderConsumer>(y => y.ConsumeFolderContents(hubId, projectId, t.Id));
            }
        }

        private string PrefixWithB(string id)
        {
            if (id.StartsWith("b.") == false)
                return $"b.{id}";

            return id;
        }
    }
}
