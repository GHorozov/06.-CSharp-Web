namespace MyCoolWebServer.GamesStoreApplication
{
    using System;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using MyCoolWebServer.GameStoreApplication.Controllers;
    using MyCoolWebServer.GameStoreApplication.Data;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Account;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Admin;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class GameStoreApp : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add("/");
            appRouteConfig.AnonymousPaths.Add("/account/register");
            appRouteConfig.AnonymousPaths.Add("/account/login");

            //----------------------------------------------------------------------------------------
            //account

            appRouteConfig
                .Get("/account/register", req => new AccountController(req).Register());

            appRouteConfig
                .Post("/account.register", req => new AccountController(req)
                .Register(new RegisterViewModel()
                {
                    Email = req.FormData["email"],
                    FullName = req.FormData["full-name"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirm-password"]
                }));

            appRouteConfig
                .Get("/account/login", req => new AccountController(req).Login());

            appRouteConfig
                .Post("/account/login", req => new AccountController(req)
                .Login(new LoginViewModel()
                {
                    Email = req.FormData["email"],
                    Password = req.FormData["password"]
                }));

            appRouteConfig
                .Get("/account/logout", req => new AccountController(req).Logout());

            //---------------------------------------------------------------------------------------
            //admin

            appRouteConfig
                .Get("/admin/games/add", req => new AdminController(req).Add());

            appRouteConfig
                .Post("/admin/games/add", req => new AdminController(req).Add(new AdminAddGameViewModel()
                {
                    Title = req.FormData["title"],
                    TrailerId = req.FormData["videoId"],
                    Description = req.FormData["description"],
                    ImageUrl = req.FormData["imageUrl"],
                    Price = decimal.Parse(req.FormData["price"]),
                    Size = double.Parse(req.FormData["size"]),
                    ReleaseDate = DateTime.ParseExact(req.FormData["release-date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                }));

            appRouteConfig
                .Get("/admin/games/list", req => new AdminController(req).List());

            appRouteConfig
                .Get("/admin/games/edit/{(?<id>[0-9]+)}", req => new AdminController(req).Edit(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post("/admin/games/edit/{(?<id>[0-9]+)}", req => new AdminController(req).Edit(new AdminEditGameViewModel()
                {
                    Id = int.Parse(req.UrlParameters["id"]),
                    Title = req.FormData["title"],
                    TrailerId = req.FormData["videoId"],
                    Description = req.FormData["description"],
                    ImageUrl = req.FormData["imageUrl"],
                    Price = decimal.Parse(req.FormData["price"]),
                    Size = double.Parse(req.FormData["size"]),
                    ReleaseDate = DateTime.ParseExact(req.FormData["release-date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                }));

            appRouteConfig
                .Get("/admin/games/delete/{(?<id>[0-9]+)}", req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
               .Post("/admin/games/delete/{(?<id>[0-9]+)}", req => new AdminController(req).DeleteGame(int.Parse(req.UrlParameters["id"])));

            //-------------------------------------------------------------------------------------
            //home

            appRouteConfig
                .Get("/", req => new HomeController(req).Index());

            appRouteConfig
                .Get("/home/games/user/{(?<id>[0-9]+)}", req => new HomeController(req).OwnedGames(int.Parse(req.UrlParameters["id"])));


        }
    }
}
