using Consid.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Application.Queries;

public class GetMinTempMaxWindSpeedForCountryQuery : IRequest<GetMinTempMaxWindSpeedForCountryQueryResult>
{
    public decimal MaxTemperature { get; private set; }
    public GetMinTempMaxWindSpeedForCountryQuery(decimal maxTemperature)
    {
        MaxTemperature = maxTemperature;
    }
}

public class GetMinTempMaxWindSpeedForCountryQueryResult
{
    public List<CountryMinTempMaxWindSpeed> CountryMinTempMaxWindSpeeds { get; private set; }
    public GetMinTempMaxWindSpeedForCountryQueryResult(List<CountryMinTempMaxWindSpeed> countryMinTempMaxWindSpeeds)
    {
        CountryMinTempMaxWindSpeeds = countryMinTempMaxWindSpeeds;
    }
}

