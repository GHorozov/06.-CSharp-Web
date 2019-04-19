namespace MyCoolWebServer.ByTheCakeApplication.Infrastructure
{
    using MyCoolWebServer.ByTheCakeApplication.Views;
    using MyCoolWebServer.Server.Enums;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.IO;

    public abstract class Controller
    {
        public IHttpResponse FileViewResponse(string fileName)
        {
            var fileHtml = File.ReadAllText($@"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\04. Web Server - Asynchronous Processing\04. HTML-Pages-Exersice-2\Launcher\ByTheCakeApplication\Resources\{fileName}.html");

            return new ViewResponse(HttpStatusCode.Ok, new FileView(fileHtml));
        }
    }
}
