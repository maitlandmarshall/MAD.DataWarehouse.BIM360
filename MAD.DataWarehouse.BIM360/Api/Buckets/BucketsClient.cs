using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Buckets
{
    public class BucketsClient
    {
        private readonly HttpClient httpClient;

        public BucketsClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetSignedS3DownloadUrl(string url)
        {
            var uriBuilder = new UriBuilder(url);
            uriBuilder.Path += "/signeds3download";

            var response = await httpClient.GetStringAsync(uriBuilder.ToString());
            var responseJson = JObject.Parse(response);

            return responseJson.Value<string>("url");
        }
    }
}
