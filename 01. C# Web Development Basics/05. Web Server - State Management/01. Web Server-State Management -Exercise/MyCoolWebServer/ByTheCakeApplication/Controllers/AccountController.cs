namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Models;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Collections.Generic;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            return this.FileViewResponse(@"account\login", new Dictionary<string, string>()
            {
                ["showError"] = "none",
            });
           
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if(!req.FormData.ContainsKey(formNameKey) || !req.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = req.FormData[formNameKey];
            var password = req.FormData[formPasswordKey];

            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                return this.FileViewResponse(@"account\login", new Dictionary<string, string>()
                {
                    ["showError"] = "display",
                    ["error"] = "You must fulfill all fields!"
                });
            }

            //add const from sessionstore class to discribe current user in session
            req.Session.Add(SessionStore.CurrentUserKey, name);

            //add shopping cart to session
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse(@"/");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse(@"/login");
        }
    }
}
