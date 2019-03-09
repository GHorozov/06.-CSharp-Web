namespace MyWebServer.Application.Views
{
    using MyWebServer.Server.Contracts;
    using System.IO;

    public class HomeIndexView : IView
    {
        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\index.html");

            return html;
        }
    }
}
