using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IPlaceWeatherRepository : IDisposable
    {
        void DeletePlace(Place place);

        void DeleteWeather(Weather weather);

        IEnumerable<Place> GetAllPlaces();

        IEnumerable<Weather> GetAllWeather();

        Place GetPlace(int placeId);

        Weather GetWeather(int weatherId);

        IEnumerable<Weather> GetWeatherForPlace(int placeId);

        IEnumerable<Weather> FindPlaceWeatherForDateTime(Weather weather);

        void DeleteWeatherForPlace(Place place);

        void InsertPlace(Place place);

        void InsertWeather(IEnumerable<Weather> weatherList);

        void InsertWeather(Weather weather);

        void UpdatePlace(Place place);

        void UpdateWeather(Weather weather);

        void Save();
    }
}
