using MAD.DataWarehouse.BIM360.Api.Buckets;
using MAD.DataWarehouse.BIM360.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Tests
{
    [TestClass]
    public class RvtModelDataConsumerTests
    {
        [TestMethod]
        public async Task EnqueueWorkItem_Test()
        {
            var projectId = "b.de3ae725-e382-47a8-bb49-fa52b29737ec";
            var folderItemId = "urn:adsk.wipprod:fs.file:vf.6oxKZDcFRQ2FWQFZ1yXuCA?version=2";

            var bucketsClient = ServiceProviderFactory.Create().GetRequiredService<ReportRunProducer>();
            await bucketsClient.EnqueueWorkItem(projectId, folderItemId);
        
        }
    }
}
