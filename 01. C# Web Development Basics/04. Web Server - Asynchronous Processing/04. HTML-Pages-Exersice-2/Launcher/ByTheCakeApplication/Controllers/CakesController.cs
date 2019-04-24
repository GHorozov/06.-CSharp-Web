namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.ByTheCakeApplication.Models;
    using MyCoolWebServer.Server.Http.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesController : Controller
    {
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            return this.FileViewResponse(@"cakes\add", new Dictionary<string, string>()
            {
                ["display"] = "none"
            });
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake(name, decimal.Parse(price));
            cakes.Add(cake);

            using (var streamWriter = new StreamWriter(@"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\04. Web Server - Asynchronous Processing\04. HTML-Pages-Exersice-2\Launcher\ByTheCakeApplication\Data\database.csv", true))
            {
                streamWriter.WriteLine($"{name},{price}");
            }

            var result = this.FileViewResponse(@"cakes\add", new Dictionary<string, string>()
            {
                ["name"] = name,
                ["price"] = price,
                ["display"] = "block"
            });

            return result;
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            const string searchNameKey = "name";
            var results = string.Empty;

            if (urlParameters.ContainsKey(searchNameKey))
            {
                var searchName = urlParameters[searchNameKey];

                var searchedCakesDivs = File
                    .ReadAllLines(@"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\04. Web Server - Asynchronous Processing\04. HTML-Pages-Exersice-2\Launcher\ByTheCakeApplication\Data\database.csv")
                    .Where(x => x.Contains(","))
                    .Select(x => x.Split(","))
                    .Select(x => new Cake(x[0], decimal.Parse(x[1])))
                    .Where(x => x.Name.ToLower().Contains(searchName.ToLower()))
                    .Select(x => $"<div>{x.Name} - ${x.Price}</div>");

                results = string.Join(Environment.NewLine, searchedCakesDivs);
            }

            return this.FileViewResponse(@"cakes\search", new Dictionary<string, string>()
            {
                ["results"] = results
            });
        }
    }
}
