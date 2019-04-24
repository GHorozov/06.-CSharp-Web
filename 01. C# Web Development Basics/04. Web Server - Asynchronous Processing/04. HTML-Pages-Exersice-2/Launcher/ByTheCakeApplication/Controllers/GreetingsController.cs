namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Collections.Generic;

    public class GreetingsController : Controller
    {
        public IHttpResponse GetPage()
        {
            return this.FileViewResponse(@"greetings\greetings", new Dictionary<string, string>()
            {
                ["displayDiv"] = "none",
            });
        }

        public IHttpResponse ShowGreetings(string firstName, string lastName, string age)
        {
            if(string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(age))
            {
                return new RedirectResponse(@"/greetings");
            }

            var greetings = $"Hello {firstName} {lastName} at age {age}!";

            return this.FileViewResponse(@"greetings\greetings", new Dictionary<string, string>()
            {
                ["displayDiv"] = "display",
                ["greetings"] = greetings
            });
        }
    }
}
