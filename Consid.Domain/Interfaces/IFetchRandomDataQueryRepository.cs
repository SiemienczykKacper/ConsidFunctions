using Consid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.Interfaces;

public interface IFetchRandomDataQueryRepository
{
    public Task<RandomJsonData> GetRandomDataByName(string name);
    

}
