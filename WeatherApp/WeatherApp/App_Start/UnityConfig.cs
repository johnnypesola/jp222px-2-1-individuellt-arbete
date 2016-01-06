using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WeatherApp.Models;
using WeatherApp.Models.WebServices;

namespace WeatherApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // Automatically Adds dependency injection to all IContactRepository with ContactRepository and also other intefaces
            container.RegisterType<IPlaceWeatherRepository, PlaceWeatherRepository>();
            container.RegisterType<ISmhiWebService, SmhiWebService>();
            container.RegisterType<IPlaceWeatherService, PlaceWeatherService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}