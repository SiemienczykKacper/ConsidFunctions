using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Consid.Application.Commands;

public class FetchWeatherDataCommandHandler : IRequestHandler<FetchWeatherDataCommand, FetchWeatherDataCommandResult>
{
    private ILogger<FetchWeatherDataCommandHandler> _logger;
    private readonly IWeatherDataCommandRepository _weatherConmmandRepository;
    private readonly IWeatherDataQueryRepository _weatherQuerydRepository;
    private readonly IWeatherDataService _weatherDataService;
    IServiceScopeFactory _serviceScopeFactory;


    public FetchWeatherDataCommandHandler(ILogger<FetchWeatherDataCommandHandler> logger, IWeatherDataCommandRepository weatherConmmandRepository,
        IWeatherDataQueryRepository weatherQuerydRepository,
        IWeatherDataService weatherDataService,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _weatherConmmandRepository = weatherConmmandRepository;
        _weatherQuerydRepository = weatherQuerydRepository;
        _weatherDataService = weatherDataService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<FetchWeatherDataCommandResult> Handle(FetchWeatherDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cities = await _weatherQuerydRepository.GetCities();
            var weatherData = await _weatherDataService.GetWeatherData(cities);
            await _weatherConmmandRepository.AddWeatherData(weatherData, cancellationToken);

            _logger.LogInformation("Weather data fetched and saved");
            return new FetchWeatherDataCommandResult(true);            
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Failed to fetch weather data");
            return new FetchWeatherDataCommandResult(false);
        }
    }
}

