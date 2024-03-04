using Consid.Domain.Entities;
using Consid.Domain.Events;
using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Consid.Application.Queries;

public  class GetFetchRandomDataQueryHandler : IRequestHandler<GetFetchRandomDataQuery, RandomJsonData>
{
    private readonly IFetchRandomDataQueryRepository _fetchRandomDataQueryRepository;
    private readonly IFetchRandomDataCommandRepository _fetchRandomDataCommandRepository;
    private readonly ILogger<GetFetchRandomDataQueryHandler> _logger;

    public GetFetchRandomDataQueryHandler(IFetchRandomDataQueryRepository fetchRandomDataQueryRepository, IFetchRandomDataCommandRepository fetchRandomDataCommandRepository, ILogger<GetFetchRandomDataQueryHandler> logger)
    {
        _fetchRandomDataQueryRepository = fetchRandomDataQueryRepository;
        _fetchRandomDataCommandRepository = fetchRandomDataCommandRepository;
        _logger = logger;
    }

    public async Task<RandomJsonData> Handle(GetFetchRandomDataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _fetchRandomDataQueryRepository.GetRandomDataByName(request.BlobName);

            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch random data");
            throw;
        }
    }
}


