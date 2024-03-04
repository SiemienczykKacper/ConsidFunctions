using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.ValueObjects;

public class CountryMinTempMaxWindSpeed
{
    public string Country { get; set; }
    public decimal MinTemperature { get; set; }
    public decimal MaxWindSpeed { get; set; }
}
