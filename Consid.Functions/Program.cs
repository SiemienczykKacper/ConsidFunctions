using Consid.Application;
using Consid.Domain.Interfaces;
using Consid.Infrastructure;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;


internal class Program
{
    private  static void Main(string[] args)
    {

        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()            
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath);
                config.AddEnvironmentVariables();
                config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);


            })
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;

                services.AddHttpContextAccessor();
                services.AddApplication();
                services.AddInfrastructure(configuration);
                services.AddSingleton<IConfiguration>(configuration);
            })
            .ConfigureOpenApi()
            .Build();

        host.Run();
    }
}

