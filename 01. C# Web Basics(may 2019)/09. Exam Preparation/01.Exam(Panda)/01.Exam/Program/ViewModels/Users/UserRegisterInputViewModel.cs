using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.App.ViewModels.Users
{
    public class UserRegisterInputViewModel
    {
        private const string ConstUsernameErrorMessage = "Username lenght must be between 5 and 20 symbols.";
        private const string ConstEmailErrorMessage = "Email lenght must be between 5 and 20 symbols.";

        [RequiredSis]
        [StringLengthSis(5, 20, ConstUsernameErrorMessage)]
        public string Username { get; set; }

        [RequiredSis]
        [StringLengthSis(5, 20, ConstEmailErrorMessage)]
        [EmailSis]
        public string Email { get; set; }

        [RequiredSis]
        [PasswordSis(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }
    }
}
