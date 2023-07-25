using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Buckets;
using MAD.DataWarehouse.BIM360.Api.Data;
using MAD.DataWarehouse.BIM360.Api.DesignAutomation;
using MAD.DataWarehouse.BIM360.Database;
using Microsoft.EntityFrameworkCore;
using MIFCore.Hangfire;
using System;
using System.Linq;
using System.Net.Http;
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
            using var db = await this.dbContextFactory.CreateDbContextAsync();

            var versions = db.Set<FolderItemDerivative>()
                .Where(y => y.RVTVersion == "2018")
                .Where(y => y.Project.Status == "active")
                .AsAsyncEnumerable();

            await foreach (var v in versions)
            {
                this.backgroundJobClient.Enqueue<ReportRunProducer>(y => y.EnqueueWorkItem(v.ProjectId, v.FolderItemId));
            }
        }

        public async Task EnqueueWorkItem(string projectId, string folderItemId)
        {
            projectId = this.PrefixWithB(projectId);

            using var db = await this.dbContextFactory.CreateDbContextAsync();
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
            using var db = await this.dbContextFactory.CreateDbContextAsync();

            var reportRun = db.Find<ReportRun>(workItemId);
            var workItem = await this.designAutomationClient.GetWorkItem(workItemId);

            reportRun.Stats = workItem.Stats;
            reportRun.Status = workItem.Status;

            if (string.IsNullOrWhiteSpace(workItem.ReportUrl) == false)
            {
                using var client = new HttpClient();
                reportRun.Report = await client.GetStringAsync(workItem.ReportUrl);
            }

            await db.SaveChangesAsync();

            switch (workItem.Status)
            {
                case "success":
                    this.backgroundJobClient.Enqueue<ReportRunConsumer>(y => y.ConsumeReportRun(workItemId));
                    break;
                case "pending":
                    if (BackgroundJobContext.Current.GetJobParameter<int>("RetryCount") > 7) BackgroundJobContext.Current.BackgroundJob.SetJobParameter("RetryCount", 7);
                    throw new DesignAutomationStateException($"Waiting. Job is in state: {workItem.Status}.");
                case "inprogress":
                    if (BackgroundJobContext.Current.GetJobParameter<int>("RetryCount") > 7) BackgroundJobContext.Current.BackgroundJob.SetJobParameter("RetryCount", 7);
                    throw new DesignAutomationStateException($"Waiting. Job is in state: {workItem.Status}.");
                case "cancelled":
                case "failedLimitProcessingTime":
                case "failedDownload":
                case "failedInstructions":
                case "failedUpload":
                case "failedUploadOptional":
                    this.backgroundJobClient.Enqueue<ReportRunConsumer>(y => y.ConsumeReportRun(workItemId));
                    break;
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
