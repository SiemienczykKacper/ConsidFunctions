using Consid.Domain.Entities;
using Consid.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.Interfaces
{
    public interface IWeatherDataQueryRepository
    {
        public Task<List<City>> GetCities();

        public Task<CountryMaxWindSpeed> GetMaxWindSpeedForCountry(string country, CancellationToken cancellationToken);

        public Task<List<CountryMinTempMaxWindSpeed>> GetMinTemeratureAndMaxWindSpeedForCountry(decimal maxTemperature, CancellationToken cancellationToken);
    }
}
