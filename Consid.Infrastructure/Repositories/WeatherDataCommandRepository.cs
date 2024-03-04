using Consid.Domain.Entities;
using Consid.Domain.Interfaces;
using Consid.Infrastructure.Repositories.Context;
using Microsoft.Extensions.Logging;


namespace Consid.Infrastructure.Repositories;

internal class WeatherDataCommandRepository : IWeatherDataCommandRepository
{
    private readonly ILogger<WeatherDataCommandRepository> _logger;
    WeatherHistoryDbContext _dbContext;
    public WeatherDataCommandRepository(ILogger<WeatherDataCommandRepository> logger, WeatherHistoryDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;        
    }

    public Task AddWeatherData(List<WeatherHistory> weatherData, CancellationToken cancellationToken)
    {
        foreach (var data in weatherData)
        {
            _dbContext.WeatherHistories.Add(data);
        }
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
