using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Models
{
    public class PlaceWeatherService : IPlaceWeatherService
    {
        private const int WEATHER_DATA_REFRESH_HOURS = 2;
        private readonly ISmhiWebService _webService;
        private readonly IPlaceWeatherRepository _repository;

        // Constructor for injecting repositories and services (makes testing easier). Handled in unityconfig
        public PlaceWeatherService(ISmhiWebService webService, IPlaceWeatherRepository placeWeatherRepository)
        {
            _webService = webService;
            _repository = placeWeatherRepository;
        }

        // Private methods
        private void AddWeatherToRepository(Place place, IEnumerable<Weather> weatherList)
        {
            // Remove old weather
            _repository.DeleteWeatherForPlace(place);

            // Add new weather
            _repository.InsertWeather(weatherList);

            _repository.Save();
        }

        // Public methods

        public IEnumerable<Place> GetPlaces()
        {
            return _repository.GetAllPlaces();
        }

        public List<DayForecast> ConvertToDayForecasts(IEnumerable<Weather> weatherList)
        {
            int lastDayNum = 0;
            int count = 0;
            var dayForecastList = new List<DayForecast>();
            DayForecast dayForecast = new DayForecast();

            foreach (Weather weather in weatherList.OrderBy(w => w.DateTime))
            {
                count++;

                // If its a new day.
                if (lastDayNum != 0 && weather.DateTime.Day != lastDayNum)
                {
                    dayForecastList.Add(dayForecast);
                    dayForecast = new DayForecast();
                }

                // Add weather to day forecast
                dayForecast.HourWeatherList.Add(weather);

                lastDayNum = weather.DateTime.Day;

                // If its the last iteration
                if (weatherList.ToList().Count == count)
                {
                    dayForecastList.Add(dayForecast);
                }
            }

            return dayForecastList;
        }

        public IEnumerable<DayForecast> GetDayForecastsForPlace(int placeId, out string placeName)
        {
            return ConvertToDayForecasts(GetDateWeatherForPlace(placeId, out placeName));
        }

        public IEnumerable<Weather> GetDateWeatherForPlace(int placeId, out string placeName, DateTime? dateToGetFor = null)
        {
            IEnumerable<Weather> weatherList = new List<Weather>(100);

            // Try to get place
            Place place = _repository.GetPlace(placeId);

            if(place == null)
            {
                placeName = "Not found";
                return null;
            }

            // Set out variable place name
            placeName = place.Name;

            // If place weather update was too long ago
            if (
                place.LastWeatherUpdate == null || 
                place.LastWeatherUpdate.GetValueOrDefault().AddHours(WEATHER_DATA_REFRESH_HOURS).CompareTo(DateTime.Now) < 0
            )
            {
                // Repository does not have new weatherdata, fetch from REST API provider.
                weatherList = _webService.GetWeatherForPlace(place);

                // Push new data to database
                AddWeatherToRepository(place, weatherList);
            }

            // Try to get weather from repository
            if(dateToGetFor == null)
            {
                weatherList = _repository.GetWeatherForPlace(placeId);
            }
            else
            {
                weatherList = _repository.GetWeatherForPlace(placeId, dateToGetFor.Value);
            }

            return weatherList;
        }
    }
}