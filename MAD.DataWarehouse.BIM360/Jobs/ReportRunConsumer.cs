using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Buckets;
using MAD.DataWarehouse.BIM360.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MIFCore.Hangfire;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    public class ReportRunConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly BucketsClient bucketsClient;
        private readonly AppConfig appConfig;

        public ReportRunConsumer(IDbContextFactory<AppDbContext> dbContextFactory, BucketsClient bucketsClient, AppConfig appConfig)
        {
            this.dbContextFactory = dbContextFactory;
            this.bucketsClient = bucketsClient;
            this.appConfig = appConfig;
        }

        [AutomaticRetry(Attempts = 10)]
        public async Task ConsumeReportRun(string workItemId)
        {
            using var db = await dbContextFactory.CreateDbContextAsync();
            var reportRun = db.Find<ReportRun>(workItemId);

            if (reportRun.Status != "success")
            {
                BackgroundJobContext.Current.BackgroundJob.SetJobParameter("RetryCount", 10);
                throw new ReportRunStatusException($"ReportRun (workItemId: {workItemId}) is not in a success state.");
            }

            var resultDownloadUri = await this.bucketsClient.GetSignedDownloadUrl(appConfig.BucketKey, reportRun.ResultObjectKey);
            
        }
    }
}
