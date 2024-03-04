using Consid.Application.Commands;
using Consid.Domain.Entities;
using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Application.Queries;

internal class GetFetchRandomDataLogQueryHandler : IRequestHandler<GetFetchRandomDataLogQuery, List<FetchRandomDataLog>>
{
    private ILogDataQueryRepository _logDataRepository;
    private ILogger<GetFetchRandomDataLogQueryHandler> _logger;

    public GetFetchRandomDataLogQueryHandler(ILogger<GetFetchRandomDataLogQueryHandler> logger, ILogDataQueryRepository logDataRepository)
    {
        _logDataRepository = logDataRepository;
        _logger = logger;
    }

    public async Task<List<FetchRandomDataLog>> Handle(GetFetchRandomDataLogQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _logDataRepository.GetFetchRandomDataLog(request.DateFrom, request.DateTo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch data from repository");
            throw;
        }
    }

}