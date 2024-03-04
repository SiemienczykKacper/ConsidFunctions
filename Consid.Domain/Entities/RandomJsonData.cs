using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Consid.Domain.Entities
{
    public class RandomJsonData 
    {
        public string Data { get; private set; }
        public string Name { get; private set; }

        public RandomJsonData(string data, string name)
        {
            Data = data;
            Name = name;            
        }
    }
}
