using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        // Fields
        private readonly IPlaceWeatherService _placeWeatherService;

        // Constructor for injecting repositories and services (makes testing easier). Handled in unityconfig
        public WeatherController(
            IPlaceWeatherService placeWeatherService
        )
        {
            _placeWeatherService = placeWeatherService;
        }

        // GET: Weather
        public ActionResult Index()
        {
            IEnumerable<Place> places;

            places = _placeWeatherService.GetPlaces();

            return View(places);
        }

        public ActionResult Get(int placeId)
        {
            // Declare vars
            IEnumerable<DayForecast> dayForecasts;
            string placeName;

            // Get day forecasts for place
            dayForecasts = _placeWeatherService.GetDayForecastsForPlace(placeId, out placeName);

            // If no place was found.
            if (dayForecasts == null && placeName == "Not found")
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            // Set place info
            ViewBag.PlaceName = placeName;

            return View(dayForecasts);
        }

        // GET: Weather/GetDetailed?placeId=
        public ActionResult GetDetailed(int placeId)
        {
            // Declare vars
            IEnumerable<Weather> weatherForPlace;
            string placeName;

            // Get weather for place
            weatherForPlace = _placeWeatherService.GetWeatherForPlace(placeId, out placeName);

            // If no place was found.
            if (weatherForPlace == null && placeName == "Not found")
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            // Set place info
            ViewBag.PlaceName = placeName;

            return View(weatherForPlace);
        }
    }
}