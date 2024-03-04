using Consid.Domain.Entities;
using Consid.Domain.Interfaces;
using Consid.Domain.ValueObjects;
using Consid.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure.Repositories
{
    internal class WeatherDataQueryRepository : IWeatherDataQueryRepository
    {
        private readonly ILogger<WeatherDataQueryRepository> _logger;
        WeatherHistoryDbContext _dbContext;
        public WeatherDataQueryRepository(ILogger<WeatherDataQueryRepository> logger, WeatherHistoryDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<List<City>> GetCities()
        {
            return await _dbContext.Cities.AsNoTracking().ToListAsync();
        }

        public async Task<List<WeatherHistory>> GetWeatherData(List<City> cities)
        {            
            var cityIds = cities.Select(x => x.CityId).ToList();
            return await _dbContext.WeatherHistories.AsNoTracking().Where(x => cityIds.Contains(x.CityId)).AsNoTracking().ToListAsync();            
        }

        public async Task<List<CountryMinTempMaxWindSpeed>> GetMinTemeratureAndMaxWindSpeedForCountry(decimal maxTemperature, CancellationToken cancellationToken)
        {
           return await _dbContext.Set<CountryMinTempMaxWindSpeed>()
            .FromSqlRaw("EXEC GetMinTemeratureAndMaxWindSpeedForCountry {0}", maxTemperature)
            .ToListAsync(cancellationToken);

        }
        public async Task<CountryMaxWindSpeed> GetMaxWindSpeedForCountry (string country, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Set<CountryMaxWindSpeed>()
             .FromSqlRaw("EXEC GetMaxWindSpeedForCountry {0}", country)
             .ToListAsync(cancellationToken);

            return data.FirstOrDefault();
        }
    }
}
