using Azure.Data.Tables;
using Consid.Domain.Entities;
using Consid.Domain.Events;
using Consid.Domain.Interfaces;
using Consid.Infrastructure.AzureTableLogger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure.Repositories
{
    internal class LogDataQueryRepository : ILogDataQueryRepository
    {

        private readonly TableClient _tableClient;
        private readonly TableServiceClient _tableServiceClient;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        
        

        public LogDataQueryRepository(ILogger<LogDataQueryRepository> logger, TableServiceClient tableServiceClient, IConfiguration configuration)
        {
            _tableServiceClient = tableServiceClient;
            _configuration = configuration;
            _tableClient = _tableServiceClient.GetTableClient(_configuration["AzureLogTableName"]);
            _logger = logger;
            _configuration = configuration;
            
        }
        
        

        public async Task<List<FetchRandomDataLog>> GetFetchRandomDataLog(DateTime dateFrom, DateTime dateTo)
        {
            try
            {   
                var fetchStatusEntities = new List<FetchRandomDataLog>();
                var fetchStatusQuery = _tableClient.QueryAsync<LogEntity>(x => 
                (x.EventId == LogEvents.FetchRandomDataFailedLogEventId || x.EventId==LogEvents.FetchRandomDataSucceededLogEventId) 
                && x.Timestamp >= dateFrom && x.Timestamp <= dateTo); 

                await foreach (var fetchStatus in fetchStatusQuery)
                {
                    fetchStatusEntities.Add(new FetchRandomDataLog(fetchStatus.Timestamp.Value.UtcDateTime, fetchStatus.EventId==LogEvents.FetchRandomDataSucceededLogEventId,fetchStatus.Message ));
                }

                return fetchStatusEntities;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch log data");
                throw;
            }

        }
    } 
}
