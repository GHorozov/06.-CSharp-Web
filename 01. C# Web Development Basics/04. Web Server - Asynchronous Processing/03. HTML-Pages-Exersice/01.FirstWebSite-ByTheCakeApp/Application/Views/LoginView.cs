namespace _01.FirstWebSite_ByTheCakeApp.Application.Views
{
    using System;
    using System.IO;
    using MyWebServer.Server.Contracts;

    public class LoginView : IView
    {
        private const string htmlElement = "<!--replace-->";
        public LoginView()
        {
        }

        public LoginView(string username,string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get;private set; }
        public string Password { get;private set; }
        public string ErrorString { get; set; }

        public string View()
        {
            var html = File.ReadAllText(@"..\..\..\Application\Resourses\login.html");

            if(this.ErrorString != null)
            {
                html = html.Replace(htmlElement, this.ErrorString);
            }
            else if(!string.IsNullOrWhiteSpace(this.Username))
            {
                html = html.Replace(htmlElement, $@"Hi {this.Username}, your password is {this.Password}");
            }

            return html;
        }
    }
}
