using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Consid.Application.Queries.GetMaxWindSpeedForCountryQuery;

namespace Consid.Application.Queries
{
    internal class GetMaxWindSpeedForCountryQueryHandler : IRequestHandler<GetMaxWindSpeedForCountryQuery, GetMaxWindSpeedForCountryQueryResult>
    {
        private readonly ILogger<GetMaxWindSpeedForCountryQueryHandler> _logger;
        private readonly IWeatherDataQueryRepository _repository;

        public GetMaxWindSpeedForCountryQueryHandler(ILogger<GetMaxWindSpeedForCountryQueryHandler> logger, IWeatherDataQueryRepository repository)
        {
            _logger = logger;
            _repository = repository;

        }

        public async Task<GetMaxWindSpeedForCountryQueryResult> Handle(GetMaxWindSpeedForCountryQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetMaxWindSpeedForCountry(request.Country, cancellationToken);
            return new GetMaxWindSpeedForCountryQueryResult(data);
        }
    }
}
