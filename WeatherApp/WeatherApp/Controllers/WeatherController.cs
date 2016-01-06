using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        // Fields
        private readonly IPlaceWeatherService _weatherService;

        // Constructor for injecting repositories and services (makes testing easier). Handled in unityconfig
        public WeatherController(
            IPlaceWeatherService weatherService
        )
        {
            _weatherService = weatherService;
        }

        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(int placeId)
        {
            // Declare vars
            IEnumerable<Weather> weatherForPlace;
            string placeName;

            // Get weather for place
            weatherForPlace = _weatherService.GetWeatherForPlace(placeId, out placeName);

            // Set place info
            ViewBag.PlaceName = placeName;

            return View(weatherForPlace);
        }
    }
}