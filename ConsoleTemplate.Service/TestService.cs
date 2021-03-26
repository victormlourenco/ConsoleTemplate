using ConsoleTemplate.Model;
using ConsoleTemplate.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleTemplate.Service
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;
        private readonly AppSettings _config;

        public TestService(ILogger<TestService> logger,
            IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public void Run()
        {
            _logger.LogWarning($"Foo: {_config.Foo}");
        }
    }
}
