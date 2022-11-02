using MAD.DataWarehouse.BIM360.Api.Buckets;
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
    public class BucketsClientTests
    {
        [TestMethod]
        public async Task GetSignedS3DownloadUrl_TestWithStorageHref()
        {
            var storageHref = "https://developer.api.autodesk.com/oss/v2/buckets/wip.dm.prod/objects/764c4868-34de-4d9a-8184-0fce2fb0ac92.rvt?scopes=b360project.2d8a2470-cc6b-41f4-ae5e-59786ba41203,O2tenant.4921135";
            var bucketsClient = ServiceProviderFactory.Create().GetRequiredService<BucketsClient>();
            var downloadUrl = await bucketsClient.GetSignedS3DownloadUrl(storageHref);
        }
    }
}
