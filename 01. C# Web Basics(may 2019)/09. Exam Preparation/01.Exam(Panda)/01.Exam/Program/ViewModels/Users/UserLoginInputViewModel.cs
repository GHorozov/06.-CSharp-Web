using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.App.ViewModels.Users
{
    public class UserLoginInputViewModel
    {
        private const string ConstErrorMessage = "Invalid username or password!";

        [RequiredSis(ConstErrorMessage)]
        public string Username { get; set; }

        [RequiredSis(ConstErrorMessage)]
        public string Password { get; set; }
    }
}
