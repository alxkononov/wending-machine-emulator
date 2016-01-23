using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.IO;



namespace wending_machine_emulator.Controllers
{
    public class IndexController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var index = File.ReadAllText("Views/Index.html");
            var response = new HttpResponseMessage {Content = new StringContent(index) };           
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
