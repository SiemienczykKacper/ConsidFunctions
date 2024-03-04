using Consid.Domain.Entities;
using Consid.Domain.Events;
using Consid.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Application.Commands;
internal class FetchRandomDataCommandHandler : IRequestHandler<FetchRandomDataCommand, RandomJsonData>
{
    private readonly ILogger<FetchRandomDataCommandHandler> _logger;
    private readonly IRandomJsonDataService _fetchDataService;
    private readonly IFetchRandomDataCommandRepository _repository;
    private string _correlationId;

    public FetchRandomDataCommandHandler(ILogger<FetchRandomDataCommandHandler> logger, IRandomJsonDataService fetchDataService, IFetchRandomDataCommandRepository repository)
    {
        _logger = logger;
        _fetchDataService = fetchDataService;
        _repository = repository;
    }

    

   


    public async Task<RandomJsonData> Handle(FetchRandomDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var correlationId = Guid.NewGuid();
            var data = await _fetchDataService.GetRandomJsonData();
            
            var name = $"Fetch_{DateTime.Now.ToString("yyyyMMddHHmmss")}.json";
            await _repository.SaveFatchedData(new RandomJsonData(data, name));
            var randomJsonData = new RandomJsonData(data, name);

            _logger.LogInformation(new EventId(999,"FetchedSucceded"), name);
            
            return randomJsonData;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(new EventId(998,"FetchFailed"), ex, null);
            throw;
        }
    }
}
