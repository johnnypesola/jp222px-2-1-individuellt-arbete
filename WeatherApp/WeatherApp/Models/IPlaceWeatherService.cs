using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IPlaceWeatherService
    {
        IEnumerable<Weather> GetWeatherForPlace(int placeId);
    }
}
