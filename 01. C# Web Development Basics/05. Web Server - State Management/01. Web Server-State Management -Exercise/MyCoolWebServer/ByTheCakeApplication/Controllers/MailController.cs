namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using MyCoolWebServer.ByTheCakeApplication.Infrastructure;
    using MyCoolWebServer.Server.Http.Contracts;
    using MyCoolWebServer.Server.Http.Response;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text.RegularExpressions;

    public class MailController : Controller
    {
        public IHttpResponse Mail()
        {
            return this.FileViewResponse(@"mail\mail", new Dictionary<string, string>()
            {
                ["loginForm"] = "display",
                ["displayError"] = "none",
                ["mailForm"] = "none",
            });
        }

        public IHttpResponse Mail(string name, string password)
        {
            const string error = "Invalid username or pasword!";

            if(name != "suAdmin" || password != "aDmInPw17")
            {
                return this.FileViewResponse(@"mail\mail", new Dictionary<string, string>()
                {
                    ["loginForm"] = "display",
                    ["displayError"] = "display",
                    ["error"] = error,
                    ["mailForm"] = "none"
                });
            }

            return  new RedirectResponse(@"/mail/send");
        }

        public IHttpResponse SendMail()
        {
            return this.FileViewResponse(@"mail\mail", new Dictionary<string, string>()
            {
                ["loginForm"] = "none",
                ["displayError"] = "none",
                ["mailForm"] = "display",
            });
        }

        public IHttpResponse SendMail(string toMail, string subjectText, string messageText)
        {
            var isDataCorrect = false;

            var pattern = @"[A-Za-z0-9]+@[A-Za-z]+.[A-Za-z]{2,3}";
            var regex = Regex.Match(toMail, pattern);
            if (regex.Success && subjectText.Length < 100)
            {
                isDataCorrect = true;
            }

            var error = "Invalid input data!";
            if (!isDataCorrect)
            {
                return this.FileViewResponse(@"mail\mail", new Dictionary<string, string>()
                {
                    ["loginForm"] = "none",
                    ["displayError"] = "display",
                    ["error"] = error,
                    ["mailForm"] = "display",
                });
            }

            var bodyBytes = WebUtility.HtmlDecode(messageText);

            var fromAddress = new MailAddress("georgihorozov64@gmail.com", "Georgi");
            var toAddress = new MailAddress("testmail8101@abv.bg", "testmail");
            const string fromPassword = ""; //enter password for from mail address
            string subject = subjectText;
            string body = bodyBytes;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            var result = "Email sended!";
            return this.FileViewResponse(@"mail\mail", new Dictionary<string, string>()
            {
                ["loginForm"] = "none",
                ["displayError"] = "display",
                ["error"] = result,
                ["mailForm"] = "display",
            });
        }
    }
}
