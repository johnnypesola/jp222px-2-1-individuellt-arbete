using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IPlaceWeatherService
    {
        IEnumerable<Weather> GetWeatherForPlace(int placeId, out string placeName);

        IEnumerable<DayForecast> GetDayForecastsForPlace(int placeId, out string placeName);

        IEnumerable<Place> GetPlaces();
    }
}
