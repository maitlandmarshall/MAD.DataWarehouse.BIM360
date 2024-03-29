﻿using MAD.DataWarehouse.BIM360.Api.Accounts;
using MAD.DataWarehouse.BIM360.Api.Authenticate;
using MAD.DataWarehouse.BIM360.Api.Buckets;
using MAD.DataWarehouse.BIM360.Api.Data;
using MAD.DataWarehouse.BIM360.Api.DesignAutomation;
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
                 .AddRefitClient<IDataClient>(settings)
                 .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/data/v1"))
                 .AddHttpMessageHandler<AuthenticationDelegationHandler>();

            serviceDescriptors
                .AddRefitClient<IAccountsClient>(settings)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/hq/v1"))
                .AddHttpMessageHandler<AuthenticationDelegationHandler>();

            serviceDescriptors
                .AddRefitClient<IDesignAutomationClient>(settings)
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://developer.api.autodesk.com/da/us-east/v3"))
                .AddHttpMessageHandler<AuthenticationDelegationHandler>();

            serviceDescriptors
                .AddHttpClient<BucketsClient>()
                .AddHttpMessageHandler<AuthenticationDelegationHandler>();

            serviceDescriptors
                .AddHttpClient(string.Empty)
                .AddHttpMessageHandler<AuthenticationDelegationHandler>();


            return serviceDescriptors;
        }
    }
}
