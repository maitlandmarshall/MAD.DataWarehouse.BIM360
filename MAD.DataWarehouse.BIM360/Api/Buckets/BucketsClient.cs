using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
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

            if (string.IsNullOrWhiteSpace(uriBuilder.Query))
            {
                uriBuilder.Query = "minutesExpiration=60";
            }
            else
            {
                uriBuilder.Query += "&minutesExpiration=60";
            }

            var response = await httpClient.GetStringAsync(uriBuilder.ToString());
            var responseJson = JObject.Parse(response);

            return responseJson.Value<string>("url");
        }

        public async Task<string> GetSignedUploadUrl(string bucketKey, string objectKey)
        {
            var uriBuilder = new UriBuilder("https://developer.api.autodesk.com/oss/v2/buckets");
            uriBuilder.Path += $"/{bucketKey}/objects/{objectKey}/signed";
            uriBuilder.Query = "access=write";

            var response = await httpClient.PostAsync(uriBuilder.ToString(), new StringContent("{}", System.Text.Encoding.UTF8, "application/json"));
            var responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());

            return responseJson.Value<string>("signedUrl");
        }

        public async Task<string> GetSignedDownloadUrl(string bucketKey, string objectKey)
        {
            var uriBuilder = new UriBuilder("https://developer.api.autodesk.com/oss/v2/buckets");
            uriBuilder.Path += $"/{bucketKey}/objects/{objectKey}/signed";
            uriBuilder.Query = "access=read";

            var response = await httpClient.GetStringAsync(uriBuilder.ToString());
            var responseJson = JObject.Parse(response);
            
            return responseJson.Value<string>("signedUrl");
        }

        public async Task<Stream> DownloadObject(string bucketKey, string objectKey)
        {
            var uriBuilder = new UriBuilder("https://developer.api.autodesk.com/oss/v2/buckets");
            uriBuilder.Path += $"/{bucketKey}/objects/{objectKey}";

            var response = await httpClient.GetStreamAsync(uriBuilder.ToString());
            return response;
        }
    }
}
