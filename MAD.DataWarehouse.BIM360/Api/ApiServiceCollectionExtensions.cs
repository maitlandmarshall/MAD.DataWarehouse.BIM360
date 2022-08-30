using MAD.DataWarehouse.BIM360.Api.Authenticate;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace MAD.DataWarehouse.BIM360.Api
{
    internal static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection serviceDescriptors)
        {
            var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());

            serviceDescriptors
                .AddRefitClient<IAuthenticateClient>(settings)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/authentication/v1"));

            return serviceDescriptors;
        }
    }
}
