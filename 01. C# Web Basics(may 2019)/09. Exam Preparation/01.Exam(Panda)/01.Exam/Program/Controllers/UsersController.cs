using Panda.App.ViewModels.Users;
using Panda.Models;
using Panda.Services.Contracts;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Action;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using System.Security.Cryptography;
using System.Text;

namespace Panda.App.Controllers
{
    public class UsersController : Controller
    {
        public IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Users/Login");
            }

            var user = this.userService.GetUserByusernameAndPassword(model.Username, this.HashPassword(model.Password));
            if(user == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(user.Id, user.Username, user.Email);

            return this.Redirect("/");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Users/Register");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            var user = new User()
            {
                Username = model.Username,
                Password = this.HashPassword(model.Password),
                Email = model.Email,
            };

            this.userService.CreateUser(user);

            return this.Redirect("/Users/Login");
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }

        [NonAction]
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var result = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));

                return result;
            }
        }
    }
}
