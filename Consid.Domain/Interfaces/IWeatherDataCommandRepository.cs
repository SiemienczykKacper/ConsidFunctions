using Consid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.Interfaces;

public interface IWeatherDataCommandRepository
{
    public Task AddWeatherData(List<WeatherHistory> weatherData, CancellationToken cancellationToken);
}
