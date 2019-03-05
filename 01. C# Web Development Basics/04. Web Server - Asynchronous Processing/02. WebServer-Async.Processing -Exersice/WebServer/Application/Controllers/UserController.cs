namespace WebServer.Application.Controllers
{
    using WebServer.Application.Views;
    using WebServer.Server;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Response;

    public class UserController
    {
        public HttpResponse RegisterGet()
        {
            return new ViewResponse(HttpResponceCode.OK, new RegisterView());
        }

        public HttpResponse RegisterPost(string name)
        {
            Model model = new Model { ["name"] = name };
            return new RedirectResponse($"/user/{name}");
        }

        public HttpResponse Details(string name)
        {
            Model model = new Model{ ["name"] = name };
            return new ViewResponse(HttpResponceCode.OK, new UserDetailsView(model));
        }
    }
}
