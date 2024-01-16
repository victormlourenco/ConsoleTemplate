using System.Net;
using System.Net.Http;
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
        private readonly HttpClient _httpClient;

        public TestService(ILogger<TestService> logger,
            IOptions<AppSettings> config, HttpClient httpClient)
        {
            _logger = logger;
            _config = config.Value;
            _httpClient = httpClient;
        }

        public async void Run()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_config.Url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"{_config.Url} is UP");
            }
            _logger.LogWarning($"Foo: {_config.Foo}");
        }
    }
}
