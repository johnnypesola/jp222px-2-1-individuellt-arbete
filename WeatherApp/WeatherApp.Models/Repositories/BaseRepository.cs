using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models.DataModels;

namespace WeatherApp.Models
{
    public class BaseRepository : IDisposable
    {
        // Fields
        protected readonly PlaceWeatherEntities _entities = new PlaceWeatherEntities();
        bool isDisposed = false; // Flag: Has Dispose already been called?


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