using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WeatherApp.Models.DataModels;

namespace WeatherApp.Models
{
    public class PlaceWeatherRepository : BaseRepository, IPlaceWeatherRepository
    {

        #region Places

        public IEnumerable<Place> GetAllPlaces()
        {
            return _entities.Places.OrderByDescending(c => c.PlaceId).ToList();
        }

        public Place GetPlace(int placeId)
        {
            return _entities.Places.Find(placeId);
        }

        public void InsertPlace(Place place)
        {
            _entities.Places.Add(place);
        }

        public void UpdatePlace(Place place)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(place).State == EntityState.Detached)
            {
                _entities.Places.Attach(place);
            }

            _entities.Entry(place).State = EntityState.Modified;
        }

        public void DeletePlace(Place place)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(place).State == EntityState.Detached)
            {
                _entities.Places.Attach(place);
            }

            _entities.Places.Remove(place);
        }

        #endregion

        #region weather

        public IEnumerable<Weather> GetAllWeather()
        {
            return _entities.Weathers.OrderByDescending(w => w.WeatherId).ToList();
        }

        public IEnumerable<Weather> GetWeatherForPlace(int placeId)
        {
            return _entities.Weathers.Where(w => w.PlaceId == placeId).OrderBy(w => w.DateTime).ToList();
        }

        public IEnumerable<Weather> GetWeatherForPlace(int placeId, DateTime dateToGetFor)
        {
            return _entities.Weathers.Where(
                        w => w.PlaceId == placeId &&
                             w.DateTime.Year == dateToGetFor.Year &&
                             w.DateTime.Month == dateToGetFor.Month &&
                             w.DateTime.Day == dateToGetFor.Day
                    ).OrderBy(w => w.DateTime).ToList();
        }

        public Weather GetWeather(int weatherId)
        {
            return _entities.Weathers.Find(weatherId);
        }

        public IEnumerable<Weather> FindPlaceWeatherForDateTime(Weather weather)
        {
            return _entities.Weathers.Where(w => w.DateTime == weather.DateTime && w.PlaceId == weather.PlaceId).OrderByDescending(w => w.WeatherId).ToList();
        }

        public void InsertWeather(Weather weather)
        {
            _entities.Weathers.Add(weather);
        }

        public void InsertWeather(IEnumerable<Weather> weatherList)
        {
            _entities.Weathers.AddRange(weatherList);

        }

        public void UpdateWeather(Weather weather)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(weather).State == EntityState.Detached)
            {
                _entities.Weathers.Attach(weather);
            }

            _entities.Entry(weather).State = EntityState.Modified;
        }

        public void DeleteWeather(Weather weather)
        {
            // If weather object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(weather).State == EntityState.Detached)
            {
                _entities.Weathers.Attach(weather);
            }

            _entities.Weathers.Remove(weather);
        }

        public void DeleteWeatherForPlace(Place place)
        {
            _entities.Weathers.RemoveRange(_entities.Weathers.Where(w => w.PlaceId == place.PlaceId));
        }

        #endregion

        public void Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (Exception exception)
            {
                Trace.TraceInformation(exception.InnerException.Message);
            }
        }
    }
}