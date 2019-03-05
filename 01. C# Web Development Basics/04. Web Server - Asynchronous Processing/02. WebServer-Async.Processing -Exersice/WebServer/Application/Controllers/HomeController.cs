namespace WebServer.Application.Controllers
{
    using System;
    using WebServer.Application.Views;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Response;

    public class HomeController
    {
        public HttpResponse Index()
        {
            return new ViewResponse(HttpResponceCode.OK, new HomeIndexView());
        }
    }
}
