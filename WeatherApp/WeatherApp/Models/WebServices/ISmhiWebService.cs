using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models.WebServices
{
    public interface ISmhiWebService
    {
        IEnumerable<Weather> GetWeatherForPlace(Place place);
    }
}
