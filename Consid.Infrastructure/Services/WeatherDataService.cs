using Consid.Domain.Entities;
using Consid.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Consid.Infrastructure.Services;
public class WeatherDataService : IWeatherDataService
{
    private readonly ILogger<WeatherDataService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _openMeteoClient;



    public WeatherDataService(ILogger<WeatherDataService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _openMeteoClient = _httpClientFactory.CreateClient("openmeteo");
    }

    private class WeatherParamsDTO
    {
        public decimal[] latitude { get; set; }
        public decimal[] longitude { get; set; }
        public string[]  current { get; set; }
    }
    


    public async Task<List<WeatherHistory>> GetWeatherData(List<City> cities)
    {

        CultureInfo culture = new CultureInfo("en-US");
        culture.NumberFormat.NumberDecimalSeparator = ".";
        var fetchedData = new List<WeatherHistory>();
        foreach (var city in cities)
        {
            try
            { 
                var query = $"forecast?latitude={city.Latitude.ToString(culture)}&longitude={city.Longitude.ToString(culture)}&current=temperature_2m,cloud_cover,wind_speed_10m,wind_direction_10m";
                var weatherData = await _openMeteoClient.GetAsync(query);
                if (!weatherData.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to fetch data. Status code: {weatherData.StatusCode}");
                }
                else
                {
                    var jsonString = await weatherData.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonString);
                    fetchedData.Add(new WeatherHistory
                    {
                        CityId = city.CityId,
                        Temperature = data.current.temperature_2m,
                        CloudCoverage = data.current.cloud_cover,
                        WindSpeed = data.current.wind_speed_10m,
                        Time = DateTime.Now
                    }); 
                }                
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Failed to fetch data for city {city.CityName}");
            }
        }
        return fetchedData;

    }
}

