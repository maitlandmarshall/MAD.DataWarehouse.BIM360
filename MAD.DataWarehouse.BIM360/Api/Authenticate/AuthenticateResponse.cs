using Newtonsoft.Json;
using System;

namespace MAD.DataWarehouse.BIM360.Api.Authenticate
{
    internal class AuthenticateResponse
    {
        private int expiresIn;

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn
        {
            get => this.expiresIn;
            set
            {
                this.expiresIn = value;
                this.ExpiresAt = DateTimeOffset.Now.AddMinutes(value - 10);
            }
        }

        public DateTimeOffset ExpiresAt { get; set; }
    }
}
