using Consid.Domain.Entities;
using MediatR;

namespace Consid.Application.Queries;

public class GetCitiesQuery : IRequest<GetCitiesQueryResult>
{
}

public class GetCitiesQueryResult
{
    public List<City> Cities { get; private set; }
    public GetCitiesQueryResult(List<City> cities)
    {
        Cities = cities;
    }
}
