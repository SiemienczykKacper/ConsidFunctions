using System;
using Consid.Application.Commands;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consid.Functions
{
    public class FetchRandomDataFunction
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public FetchRandomDataFunction(ILoggerFactory loggerFactory, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<FetchRandomDataFunction>();
            _mediator = mediator;
        }

        [Function("FetchRandomDataFunction")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            await _mediator.Send(new FetchRandomDataCommand());
        }
    }

   
}
