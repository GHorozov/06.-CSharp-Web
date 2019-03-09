namespace _01.FirstWebSite_ByTheCakeApp.Application.Controllers
{
    using System;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Response;
    using _01.FirstWebSite_ByTheCakeApp.Application.Views;
    
    public class AboutUsController
    {
        public HttpResponse GetAboutUsPage()
        {
            return new ViewResponse(HttpResponceCode.OK, new AboutUsView());
        }
    }
}
