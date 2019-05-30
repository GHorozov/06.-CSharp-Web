namespace IRunes.App
{
    using SIS.WebServer;
    using SIS.WebServer.Result;
    using SIS.WebServer.Routing;
    using SIS.HTTP.Enums;
    using Data;
    using IRunes.App.Controllers;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }

            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void Configure(ServerRoutingTable serverRoutingTable)
        {
            #region Index/Home
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new RedirectResult("/Home/Index"));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Home/Index", request => new HomeController().Index(request));
            #endregion
            
            #region User
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Login", request => new UserController().Login(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Users/Login", request => new UserController().LoginConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Register", request => new UserController().Register(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Users/Register", request => new UserController().RegisterConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Logout", request => new UserController().Logout(request));
            #endregion

            #region Album
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/All", request => new AlbumController().AllAlbums(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/Create", request => new AlbumController().Create(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Albums/Create", request => new AlbumController().CreateConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/Details", request => new AlbumController().Details(request));
            #endregion

            #region Track
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Tracks/Create", request => new TrackController().Create(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/Tracks/Create", request => new TrackController().CreateConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/Tracks/Details", request => new TrackController().Details(request));
            #endregion

        }
    }
}
