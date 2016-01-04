using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IWeatherRepository : IDisposable
    {
        IEnumerable<Weather> GetAll();

        Weather Get(int weatherId);

        void Insert(Weather weather);

        void Update(Weather weather);

        void Delete(Weather weather);

        void Save();
    }
}
