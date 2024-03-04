using System;
using Microsoft.Extensions.Logging;
using Consid.Infrastructure.AzureTableLogger;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

namespace Consid.Infrastructure.AzureTableLogger;
public class AzureTableStorageLoggerProvider : ILoggerProvider
{
    
    
    private readonly TableClient _tableClient;
    private readonly TableServiceClient _tableServiceClient;
    private readonly IConfiguration _configuration;

    public AzureTableStorageLoggerProvider(TableServiceClient tableServiceClient, IConfiguration configuration)
    {   
        _tableServiceClient = tableServiceClient;
        _tableClient = _tableServiceClient.GetTableClient(configuration["AzureLogTableName"]);
        _tableClient.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        _configuration = configuration;
        
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new AzureTableStorageLogger(_tableServiceClient.GetTableClient(_configuration["AzureLogTableName"]));
    }

    public void Dispose()
    {
      
    }
}
