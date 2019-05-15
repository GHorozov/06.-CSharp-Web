namespace MyCoolWebServer.GameStoreApplication.Controllers
{
    using MyCoolWebServer.GameStoreApplication.Common;
    using MyCoolWebServer.GameStoreApplication.Services;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.Infrastructure;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;

    public abstract class BaseController : Controller
    {
        private IUserService userService;

        public BaseController(IHttpRequest request)
        {
            this.Request = request;
            this.Authentication = new Authentication(false, false);
            this.userService = new UserService();

            this.ApplyAuthentication();
        }

        protected override string ApplicationDirectory => @"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\07. Workshop - Web Application. Advanced CSS - Bootstrap\07. Workshop-GameStore\MyCoolWebServer\GameStoreApplication";

        public IHttpRequest Request { get; private set; }

        protected Authentication Authentication { get; private set; } 

        public void ApplyAuthentication()
        {
            var anonymousDisplay = "flex";
            var authDisplay = "none";
            var adminDisplay = "none";

           var isAuthenticated = this.Request.Session.Contains(SessionStore.CurrentUserKey);

            if (isAuthenticated)
            {
                anonymousDisplay = "none";
                authDisplay = "flex";

                var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
                var isAdmin = this.userService.IsAdmin(email);

                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["anonymousDisplay"] = anonymousDisplay;
            this.ViewData["authDisplay"] = authDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
        }
    }
}
