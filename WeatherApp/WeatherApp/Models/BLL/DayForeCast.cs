using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class DayForecast
    {
        // Fields
        private List<Weather> _hourWeatherList = new List<Weather>();

        // Properties
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

        int PlaceId
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