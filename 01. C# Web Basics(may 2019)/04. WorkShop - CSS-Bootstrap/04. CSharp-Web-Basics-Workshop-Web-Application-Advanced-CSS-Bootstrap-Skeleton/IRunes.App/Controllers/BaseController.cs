using IRunes.Models;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace IRunes.App.Controllers
{
    public abstract class BaseController
    {
        protected BaseController()
        {
            this.ViewData = new Dictionary<string, object>();
        }
        protected Dictionary<string, object> ViewData { get; set; }

        protected bool IsLoggedIn(IHttpRequest httpRequest)
        {
            return httpRequest.Session.ContainsParameter("username");
        }

        protected void SignIn(IHttpRequest httpRequest, User user)
        {
            httpRequest.Session.AddParameter("username", user.Username);
            httpRequest.Session.AddParameter("email", user.Email);
            httpRequest.Session.AddParameter("userId", user.Id);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();
        }

        private string ParseRemplate(string viewContent)
        {
            foreach (var item in this.ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{item.Key}", item.Value.ToString());
            }

            return viewContent;
        }

        protected IHttpResponse View([CallerMemberName] string view = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            var viewContent = File.ReadAllText("Views/" + controllerName + "/" + view + ".html");
            viewContent = this.ParseRemplate(viewContent);

            var htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }

        protected IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}
