
using Consid.Domain.Entities;
using MediatR;

namespace Consid.Application.Queries;

public class GetFetchRandomDataQuery : IRequest<RandomJsonData>
{
    public string BlobName { get; private set; }

    public GetFetchRandomDataQuery(string blobName)
    {
        BlobName = blobName;
    }
}
