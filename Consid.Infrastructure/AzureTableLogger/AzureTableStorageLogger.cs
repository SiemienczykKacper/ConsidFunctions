using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using System.Runtime.CompilerServices;

namespace Consid.Infrastructure.AzureTableLogger;

public class AzureTableStorageLogger : ILogger
{
    private readonly TableClient _tableClient;    
    private string _blobName = null;

    public AzureTableStorageLogger(TableClient tableClient)
    {
        _tableClient = tableClient;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null; // No scope support for this logger
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        // You can adjust the log level filter as needed
        return logLevel != LogLevel.None;
    }

    public void SetAdditionalFields(string blobName)
    {        
        _blobName = blobName;
    }
        
    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var logEntity = new LogEntity(logLevel, eventId, state, exception,_blobName);        
        await _tableClient.AddEntityAsync(logEntity);        
        _blobName = null;
      
    }
}

