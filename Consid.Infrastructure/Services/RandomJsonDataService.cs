using Consid.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure.Services
{
    public class RandomJsonDataService : IRandomJsonDataService
    {
        private readonly ILogger<RandomJsonDataService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _publicapisClient;
        public RandomJsonDataService(ILogger<RandomJsonDataService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _publicapisClient = _httpClientFactory.CreateClient("publicapis");
        }

        public async Task<string> GetRandomJsonData()
        {
            var response = await _publicapisClient.GetAsync("random");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch data. Status code: {response.StatusCode}");
            }
        }
    }
}
