namespace BankSystem.Models
{
    using BankSystem.Models.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        public int Id { get; set; }

        [Required]
        [UsernameAttribute]
        public string Username { get; set; }

        [Required]
        [PasswordAttribute]
        public string Password { get; set; }

        [Required]
        [EmailAttribute]
        public string Email { get; set; }

        public List<CheckingAccount> CheckingAccounts { get; set; } = new List<CheckingAccount>();
        public List<SavingsAccount> SavingsAccounts { get; set; } = new List<SavingsAccount>();
    }
}
