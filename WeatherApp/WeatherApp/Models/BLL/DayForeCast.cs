using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class DayForecast
    {
        // Fields
        private List<Weather> _hourWeatherList = new List<Weather>();

        // Properties

        public string DayName
        {
            get
            {
                string dateStr = StartDateTime.Value.ToString("d");

                if (dateStr == DateTime.Now.AddDays(-1).ToString("d"))
                {
                    return "igår";
                }
                else if(dateStr == DateTime.Now.ToString("d"))
                {
                    return "idag";
                }
                else if(dateStr == DateTime.Now.AddDays(1).ToString("d"))
                {
                    return "imorgon";
                }
                else
                {
                    return StartDateTime.Value.ToString("dddd", CultureInfo.CreateSpecificCulture("sv-SE")); 
                }
            }
        }

        public List<Weather> HourWeatherList
        {
            get
            {
                return _hourWeatherList;
            }
        }

        public DateTime? StartDateTime
        {
            get
            {
                if (HourWeatherList.Count > 0)
                {
                    return HourWeatherList[0].DateTime;
                }

                return null;
            }
        }

        public int PlaceId
        {
            get
            {
                return HourWeatherList.Count > 0 ? HourWeatherList[0].PlaceId : 0;
            }
        }

        // Methods
        public Weather GetDayWeather() {

            var resultWeather = new Weather
            {
                // Get average values from all weather hourly forecasts
                Humidity = (byte)HourWeatherList.Average(w => w.Humidity),
                Temperature = HourWeatherList.Average(w => w.Temperature),
                ThunderStormProbability = (byte)HourWeatherList.Average(w => w.ThunderStormProbability),
                TotalCloudCover = (byte)HourWeatherList.Average(w => w.TotalCloudCover),
                WindDirection = (int)HourWeatherList.Average(w => w.WindDirection),
                WindSpeed = HourWeatherList.Average(w => w.WindSpeed),
                PrecipitationIntensity = HourWeatherList.Average(w => w.PrecipitationIntensity),

                // Get the most ocurring weather type
                Precipitation = HourWeatherList.GroupBy(w => w).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First().Precipitation,

                // Other good to have values
                PlaceId = PlaceId,
                WeatherId = 0,
                DateTime = StartDateTime.Value
            };

            return resultWeather;
        }
    }
}