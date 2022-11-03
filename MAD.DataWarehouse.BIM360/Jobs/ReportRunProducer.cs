using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Buckets;
using MAD.DataWarehouse.BIM360.Api.Data;
using MAD.DataWarehouse.BIM360.Api.DesignAutomation;
using MAD.DataWarehouse.BIM360.Database;
using Microsoft.EntityFrameworkCore;
using MIFCore.Hangfire;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    public class ReportRunProducer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IDesignAutomationClient designAutomationClient;
        private readonly BucketsClient bucketsClient;
        private readonly AppConfig appConfig;

        public ReportRunProducer(
            IDbContextFactory<AppDbContext> dbContextFactory,
            IBackgroundJobClient backgroundJobClient,
            IDesignAutomationClient designAutomationClient,
            BucketsClient bucketsClient,
            AppConfig appConfig)
        {
            this.dbContextFactory = dbContextFactory;
            this.backgroundJobClient = backgroundJobClient;
            this.designAutomationClient = designAutomationClient;
            this.bucketsClient = bucketsClient;
            this.appConfig = appConfig;
        }

        public async Task EnqueueVersionsForWorkItem()
        {
            using var db = await dbContextFactory.CreateDbContextAsync();

            var versions = db.Set<FolderItem>()
                .Where(x => x.Type == "versions")
                .Where(x => x.Attributes.FileType == "rvt")
                .AsAsyncEnumerable();
                
            await foreach (var v in versions)
            {
                this.backgroundJobClient.Enqueue<ReportRunProducer>(y => y.EnqueueWorkItem(v.ProjectId, v.Id));
            }
        }

        public async Task EnqueueWorkItem(string projectId, string folderItemId)
        {
            using var db = await dbContextFactory.CreateDbContextAsync();
            var version = db.Set<FolderItem>().Find(folderItemId, projectId);

            if (version is null)
                return;

            // Create a downloadable link for the version to be consumed by the WorkItem
            var downloadLink = await this.bucketsClient.GetSignedS3DownloadUrl(version.Relationships.Storage.Meta.Link.Href);

            // Create an uploadable link for the result to be uploaded by the WorkItem
            var uploadObjectKey = $"{Guid.NewGuid()}.zip";
            var uploadLink = await this.bucketsClient.GetSignedUploadUrl(this.appConfig.BucketKey, uploadObjectKey);

            var workItem = await this.designAutomationClient.CreateWorkItem(new
            {
                activityId = this.appConfig.ActivityId,
                arguments = new
                {
                    rvtFile = new
                    {
                        url = downloadLink,
                    },
                    result = new
                    {
                        verb = "put",
                        url = uploadLink
                    }
                }
            });

            var reportRun = new ReportRun
            {
                WorkItemId = workItem.Id,
                FolderItemId = folderItemId,
                ProjectId = projectId,
                ResultObjectKey = uploadObjectKey,
                Status = workItem.Status,
                Stats = workItem.Stats
            };

            db.Add(reportRun);
            await db.SaveChangesAsync();

            this.backgroundJobClient.Enqueue<ReportRunProducer>(y => y.HandleWorkItem(workItem.Id));
        }

        [AutomaticRetry(Attempts = 15)]
        public async Task HandleWorkItem(string workItemId)
        {
            using var db = await dbContextFactory.CreateDbContextAsync();
            
            var reportRun = db.Find<ReportRun>(workItemId);
            var workItem = await this.designAutomationClient.GetWorkItem(workItemId);

            reportRun.Stats = workItem.Stats;
            reportRun.Status = workItem.Status;

            await db.SaveChangesAsync();

            switch (workItem.Status)
            {
                case "success":
                    this.backgroundJobClient.Enqueue<ReportRunConsumer>(y => y.ConsumeReportRun(workItemId));
                    break;
                case "pending":
                    throw new RescheduleJobException(DateTime.Now.AddMinutes(2));
                case "inprogress":
                    throw new RescheduleJobException(DateTime.Now.AddMinutes(1));
                case "cancelled":
                case "failedLimitProcessingTime":
                case "failedDownload":
                case "failedInstructions":
                case "failedUpload":
                case "failedUploadOptional":
                    break;
            }            
        }


    }
}
