using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.ValueObjects
{
    public class CountryMaxWindSpeed
    {
        public string Country { get; set; }
        public string CityName { get; set; }
        public decimal MaxWindSpeed { get; set; }
    }
}
