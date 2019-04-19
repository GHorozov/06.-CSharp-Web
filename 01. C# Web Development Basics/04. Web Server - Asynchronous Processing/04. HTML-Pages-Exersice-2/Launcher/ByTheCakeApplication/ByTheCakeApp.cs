namespace MyCoolWebServer.ByTheCakeApplication
{
    using System;
    using MyCoolWebServer.ByTheCakeApplication.Controllers;
    using MyCoolWebServer.Server.Contracts;
    using MyCoolWebServer.Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());
        }
    }
}
