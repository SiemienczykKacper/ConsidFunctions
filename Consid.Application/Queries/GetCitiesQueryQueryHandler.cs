

using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consid.Application.Queries;

public class GetCitiesQueryQueryHandler : IRequestHandler<GetCitiesQuery, GetCitiesQueryResult>
{
    private readonly ILogger<GetCitiesQueryQueryHandler> _logger;
    private IWeatherDataQueryRepository _weatherDataQueryRepository;

    public GetCitiesQueryQueryHandler(ILogger<GetCitiesQueryQueryHandler> logger, IWeatherDataQueryRepository weatherDataQueryRepository)
    {
        _logger = logger;
        _weatherDataQueryRepository = weatherDataQueryRepository;

    }

    public async Task<GetCitiesQueryResult> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities =  await _weatherDataQueryRepository.GetCities();
        return new GetCitiesQueryResult(cities);
    }
}


