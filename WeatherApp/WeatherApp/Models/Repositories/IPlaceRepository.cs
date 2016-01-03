using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IPlaceRepository : IDisposable
    {
        IEnumerable<Place> GetAll();

        Place Get(int placeId);

        void Insert(Place place);

        void Update(Place place);

        void Delete(Place place);

        void Save();
    }
}
