using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WeatherApp.Models;

namespace WeatherApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // Automatically Adds dependency injection to all IContactRepository with ContactRepository
            container.RegisterType<IPlaceRepository, PlaceRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}