namespace _01.FirstWebSite_ByTheCakeApp.Application.Views
{
    using System;
    using System.IO;
    using MyWebServer.Server.Contracts;
    using _01.FirstWebSite_ByTheCakeApp.Application.Models;

    public class AddView : IView
    {
        private const string htmlElement = "<!--replace-->";
        private Cake cake;

        public AddView()
        {
        }

        public AddView(Cake cake)
        {
            this.cake = cake;
        }

        public string ErrorString { get; set; }

        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\add.html");

            if (!string.IsNullOrWhiteSpace(this.ErrorString))
            {
                html = html.Replace(htmlElement, this.ErrorString);
            }
            else if(cake != null)
            {
                html = html.Replace(htmlElement, $"Name: {this.cake.Name} Price: ${this.cake.Price}");
            }

            return html;
        }
    }
}
