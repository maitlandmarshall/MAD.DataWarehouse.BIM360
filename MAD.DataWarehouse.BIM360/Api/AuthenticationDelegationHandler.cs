using MAD.DataWarehouse.BIM360.Api.Authenticate;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api
{
    internal class AuthenticationDelegationHandler : DelegatingHandler
    {
        private readonly IAuthenticateClient authenticateClient;
        private readonly AppConfig appConfig;
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        private AuthenticateResponse authenticateResponse;

        public AuthenticationDelegationHandler(IAuthenticateClient authenticateClient, AppConfig appConfig)
        {
            this.authenticateClient = authenticateClient;
            this.appConfig = appConfig;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await this.EnsureAuthenticated();

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.authenticateResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task EnsureAuthenticated()
        {
            try
            {
                await this.semaphoreSlim.WaitAsync();

                if (this.authenticateResponse is null
                    || DateTimeOffset.Now >= this.authenticateResponse.ExpiresAt)
                {
                    this.authenticateResponse = await this.authenticateClient.Authenticate(new AuthenticateRequest
                    {
                        ClientId = this.appConfig.ClientId,
                        ClientSecret = this.appConfig.ClientSecret,
                        GrantType = this.appConfig.GrantType,
                        Scope = this.appConfig.Scope
                    });
                }
            }
            finally
            {
                this.semaphoreSlim.Release();
            }
        }
    }
}
