namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.App.Messages;
    using System.Text.RegularExpressions;
    using BankSystem.App.Sessions.Contracts;

    public class RegisterCommand : Command
    {
        public RegisterCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            if (userSession.IsLoggedIn())
            {
                throw new ArgumentException(ErrorMessages.LogoutFirst);
            }

            var username = args[0];
            var password = args[1];
            var email = args[2];

            var isMatchUsername = Regex.IsMatch(username, @"^[^0-9][A-Za-z0-9]{3,}$");
            if (!isMatchUsername)
            {
                throw new ArgumentException(ErrorMessages.IncorrectUsername);
            }

            var isMatchPassword = Regex.IsMatch(password, @"^[a-zA-Z0-9]*[a-zA-Z][a-zA-Z0-9]{6,}$");
            if (!isMatchPassword)
            {
                throw new ArgumentException(string.Format(ErrorMessages.IncorrectPassword));
            }

            var isMatchEmail = Regex.IsMatch(email, @"^[A-Za-z0-9]+[.-_]?[A-Za-z0-9]+@[a-z]+\.[a-z]+$");
            if (!isMatchEmail)
            {
                throw new ArgumentException(string.Format(ErrorMessages.IncorrectEmail));
            }


            if (context.Users.Any(x => x.Username == username))
            {
                throw new ArgumentException(string.Format(ErrorMessages.UserExists));
            }

            var user = new User(username, password, email);
            context.Users.Add(user);
            context.SaveChanges();

            var result = string.Format(SuccessMessages.UserRegistered, username);

            return result;
        }
    }
}
