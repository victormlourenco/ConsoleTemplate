using ConsoleTemplate.Model;
using ConsoleTemplate.Service;
using ConsoleTemplate.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ConsoleTemplate.Crosscutting.IoC
{
    public static class ContentRootBootstrapper
    {
        public static void RegisterDependencies(this IServiceCollection serviceCollection)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            serviceCollection.AddSingleton(loggerFactory);
            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
            });

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));

            // add http client
            serviceCollection.AddHttpClient<ITestService, TestService>(client =>
            {
                client.BaseAddress = new Uri(configuration["BaseUrl"]);
                client.DefaultRequestHeaders.Add("Content-Type", configuration["application/json"]);
                client.DefaultRequestHeaders.Add("Accept-Encoding", configuration["gzip, deflate, br"]);
            }).ConfigurePrimaryHttpMessageHandler(messageHandler =>
            {
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                }
                return handler;
            });

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();
        }
    }
}
