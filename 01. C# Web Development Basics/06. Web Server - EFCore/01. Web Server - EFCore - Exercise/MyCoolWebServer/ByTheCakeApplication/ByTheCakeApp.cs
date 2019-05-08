namespace MyCoolWebServer.ByTheCakeApplication
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using MyCoolWebServer.ByTheCakeApplication.Controllers;
    using MyCoolWebServer.ByTheCakeApplication.Data;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Account;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using MyCoolWebServer.Server.Contracts;
    using MyCoolWebServer.Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new ByTheCakeDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add("/login");
            appRouteConfig.AnonymousPaths.Add("/register");

            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());

            appRouteConfig
                .Get("/register", req => new AccountController().Register());

            appRouteConfig
                .Post("/register", req => new AccountController().Register(req, new RegisterUserViewModel()
                {
                    Username = req.FormData["username"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirm-password"]
                }));

            appRouteConfig
                .Get("/login", req => new AccountController().Login());

            appRouteConfig
                .Post("/login", req => new AccountController().Login(req, new LoginViewModel()
                {
                    Username = req.FormData["username"],
                    Password = req.FormData["password"]
                }));

            appRouteConfig
                .Get("/profile", req => new AccountController().Profile(req));

            appRouteConfig
                .Post("/logout", req => new AccountController().Logout(req));

            appRouteConfig
            .Get("/add", req => new ProductsController().Add());

            appRouteConfig
                .Post("/add", req => new ProductsController().Add(new AddProductViewModel()
                {
                    Name = req.FormData["name"],
                    Price = decimal.Parse(req.FormData["price"]),
                    ImageUrl = req.FormData["imageUrl"]
                }));

            appRouteConfig
                .Get("/search", req => new ProductsController().Search(req));

            appRouteConfig
                .Get("/productDetails/{(?<id>[0-9]+)}", req => new ProductsController().Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get("/cart", req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post("/shopping/finish-order", req => new ShoppingController().FinishOrder(req));

            appRouteConfig
                .Get("/orders", req => new MyOrdersController().MyOrders(req));

            appRouteConfig
                .Get("/orderDetails/{(?<id>[0-9]+)}", req => new MyOrdersController().OrderDetails(int.Parse(req.UrlParameters["id"])));
        }
    }
}
