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
        public const string DefaultPath = @"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\06. Web Server - EFCore\01. Web Server - EFCore - Exercise\MyCoolWebServer\ByTheCakeApplication\Resources\{0}.html";
        public const string ContentPlaceholder = "{{{content}}}";

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>()
            {
                ["authDisplay"] = "block",
                ["showError"] = "none"
            };
        }

        public IDictionary<string, string> ViewData { get; private set; }

        protected void AddError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProccesFileHtml(fileName);

            if(this.ViewData != null && this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
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
