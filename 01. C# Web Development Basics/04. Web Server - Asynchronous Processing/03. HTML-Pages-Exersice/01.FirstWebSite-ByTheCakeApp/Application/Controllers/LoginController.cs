namespace _01.FirstWebSite_ByTheCakeApp.Application.Controllers
{
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.HTTP.Response;
    using _01.FirstWebSite_ByTheCakeApp.Application.Views;

    public class LoginController
    {
        public HttpResponse LoginGet()
        {
            return new ViewResponse(HttpResponceCode.OK, new LoginView());
        }

        public HttpResponse LoginPost(string username, string password)
        {
            var loginView = new LoginView(username, password);
            if (string.IsNullOrWhiteSpace(username))
            {
                loginView.ErrorString = "Invalid input!";
                return new ViewResponse(HttpResponceCode.OK, loginView);
            }

            return new ViewResponse(HttpResponceCode.OK, loginView);
        }
    }
}
