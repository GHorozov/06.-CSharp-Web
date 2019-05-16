namespace MyCoolWebServer.GameStoreApplication.Controllers
{
    using MyCoolWebServer.GameStoreApplication.Services;
    using MyCoolWebServer.GameStoreApplication.Services.Contracts;
    using MyCoolWebServer.GameStoreApplication.ViewModels;
    using MyCoolWebServer.GameStoreApplication.ViewModels.Account;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;

    public class AccountController : BaseController
    {
        private const string RegisterViewPathConst = @"account\register";
        private const string LoginViewPathConst = @"account\login";

        private IUserService userService;

        public AccountController(IHttpRequest request) 
            : base(request)
        {
            this.userService = new UserService();
        }

        public IHttpResponse Register()
        {
            return this.FileViewResponse(@"account\register");
        }

        public IHttpResponse Register(RegisterViewModel model)
        {
            if(!this.ValidateModel(model))
            {
                return this.FileViewResponse(RegisterViewPathConst); 
            }

            var success = this.userService.CreateUser(model);
            if (!success)
            {
                this.ShowError("User already exist or email is not unique!");

                return this.FileViewResponse(RegisterViewPathConst);
            }

            this.LoginUser(model.Email);

            return new RedirectResponse("/");
        }

        public IHttpResponse Login()
        {
            return this.FileViewResponse(LoginViewPathConst);
        }

        public IHttpResponse Login(LoginViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.FileViewResponse(LoginViewPathConst);
            }

            var success = this.userService.Find(model);
            if (!success)
            {
                this.ShowError("Invalid email or password!");

                return this.FileViewResponse(LoginViewPathConst);
            }

            this.LoginUser(model.Email);

            return new RedirectResponse("/");
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return new RedirectResponse("/");
        }

        private void LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
            this.Request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}
