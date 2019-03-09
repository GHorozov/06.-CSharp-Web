namespace _01.FirstWebSite_ByTheCakeApp.Application.Views
{
    using System;
    using System.IO;
    using MyWebServer.Server;
    using MyWebServer.Server.Contracts;

    public class AboutUsView : IView
    {
        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\aboutUs.html");

            return html;
        }
    }
}
