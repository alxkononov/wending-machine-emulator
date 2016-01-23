using Owin;
using System.Web.Http;
using System.Web;

namespace wending_machine_emulator
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                 name: "Index",
                 routeTemplate: "Index",
                 defaults: new { controller = "Index"}
             );
            appBuilder.UseWebApi(config);
        }
    }
}
