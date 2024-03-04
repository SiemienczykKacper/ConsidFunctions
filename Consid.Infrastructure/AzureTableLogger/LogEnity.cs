using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;


namespace Consid.Infrastructure.AzureTableLogger;

public class LogEntity : ITableEntity
{
    public LogLevel LogLevel { get; set; }
    public int  EventId { get; set; }

    public string EventName { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }    

    public string BlobName { get; set; }

    public LogEntity() { }

    public LogEntity(LogLevel logLevel, EventId eventId, object state, Exception exception, string blobName)
    {
        PartitionKey = DateTime.UtcNow.ToShortDateString();
        RowKey = Guid.NewGuid().ToString();
        Timestamp = DateTimeOffset.UtcNow;

        LogLevel = logLevel;
        EventId = eventId.Id;
        EventName = eventId.Name;        
        Message = state?.ToString();
        Exception = exception?.ToString();        
        BlobName = blobName;
    }
}
