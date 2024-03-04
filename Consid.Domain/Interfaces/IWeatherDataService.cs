using Consid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.Interfaces;
public interface IWeatherDataService
{
    public Task<List<WeatherHistory>> GetWeatherData(List<City> cities);
}
