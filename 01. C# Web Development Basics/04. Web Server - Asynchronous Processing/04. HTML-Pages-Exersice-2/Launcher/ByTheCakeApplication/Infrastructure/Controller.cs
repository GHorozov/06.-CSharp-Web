namespace MyCoolWebServer.ByTheCakeApplication.Infrastructure
{
    using MyCoolWebServer.ByTheCakeApplication.Views;
    using MyCoolWebServer.Server.Enums;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public abstract class Controller
    {
        public const string DefaultPath = @"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\04. Web Server - Asynchronous Processing\04. HTML-Pages-Exersice-2\Launcher\ByTheCakeApplication\Resources\{0}.html";
        public const string ContentPlaceholder = "{{{content}}}"; 

        public IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProccesFileHtml(fileName);

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        public IHttpResponse FileViewResponse(string fileName, Dictionary<string, string> values)
        {
            var result = this.ProccesFileHtml(fileName);

            if(values != null && values.Any())
            {
                foreach (var value in values)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        private string ProccesFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }
    }
}
