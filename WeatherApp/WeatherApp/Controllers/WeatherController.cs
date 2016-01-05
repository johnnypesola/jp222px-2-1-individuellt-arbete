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
        private ISmhiWebService _webservice;
        private IPlaceRepository _placeRepository;
        private IWeatherRepository _weatherRepository;

        // Constructor for injecting repositories and services (makes testing easier). Handled in unityconfig
        public WeatherController(
            ISmhiWebService webservice,
            IPlaceRepository placeRepository,
            IWeatherRepository weatherRepository
        )
        {
            _webservice = webservice;
            _placeRepository = placeRepository;
            _weatherRepository = weatherRepository;
        }

        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(int placeId)
        {
            // Declare vars
            Place place;
            IEnumerable<Weather> weatherForPlace;

            // Get place
            place = _placeRepository.Get(placeId);

            // Set place info
            ViewBag.PlaceName = place.Name;

            // Get weather for place
            weatherForPlace = _webservice.GetWeatherForPlace(place).AsEnumerable();

            return View(weatherForPlace);
        }
    }
}