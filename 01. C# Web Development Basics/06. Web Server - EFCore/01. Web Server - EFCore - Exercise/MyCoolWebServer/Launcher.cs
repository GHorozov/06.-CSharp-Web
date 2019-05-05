namespace MyCoolWebServer
{
    using System;
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using ByTheCakeApplication;

    public class Launcher
    {
        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new ByTheCakeApp();
            mainApplication.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}
