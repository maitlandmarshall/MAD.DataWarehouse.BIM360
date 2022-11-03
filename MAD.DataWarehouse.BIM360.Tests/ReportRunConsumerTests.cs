using MAD.DataWarehouse.BIM360.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAD.DataWarehouse.BIM360.Tests
{
    [TestClass]
    public class ReportRunConsumerTests
    {
        [TestMethod]
        public async Task ConsumeReportRun_Test()
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            var workItemId = "27ca80ea758348228b4c4ef457dcfe16";
            var reportRunConsumer = ServiceProviderFactory.Create().GetRequiredService<ReportRunConsumer>();

            await reportRunConsumer.ConsumeReportRun(workItemId);
        }
    }
}
