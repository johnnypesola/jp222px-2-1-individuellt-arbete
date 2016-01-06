using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Models
{
    public class PlaceWeatherService : IPlaceWeatherService
    {
        private readonly ISmhiWebService _webService;
        private readonly IPlaceWeatherRepository _repository;

        // Constructor for injecting repositories and services (makes testing easier). Handled in unityconfig
        public PlaceWeatherService(ISmhiWebService webService, IPlaceWeatherRepository placeRepository)
        {
            _webService = webService;
            _repository = placeRepository;
        }

        // Private methods
        private bool HasWeatherForNumberOfDays(IEnumerable<Weather> weatherList, int days)
        {
            DateTime currentDate = new DateTime();
            bool foundMatch;
            
            while(days > 0)
            {
                foundMatch = false;

                foreach (Weather weather in weatherList)
                {
                    if(
                        weather.DateTime.Year == currentDate.Year &&
                        weather.DateTime.Month == currentDate.Month &&
                        weather.DateTime.Day == currentDate.Day
                    )
                    {
                        foundMatch = true;
                    }
                }

                // Abort if there is no weather for this day.
                if (!foundMatch)
                {
                    return false;
                }

                currentDate.AddDays(1);
                days--;
            }

            return true;
        }

        private void AddOnlyNewWeatherToDatabase(Place place, IEnumerable<Weather> weatherList)
        {
            IEnumerable<Weather> weatherListToAdd;

            // Filter items that allready are in the repository
            //weatherListToAdd = weatherList.Except(place.Weather);

            place.Weather = weatherList.ToList();

            foreach (Weather weather in weatherList)
            {
                weather.Place = place;
            }

            _repository.Save();

            // Save new weather objects
            /*
            if(weatherListToAdd.ToList().Count > 0)
            {
                _repository.InsertWeather(weatherListToAdd);
                _repository.Save();
            }*/

        }

        // Public methods
        public IEnumerable<Weather> GetWeatherForPlace(int placeId)
        {
            IEnumerable<Weather> weatherList = new List<Weather>(100);

            // Get place
            Place place = _repository.GetPlace(placeId);

            // Try to get weather from repository
            weatherList = _repository.GetWeatherForPlace(placeId);

            if (weatherList.ToList().Count > 0)
            {
                if (HasWeatherForNumberOfDays(weatherList, 5))
                {
                    return weatherList;
                }
            }
                      

            // Repository does not have enough weatherdata, fetch from REST API provider.
            weatherList = _webService.GetWeatherForPlace(place);

            // Push new data to database
            AddOnlyNewWeatherToDatabase(place, weatherList);

            return weatherList;
        }
    }
}