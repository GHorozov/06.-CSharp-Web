namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.Server.Http.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse("index");
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse("about");
        }
    }
}
