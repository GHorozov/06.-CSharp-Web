﻿namespace MyCoolWebServer.GameStoreApplication.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required]
        [Utilities.EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
