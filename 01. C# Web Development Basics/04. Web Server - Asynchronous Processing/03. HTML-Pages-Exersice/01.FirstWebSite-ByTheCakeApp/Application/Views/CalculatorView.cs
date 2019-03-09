namespace _01.FirstWebSite_ByTheCakeApp.Application.Views
{
    using System;
    using System.IO;
    using MyWebServer.Server.Contracts;

    public class CalculatorView : IView
    {
        private const string htmlElement = "<!--replace-->";
        public string ErrorString { get; set; }

        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\calculator.html");

            if(!string.IsNullOrWhiteSpace(this.ErrorString))
            {
                html = html.Replace(htmlElement, this.ErrorString);
            }

            return html;
        }
    }
}
