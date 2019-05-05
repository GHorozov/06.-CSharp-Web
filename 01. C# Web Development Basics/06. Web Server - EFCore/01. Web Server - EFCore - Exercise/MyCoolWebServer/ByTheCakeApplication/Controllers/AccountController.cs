namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Services;
    using MyCoolWebServer.ByTheCakeApplication.Services.Contracts;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels;
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Account;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System;
    using System.Collections.Generic;

    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }

        public IHttpResponse Register()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(@"account\register");
        }

        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            this.SetDefaultViewData();

            if(model.Username.Length < 3 || model.Password != model.ConfirmPassword)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid user details!";

                return this.FileViewResponse(@"account\register");
            }

            bool registrationResult = this.userService.Create(model.Username, model.Password);

            if (registrationResult)
            {
                this.LoginUser(req, model.Username);
                return new RedirectResponse(@"/");
            }
            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "This username is taken!";

                return this.FileViewResponse(@"account\register");
            }
        }

        public IHttpResponse Login()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req, LoginViewModel model)
        {
            
            if(string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                this.ViewData["showError"] = "display";
                this.ViewData["error"] = "You must fulfill all fields!";

                return this.FileViewResponse(@"account\login");
            }

            var loginResult = this.userService.Find(model.Username, model.Password);

            if (loginResult)
            {
                //add const from sessionstore class to discribe current user in session
                req.Session.Add(SessionStore.CurrentUserKey, model.Username);

                //add shopping cart to session
                req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

                return new RedirectResponse(@"/");
            }
            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Incorrect username or password!";

                return this.FileViewResponse(@"account\login");
            }
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = this.userService.GetProfile(username);
            if(profile == null)
            {
                throw new InvalidOperationException($"The user could not be found in database.");
            }

            this.ViewData["username"] = profile.Username;
            this.ViewData["registrationDate"] = profile.RegistrationDate.ToShortDateString();
            this.ViewData["ordersCount"] = profile.TotalOrders.ToString();

            return this.FileViewResponse(@"account\profile");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse(@"/login");
        }

        private void SetDefaultViewData()
        {
            this.ViewData["authDisplay"] = "none";
        }

        private void LoginUser(IHttpRequest req, string username)
        {
            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}
