namespace _01.FirstWebSite_ByTheCakeApp.Application.Views
{
    using MyWebServer.Server.Contracts;
    using System.IO;

    public class NotFoundView : IView
    {
        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\notFound.html");

            return html;
        }
    }
}
