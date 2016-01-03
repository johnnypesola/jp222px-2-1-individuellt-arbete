using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeatherApp.Models.DataModels;

namespace WeatherApp.Models
{
    public class PlaceRepository : IPlaceRepository
    {
        // Fields
        private readonly PlaceWeatherEntities _entities = new PlaceWeatherEntities();
        bool isDisposed = false; // Flag: Has Dispose already been called?

        // Public Methods
        public IEnumerable<Place> GetAll()
        {
            return _entities.Places.OrderByDescending(c => c.PlaceId).ToList();
        }

        public Place Get(int placeId)
        {
            return _entities.Places.Find(placeId);
        }

        public void Insert(Place place)
        {
            _entities.Places.Add(place);
        }

        public void Update(Place place)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(place).State == EntityState.Detached)
            {
                _entities.Places.Attach(place);
            }

            _entities.Entry(place).State = EntityState.Modified;
        }

        public void Delete(Place place)
        {
            // If contact object is not attatched to the DbContext object, attach it. (track it)
            if (_entities.Entry(place).State == EntityState.Detached)
            {
                _entities.Places.Attach(place);
            }

            _entities.Places.Remove(place);
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