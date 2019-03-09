namespace MyWebServer.Application
{
    using System;
    using MyWebServer.Server.Handlers;
    using MyWebServer.Server.Contracts;
    using MyWebServer.Application.Controllers;
    using MyWebServer.Server.Routing.Contracts;
    using _01.FirstWebSite_ByTheCakeApp.Application.Controllers;
    using _01.FirstWebSite_ByTheCakeApp.Application.Models;

    public class MainApplication : IAplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/",
                new GETHandler(httpContext => new HomeController().Index()));

            appRouteConfig
                .AddRoute("/about",
                new GETHandler(x => new AboutUsController().GetAboutUsPage()));

            appRouteConfig
                .AddRoute("/add",
                new GETHandler(x => new AddController().AddGet()));

            appRouteConfig
                .AddRoute("/add",
                new POSTHandler(x => new AddController()
                .AddPost(x.FormData["Name"], x.FormData["Price"])));

            appRouteConfig
                .AddRoute("/search",
                new GETHandler(x => new SearchController().SearchGet()));

            appRouteConfig
                .AddRoute("/search",
                new POSTHandler(x => new SearchController().SearchPost(x.FormData["Name"])));

            appRouteConfig
                .AddRoute("/calculator",
                new GETHandler(x => new CalculatorController().CalculatorGet()));

            appRouteConfig
                  .AddRoute("/calculator",
                  new POSTHandler(x => new CalculatorController()
                  .CalculatorPost(x.FormData["value1"], x.FormData["sign"], x.FormData["value2"])));

            appRouteConfig
                  .AddRoute("/login",
                  new GETHandler(x => new LoginController().LoginGet()));

            appRouteConfig
                .AddRoute("/login",
                new POSTHandler(x => new LoginController()
                .LoginPost(x.FormData["username"], x.FormData["password"])));

            //appRouteConfig
            //    .AddRoute("/register",
            //    new POSTHandler(httpContext => new UserController()
            //    .RegisterPost(httpContext.FormData["name"])));

            //appRouteConfig
            //    .AddRoute("/register",
            //    new GETHandler(httpContext => new UserController().RegisterGet()));

            //appRouteConfig
            //    .AddRoute("/user/{(?<name>[a-z]+)}",
            //        new GETHandler(httpContext => new UserController()
            //        .Details(httpContext.UrlParameters["name"])));
        }
    }
}
