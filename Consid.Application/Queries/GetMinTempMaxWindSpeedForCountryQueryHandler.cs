using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Consid.Application.Queries.GetMaxWindSpeedForCountryQuery;

namespace Consid.Application.Queries;

public  class GetMinTempMaxWindSpeedForCountryQueryHandler : IRequestHandler<GetMinTempMaxWindSpeedForCountryQuery, GetMinTempMaxWindSpeedForCountryQueryResult>
{
    private readonly ILogger<GetMinTempMaxWindSpeedForCountryQueryHandler> _logger;
    private readonly IWeatherDataQueryRepository _repository;

    public GetMinTempMaxWindSpeedForCountryQueryHandler(ILogger<GetMinTempMaxWindSpeedForCountryQueryHandler> logger, IWeatherDataQueryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<GetMinTempMaxWindSpeedForCountryQueryResult> Handle(GetMinTempMaxWindSpeedForCountryQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetMinTemeratureAndMaxWindSpeedForCountry(request.MaxTemperature, cancellationToken);
        return new GetMinTempMaxWindSpeedForCountryQueryResult(data);
    }
}


