namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Collections.Generic;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            return this.FileViewResponse(@"login\login", new Dictionary<string, string>()
            {
                ["displayForm"] = "none"
            });
        }

        public IHttpResponse Login(string name, string password)
        {
            return this.FileViewResponse(@"login\login", new Dictionary<string, string>()
            {
                ["name"] = name,
                ["password"] = password,
                ["displayForm"] = "display"
            });
        }

        public IHttpResponse Logout()
        {
            return new RedirectResponse(@"/");
        }
    }
}
