using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Application.Commands
{
    public class FetchWeatherDataCommand : IRequest<FetchWeatherDataCommandResult>
    {
    }

    public class FetchWeatherDataCommandResult
    {
        public bool Succeded { get; private set; }
        public FetchWeatherDataCommandResult(bool succeded)
        {
            Succeded = succeded;
        }
    }
}
