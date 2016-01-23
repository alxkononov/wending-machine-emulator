using Owin;
using System.Web.Http;

namespace wending_machine_emulator
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
                
            );

            config.Routes.MapHttpRoute(
                 name: "Index",
                 routeTemplate: "Index",
                 defaults: new { controller = "Index"}
             );
            appBuilder.UseWebApi(config);
        }
    }
}
