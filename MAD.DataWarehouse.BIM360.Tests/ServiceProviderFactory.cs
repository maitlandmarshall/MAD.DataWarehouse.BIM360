using Microsoft.Extensions.DependencyInjection;

namespace MAD.DataWarehouse.BIM360.Tests
{
    internal class ServiceProviderFactory
    {
        public static IServiceProvider Create()
        {
            var serviceCollection = new ServiceCollection();
            var startup = new Startup();

            startup.ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
