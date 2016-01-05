using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace WeatherApp.Models.WebServices
{
    public class SmhiWebService : ISmhiWebService
    {
        public IEnumerable<Weather> GetWeatherForPlace(Place place)
        {
            List<Weather> WeathersForPlace = new List<Weather>(20);
            string rawJson;
            string uriString = String.Format("http://opendata-download-metfcst.smhi.se/api/category/pmp1.5g/version/1/geopoint/lat/{0}/lon/{1}/data.json", place.UrlFriendlyLatitude , place.UrlFriendlyLongitude);

            // Fetch JSON from web
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriString);

            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    rawJson = reader.ReadToEnd();
                }
            }

            // Load JSON from temp file.
            /*
            using(var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/smhi_response.json")))
            {
                rawJson = reader.ReadToEnd();
            }
            */

            // Parse json to object
            dynamic parsedObj = JObject.Parse(rawJson);

            foreach (var weatherData in parsedObj.timeseries)
            {

                // Create and add weather to list
                WeathersForPlace.Add(
                    new Weather
                    {
                        PlaceId = place.PlaceId,
                        DateTime = (DateTime)weatherData["validTime"],
                        Temperature = (Decimal)weatherData["t"],
                        WindDirection = (Int32)weatherData["wd"],
                        WindSpeed = (Decimal)weatherData["ws"],
                        Humidity = (Byte)weatherData["r"],
                        Precipitation = (Byte)weatherData["pcat"],
                        TotalCloudCover = (Byte)weatherData["tcc"],
                        ThunderStormProbability = (Byte)weatherData["tstm"]
                    }
                );

            }

            return WeathersForPlace.AsEnumerable();
        }
    }
}