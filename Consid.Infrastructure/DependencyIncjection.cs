using Consid.Domain.Interfaces;
using Consid.Infrastructure.AzureTableLogger;
using Consid.Infrastructure.Repositories;
using Consid.Infrastructure.Repositories.Context;
using Consid.Infrastructure.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure;

public static class DependencyIncjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("publicapis", c =>
        {
            c.BaseAddress = new Uri("https://api.publicapis.org/");
        });

        services.AddHttpClient("openmeteo", c =>
        {
            c.BaseAddress = new Uri("https://api.open-meteo.com/v1/");
        });

        services.AddScoped<IRandomJsonDataService, RandomJsonDataService>();
        services.AddSingleton<AzureTableStorageLoggerProvider>();

        string azureWebJobsStorage = configuration["AzureWebJobsStorage"];


        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(azureWebJobsStorage);
            builder.AddTableServiceClient(azureWebJobsStorage);
        });

        services.AddLogging(builder =>
        {
            builder.AddProvider((ILoggerProvider)services.BuildServiceProvider().GetService(typeof(AzureTableStorageLoggerProvider)));
            builder.AddFilter("Azure", LogLevel.Warning);

            
        });

        services.AddScoped<WeatherHistoryDbContext>();

        services.AddScoped<ILogDataQueryRepository, LogDataQueryRepository>();
        services.AddScoped<IFetchRandomDataCommandRepository, FetchRandomDataCommandRepository>();
        services.AddScoped<IFetchRandomDataQueryRepository, FetchRandomDataQueryRepository>();
        services.AddScoped<IWeatherDataCommandRepository, WeatherDataCommandRepository>();
        services.AddScoped<IWeatherDataQueryRepository, WeatherDataQueryRepository>();
        services.AddScoped<IWeatherDataService, WeatherDataService>();
    }

}
