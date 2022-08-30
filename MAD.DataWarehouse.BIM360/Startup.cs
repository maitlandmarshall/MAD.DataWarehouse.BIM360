using Microsoft.Extensions.DependencyInjection;
using MIFCore.Settings;

namespace MAD.DataWarehouse.BIM360
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();
        }

        public void Configure()
        {

        }
    }
}