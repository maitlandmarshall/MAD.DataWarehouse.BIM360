﻿using Hangfire;
using MAD.DataWarehouse.BIM360.Api;
using MAD.DataWarehouse.BIM360.Database;
using MAD.DataWarehouse.BIM360.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MIFCore.Hangfire;
using MIFCore.Settings;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MAD.DataWarehouse.BIM360.Tests")]
namespace MAD.DataWarehouse.BIM360
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();
            serviceDescriptors.AddApi();

            serviceDescriptors.AddDbContextFactory<AppDbContext>((svc, options) => options.UseSqlServer(svc.GetRequiredService<AppConfig>().ConnectionString));

            serviceDescriptors.AddScoped<HubConsumer>();
            serviceDescriptors.AddScoped<ProjectConsumer>();
            serviceDescriptors.AddScoped<FolderConsumer>();
            serviceDescriptors.AddScoped<DerivativesConsumer>();
            serviceDescriptors.AddScoped<ReportRunProducer>();
            serviceDescriptors.AddScoped<ReportRunConsumer>();
        }

        public void Configure()
        {

        }

        public void PostConfigure(IRecurringJobManager recurringJobManager, IDbContextFactory<AppDbContext> dbContextFactory)
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            using var db = dbContextFactory.CreateDbContext();
            db.Database.Migrate();

            recurringJobManager.CreateRecurringJob<HubConsumer>("hubs", y => y.ConsumeHubs());
            recurringJobManager.CreateRecurringJob<DerivativesConsumer>("derivatives", y => y.EnqueueDerivativesForConsumer());
            recurringJobManager.CreateRecurringJob<ReportRunProducer>("reportrunproducer", y => y.EnqueueVersionsForWorkItem(), "0 0 * * 0");
        }
    }
}