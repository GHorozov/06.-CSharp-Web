namespace WebServer.Application
{
    using System;
    using WebServer.Application.Controllers;
    using WebServer.Server.Contracts;
    using WebServer.Server.Handlers;
    using WebServer.Server.Routing.Contracts;

    public class MainApplication : IAplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/",
                new GETHandler(httpContext => new HomeController().Index()));

            appRouteConfig
                .AddRoute("/register",
                new POSTHandler(httpContext => new UserController()
                .RegisterPost(httpContext.FormData["name"])));

            appRouteConfig
                .AddRoute("/register",
                new GETHandler(httpContext => new UserController().RegisterGet()));

            appRouteConfig
                .AddRoute("/user/{(?<name>[a-z]+)}",
                    new GETHandler(httpContext => new UserController()
                    .Details(httpContext.UrlParameters["name"])));
        }
    }
}
