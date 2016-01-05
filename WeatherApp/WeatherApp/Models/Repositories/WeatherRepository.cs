using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeatherApp.Models.DataModels;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Models
{
    public class WeatherRepository : IWeatherRepository
    {
        // Fields
        private readonly PlaceWeatherEntities _entities = new PlaceWeatherEntities();
        bool isDisposed = false; // Flag: Has Dispose already been called?
        private ISmhiWebService _webservice;

        // Constructor for injecting repositories and services (makes testing easier). Handled in unityconfig
        public WeatherRepository(ISmhiWebService webservice)
        {
            _webservice = webservice;
        }

        // Public Methods
        public IEnumerable<Weather> GetAll()
        {
            return _entities.Weathers.OrderByDescending(c => c.WeatherId).ToList();
        }

        public Weather Get(int weatherId)
        {
            return _entities.Weathers.Find(weatherId);
        }

        public void Insert(Weather weather)
        {
            _entities.Weathers.Add(weather);
        }

        public void Update(Weather weather)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(weather).State == EntityState.Detached)
            {
                _entities.Weathers.Attach(weather);
            }

            _entities.Entry(weather).State = EntityState.Modified;
        }

        public void Delete(Weather weather)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(weather).State == EntityState.Detached)
            {
                _entities.Weathers.Attach(weather);
            }

            _entities.Weathers.Remove(weather);
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

        // Public implementation of Dispose pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool shouldDispose)
        {
            if (!isDisposed)
            {
                if (shouldDispose)
                {
                    // Free any other managed objects.
                    _entities.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}