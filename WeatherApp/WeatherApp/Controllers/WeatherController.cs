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

            try
            {
                places = _placeWeatherService.GetPlaces();

                return View(places);
            }
            catch (HandleableExeption e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Get(int placeId)
        {
            // Declare vars
            IEnumerable<DayForecast> dayForecasts;
            string placeName;

            try
            {
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
            catch (HandleableExeption e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }

            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Weather/GetDetailed?placeId=
        public ActionResult GetDetailed(int placeId, int year, int month, int day)
        {
            // Declare vars
            IEnumerable<Weather> weatherForPlace;
            string placeName;

            try
            {
                DateTime dateToGetFor = DateTime.Parse(String.Format("{0:0000}-{1:00}-{2:00}", year, month, day));

                // Get weather for place
                weatherForPlace = _placeWeatherService.GetDateWeatherForPlace(placeId, out placeName, dateToGetFor);

                // If no place was found.
                if (weatherForPlace == null && placeName == "Not found")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                // Set place info
                ViewBag.PlaceName = placeName;
                ViewBag.Date = dateToGetFor;
                ViewBag.PlaceId = placeId;

                return View(weatherForPlace);
            }
            catch (HandleableExeption e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
                
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
                 
        }
    }
}