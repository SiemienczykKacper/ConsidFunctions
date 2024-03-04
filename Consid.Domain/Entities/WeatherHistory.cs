using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.Entities;

public class WeatherHistory
{    public int WeatherDataId { get; set; }
    public int CityId { get; set; }
    public DateTime Time { get; set; }
    public decimal Temperature { get; set; }
    public decimal CloudCoverage { get; set; }
    public decimal WindSpeed { get; set; }
    public City City { get; set; }

    public WeatherHistory()
    {
    }

    public WeatherHistory(int cityId, DateTime time, decimal temperature, decimal cloudCoverage, decimal windSpeed)
    {
        CityId = cityId;
        Time = time;
        Temperature = temperature;
        CloudCoverage = cloudCoverage;
        WindSpeed = windSpeed;
    }

}

