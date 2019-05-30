using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.App.Controllers
{
    public class UserController : BaseController
    {
        private string HashPassword(string password)
        {
            using(SHA256 sha256hash = SHA256.Create())
            {
                var passwordInBytes = Encoding.UTF8.GetBytes(password);
                var hashPassword = sha256hash.ComputeHash(passwordInBytes);
                var hashPasswordAsString = Encoding.UTF8.GetString(hashPassword);

                return hashPasswordAsString;
            }
        }

        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var db = new RunesDbContext())
            {
                var username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                var password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();

                var userFromDb = db
                    .Users
                    .Where(x => (x.Username == username || x.Email == username)
                        && x.Password == this.HashPassword(password))
                    .FirstOrDefault();

                if(userFromDb == null)
                {
                    return this.Redirect("/Users/Login");
                }

                this.SignIn(httpRequest, userFromDb);
            }

            return this.Redirect("/");
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var db = new RunesDbContext())
            {
                var username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                var password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
                var confirmPassword = ((ISet<string>)httpRequest.FormData["confirmPassword"]).FirstOrDefault();
                var email = ((ISet<string>)httpRequest.FormData["email"]).FirstOrDefault();

                if(password != confirmPassword)
                {
                    return this.Redirect("/Users/Register");
                }

                var user = new User()
                {
                    Username = username,
                    Password = this.HashPassword(password),
                    Email = email
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            return this.Redirect("/Users/Login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            this.SignOut(httpRequest);

            return this.Redirect("/");
        }
    }
}
