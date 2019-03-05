namespace WebServer.Application.Views
{
    using WebServer.Server.Contracts;

    public class HomeIndexView : IView
    {
        public string View() => "<h1>Welcome</h1>";
    }
}
