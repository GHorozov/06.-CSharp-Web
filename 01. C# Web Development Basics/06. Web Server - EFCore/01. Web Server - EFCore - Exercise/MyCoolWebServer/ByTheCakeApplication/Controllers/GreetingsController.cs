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
            this.ViewData["displayDiv"] = "none";

            return this.FileViewResponse(@"greetings\greetings");
        }

        public IHttpResponse ShowGreetings(string firstName, string lastName, string age)
        {
            if(string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(age))
            {
                return new RedirectResponse(@"/greetings");
            }

            var greetings = $"Hello {firstName} {lastName} at age {age}!";

            this.ViewData["displayDiv"] = "display";
            this.ViewData["greetings"] = greetings;

            return this.FileViewResponse(@"greetings\greetings");
        }
    }
}
