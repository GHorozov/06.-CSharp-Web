namespace MyWebServer.Application.Controllers
{
    using System;
    using MyWebServer.Application.Views;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Response;

    public class HomeController
    {
        public HttpResponse Index()
        {
            return new ViewResponse(HttpResponceCode.OK, new HomeIndexView());
        }
    }
}
