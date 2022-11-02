using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Data;
using MAD.DataWarehouse.BIM360.Api.Project;
using MAD.DataWarehouse.BIM360.Database;
using MAD.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    internal class FolderConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly IProjectClient projectClient;
        private readonly IDataClient dataClient;
        private readonly IBackgroundJobClient backgroundJobClient;

        public FolderConsumer(
            IDbContextFactory<AppDbContext> dbContextFactory,
            IProjectClient projectClient,
            IDataClient dataClient,
            IBackgroundJobClient backgroundJobClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.projectClient = projectClient;
            this.dataClient = dataClient;
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
                t.ProjectId = projectId;
                db.Upsert(t);
            }

            await db.SaveChangesAsync();

            foreach (var t in topFolders.Data)
            {
                backgroundJobClient.Enqueue<FolderConsumer>(y => y.ConsumeFolderContents(projectId, t.Id));
            }
        }

        public async Task ConsumeFolderContents(string projectId, string folderId)
        {
            using var db = await dbContextFactory.CreateDbContextAsync();
            
            // Get the contents of the folder
            var contents = await dataClient.FolderContents(projectId, folderId);

            // Merge the Data and Included properties, as the Included property may include versions of files
            var data = contents.Data.Concat(contents.Included ?? new List<FolderItem>()).ToList();

            foreach (var t in data)
            {
                t.ProjectId = projectId;
                db.Upsert(t);
            }

            await db.SaveChangesAsync();

            foreach (var t in contents.Data)
            {
                if (t.Type != "folders")
                    continue;

                backgroundJobClient.Enqueue<FolderConsumer>(y => y.ConsumeFolderContents(projectId, t.Id));
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
