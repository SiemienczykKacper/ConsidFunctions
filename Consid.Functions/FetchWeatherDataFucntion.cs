using System;
using Consid.Application.Commands;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consid.Functions
{
    public class FetchWeatherDataFucntion
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public FetchWeatherDataFucntion(ILoggerFactory loggerFactory, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<FetchWeatherDataFucntion>();
            _mediator = mediator;
        }

        [Function("FetchWeatherDataFucntion")]
        public async Task FetchWeatherDataRun([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            await _mediator.Send(new FetchWeatherDataCommand());
        }
    }   
}
