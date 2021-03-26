using ConsoleTemplate.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleTemplate
{
    public class App
    {
        private readonly ITestService _testService;
        private readonly ILogger<App> _logger;

        public App(ITestService testService,
            ILogger<App> logger)
        {
            _testService = testService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation($"Starting Application...");
            _testService.Run();
            Console.ReadKey();
        }
    }
}
