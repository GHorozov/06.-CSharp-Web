namespace _MyWebServer
{
    using System;
    using MyWebServer.Application;
    using MyWebServer.Server;
    using MyWebServer.Server.Contracts;
    using MyWebServer.Server.Routing;
    using MyWebServer.Server.Routing.Contracts;

    public class Launcher
    {
        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            IAplication app = new MainApplication();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            app.Start(routeConfig);

            var webServer = new WebServerClass(1337, routeConfig);
            webServer.Run();
        }
    }
}
