using MAD.DataWarehouse.BIM360.Api.Accounts;
using MAD.DataWarehouse.BIM360.Api.Authenticate;
using MAD.DataWarehouse.BIM360.Api.Project;
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

            serviceDescriptors.AddTransient<AuthenticationDelegationHandler>();

            serviceDescriptors
                .AddRefitClient<IAuthenticateClient>(settings)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/authentication/v1"));

            serviceDescriptors
                .AddRefitClient<IProjectClient>(settings)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/project/v1"))
                .AddHttpMessageHandler<AuthenticationDelegationHandler>();

            serviceDescriptors
                .AddRefitClient<IAccountsClient>(settings)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/hq/v1"))
                .AddHttpMessageHandler<AuthenticationDelegationHandler>();

            return serviceDescriptors;
        }
    }
}
