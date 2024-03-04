using Consid.Domain.Entities;
using MediatR;

namespace Consid.Application.Commands
{
    public class FetchRandomDataCommand : IRequest<RandomJsonData>
    {
    }
}
