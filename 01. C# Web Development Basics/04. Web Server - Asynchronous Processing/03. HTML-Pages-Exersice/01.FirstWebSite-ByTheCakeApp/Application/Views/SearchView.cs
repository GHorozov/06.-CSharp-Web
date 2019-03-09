namespace _01.FirstWebSite_ByTheCakeApp.Application.Views
{
    using System;
    using System.IO;
    using MyWebServer.Server.Contracts;

    public class SearchView : IView
    {
        private const string htmlElement = "<!--replace-->";
        private string cakeName;

        public SearchView()
        {
        }

        public SearchView(string cakeName)
        {
            this.cakeName = cakeName;
        }

        public string ErrorString { get; set; }

        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\search.html");

            if (!string.IsNullOrWhiteSpace(this.ErrorString))
            {
                html = html.Replace(htmlElement, this.ErrorString);
            }
            else if (cakeName != null)
            {
                html = html.Replace(htmlElement, $@"{cakeName}");
            }

            return html;
        }
    }
}
