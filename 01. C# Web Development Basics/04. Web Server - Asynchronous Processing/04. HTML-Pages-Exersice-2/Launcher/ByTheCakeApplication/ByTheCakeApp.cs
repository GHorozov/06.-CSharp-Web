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

            appRouteConfig
                .Get("/add", req => new CakesController().Add());

            appRouteConfig
                .Post("/add", req => new CakesController().Add(req.FormData["name"], req.FormData["price"]));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req.UrlParameters));

            appRouteConfig
                 .Get("/calculator", req => new CalculatorContoller().Calculator());

            appRouteConfig
                .Post("/calculator", req => new CalculatorContoller().Calculator(req.FormData["firstValue"], req.FormData["sign"], req.FormData["secondValue"]));

            appRouteConfig
                .Get("/login", req => new AccountController().Login());

            appRouteConfig
                .Post("/login", req => new AccountController().Login(req.FormData["name"], req.FormData["password"]));

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
        }
    }
}
