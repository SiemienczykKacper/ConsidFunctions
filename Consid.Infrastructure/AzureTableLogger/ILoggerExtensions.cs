using Microsoft.Extensions.Logging;

namespace Consid.Infrastructure.AzureTableLogger;

public static class IloggerExtensions
{
    public static void SetAdditionalFields(this ILogger logger, string blobName)
    {
        var log = logger as AzureTableStorageLogger;
        if (log != null)
        {
            log.SetAdditionalFields(blobName);
        }
    }
}


