using Newtonsoft.Json;

namespace MAD.DataWarehouse.BIM360.Api.Authenticate
{
    internal class AuthenticateRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
