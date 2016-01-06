﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Models
{
    public class PlaceWeatherService : IPlaceWeatherService
    {
        private const int WEATHER_DATA_REFRESH_HOURS = 1;
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

        private void AddWeatherToRepository(Place place, IEnumerable<Weather> weatherList)
        {
            // Remove old weather
            _repository.DeleteWeatherForPlace(place);

            // Add new weather
            _repository.InsertWeather(weatherList);

            _repository.Save();
        }

        // Public methods
        public IEnumerable<Weather> GetWeatherForPlace(int placeId, out string placeName)
        {
            IEnumerable<Weather> weatherList = new List<Weather>(100);

            // Get place
            Place place = _repository.GetPlace(placeId);

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
            weatherList = _repository.GetWeatherForPlace(placeId);

            return weatherList;
        }
    }
}