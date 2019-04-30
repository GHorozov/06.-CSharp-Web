namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.IO;

    public class ServeyController : Controller
    {
        public IHttpResponse GetServey()
        {
            return this.FileViewResponse(@"servey\servey");
        } 

        public IHttpResponse SendServey(string firstName, string lastName, string date, string gender, string status, string recomendations, string youown, string buttonClick)
        {
          
            if(buttonClick == "Reset all fields")
            {
                return new RedirectResponse(@"/servey");
            }

            var fileName = "survey-results";
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(gender) ||recomendations.Length > 120)
            {
                return new RedirectResponse(@"/servey");
            }

            using (var streamWriter = new StreamWriter($@"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\04. Web Server - Asynchronous Processing\04. HTML-Pages-Exersice-2\Launcher\ByTheCakeApplication\Data\{fileName}.csv", true))
            {
                streamWriter.WriteLine($"{firstName},{lastName},{date},{gender},{status},{recomendations},{youown}");
            }

            return new RedirectResponse(@"/servey");
        }
    }
}
