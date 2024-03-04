using Consid.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Application.Queries;



public class GetFetchRandomDataLogQuery : IRequest<List<FetchRandomDataLog>>
{

    public DateTime DateFrom { get; private set; }
    public DateTime DateTo { get; private set; }

    public GetFetchRandomDataLogQuery(DateTime dateFrom, DateTime dateTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

}
