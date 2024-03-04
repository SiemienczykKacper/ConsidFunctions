using Consid.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Consid.Application.Queries.GetMaxWindSpeedForCountryQuery;

namespace Consid.Application.Queries;

public class GetMaxWindSpeedForCountryQuery : IRequest<GetMaxWindSpeedForCountryQueryResult>
{
    public string Country { get; private set; }
    public GetMaxWindSpeedForCountryQuery(string country)
    {
        Country = country;
    }

    public class GetMaxWindSpeedForCountryQueryResult
    {
        public CountryMaxWindSpeed CountryMaxWindSpeed { get; private set; }
        public GetMaxWindSpeedForCountryQueryResult(CountryMaxWindSpeed countryMaxWindSpeed)
        {
            CountryMaxWindSpeed = countryMaxWindSpeed;
        }
    }
}
