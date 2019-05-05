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
                 .Get("/calculator", req => new CalculatorContoller().Calculator());

            appRouteConfig
                .Post("/calculator", req => new CalculatorContoller().Calculator(req.FormData["firstValue"], req.FormData["sign"], req.FormData["secondValue"]));

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
                .Get("/mail", req => new MailController().Mail());

            appRouteConfig
                .Post("/mail", req => new MailController().Mail(req.FormData["name"], req.FormData["password"]));

            appRouteConfig
                .Get("/mail/send", req => new MailController().SendMail());

            appRouteConfig
                .Post("/mail/send", req => new MailController().SendMail(req.FormData["to"], req.FormData["subject"], req.FormData["message"]));

            appRouteConfig
                .Get("/greetings", req => new GreetingsController().GetPage());

            appRouteConfig
                .Post("/greetings", req => new GreetingsController().ShowGreetings(req.FormData["firstName"], req.FormData["lastName"], req.FormData["age"]));

            appRouteConfig
                .Get("/servey", req => new ServeyController().GetServey());

            appRouteConfig
                .Post("/servey", req => new ServeyController()
                .SendServey(req.FormData["firstName"], req.FormData["lastName"], req.FormData["date"], req.FormData["gender"], req.FormData["status"], req.FormData["recomendations"], req.FormData["youown"], req.FormData["buttonClick"]));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get("/cart", req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post("/shopping/finish-order", req => new ShoppingController().FinishOrder(req));
        }
    }
}
