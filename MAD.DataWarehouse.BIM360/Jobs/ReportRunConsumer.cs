using Hangfire;
using MAD.DataWarehouse.BIM360.Api.Buckets;
using MAD.DataWarehouse.BIM360.Database;
using MAD.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MIFCore.Hangfire;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    public class ReportRunConsumer
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly BucketsClient bucketsClient;
        private readonly AppConfig appConfig;

        public ReportRunConsumer(
            IDbContextFactory<AppDbContext> dbContextFactory,
            BucketsClient bucketsClient,
            AppConfig appConfig)
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

            // Download the output.zip from the ReportRun's ResultObjectKey
            var zipFileTempPath = Path.Combine("temp", reportRun.ResultObjectKey);
            var zipFileExtractTempPath = Path.Combine("temp", Path.GetFileNameWithoutExtension(zipFileTempPath));
            await this.DownloadZipFile(reportRun.ResultObjectKey, zipFileTempPath);
            
            // Extract the zip
            ZipFile.ExtractToDirectory(zipFileTempPath, zipFileExtractTempPath, true);

            // Open the ReportRun spreadsheet and convert the Checks sheet into ReportRunCheck
            var reportRunPath = Path.Combine(zipFileExtractTempPath, "ReportRun.xlsx");
            var reportRunChecks = this.ReadChecksSheet(workItemId, reportRunPath);
            
            foreach (var r in reportRunChecks)
            {
                db.Upsert(r);
            }

            await db.SaveChangesAsync();

            // Clean up all temp files
            Directory.Delete(zipFileExtractTempPath, true);
            File.Delete(zipFileTempPath);
        }

        private async Task DownloadZipFile(string objectKey, string destinationPath)
        {
            using var zipStream = await this.bucketsClient.DownloadObject(appConfig.BucketKey, objectKey);
            using var fs = new FileStream(destinationPath, FileMode.Create);

            await zipStream.CopyToAsync(fs);
            await fs.FlushAsync();
        }

        private IEnumerable<ReportRunCheck> ReadChecksSheet(string workItemId, string spreadsheetPath)
        {
            var workbook = new XSSFWorkbook(spreadsheetPath);

            try
            {
                var checksSheet = workbook.GetSheet("Checks");
                var totalRows = checksSheet.LastRowNum;

                // Skip the header row
                for (int i = 1; i <= totalRows; i++)
                {
                    var row = checksSheet.GetRow(i);

                    var checkId = row.GetCell(1)?.StringCellValue;
                    var name = row.GetCell(2)?.StringCellValue;
                    var description = row.GetCell(3)?.StringCellValue;
                    var result = row.GetCell(4)?.StringCellValue;
                    var failureMessage = row.GetCell(5)?.StringCellValue;
                    var resultMessage = row.GetCell(6)?.StringCellValue;
                    var error = row.GetCell(7)?.StringCellValue;
                    var countString = row.GetCell(8)?.StringCellValue;

                    int.TryParse(countString, System.Globalization.NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var count);

                    yield return new ReportRunCheck
                    {
                        WorkItemId = workItemId,
                        CheckId = checkId,
                        Count = count,
                        Description = description,
                        Error = error,
                        FailureMessage = failureMessage,
                        Name = name,
                        Result = result,
                        ResultMessage = resultMessage,
                        Index = i
                    };
                }
            }
            finally
            {
                workbook.Close();
            }
        }
    }
}
