using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MAD.DataWarehouse.BIM360.Tests
{
    internal class ServiceProviderFactory
    {
        public static IServiceProvider Create()
        {
            var serviceCollection = new ServiceCollection();
            var startup = new Startup();

            var backgroundClientMoq = new Mock<IBackgroundJobClient>() { DefaultValue = DefaultValue.Mock };

            startup.ConfigureServices(serviceCollection);
            serviceCollection.AddTransient<IBackgroundJobClient>(x => backgroundClientMoq.Object);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
