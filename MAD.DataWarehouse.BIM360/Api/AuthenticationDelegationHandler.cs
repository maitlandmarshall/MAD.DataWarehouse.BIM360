﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api
{
    internal class AuthenticationDelegationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}