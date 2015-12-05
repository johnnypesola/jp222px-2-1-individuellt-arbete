using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherApp.Startup))]
namespace WeatherApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
