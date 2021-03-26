using ConsoleTemplate.Model;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTemplate.Crosscutting.IoC;

namespace ConsoleTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // run app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // register dependencies
            serviceCollection.RegisterDependencies();

            // add app
            serviceCollection.AddTransient<App>();
        }
    }
}
