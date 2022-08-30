using Hangfire;
using MAD.DataWarehouse.BIM360.Api;
using MAD.DataWarehouse.BIM360.Database;
using MAD.DataWarehouse.BIM360.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MIFCore.Hangfire;
using MIFCore.Settings;

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
        }

        public void Configure()
        {

        }

        public void PostConfigure(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.CreateRecurringJob<HubConsumer>("hubs", y => y.ConsumeHubs());
        }
    }
}