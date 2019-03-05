namespace WebServer
{
    using System;
    using WebServer.Application;
    using WebServer.Server;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing;
    using WebServer.Server.Routing.Contracts;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            new StartUp().Run();
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
