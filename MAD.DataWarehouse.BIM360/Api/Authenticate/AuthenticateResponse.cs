using Newtonsoft.Json;

namespace MAD.DataWarehouse.BIM360.Api.Authenticate
{
    internal class AuthenticateResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
