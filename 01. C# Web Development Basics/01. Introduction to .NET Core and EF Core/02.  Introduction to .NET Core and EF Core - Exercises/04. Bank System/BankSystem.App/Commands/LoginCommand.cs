namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.App.Messages;
    using BankSystem.App.Sessions.Contracts;

    public class LoginCommand : Command
    {
        public LoginCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            if( args.Length == 0  || args.Length < 2)
            {
                throw new InvalidOperationException(ErrorMessages.TypeCorrectUsernamePassword);
            }

            var username = args[0];
            var password = args[1];

            var user = context
                .Users
                .Where(x => x.Username == username && x.Password == password)
                .SingleOrDefault();

            if(user == null)
            {
                throw new ArgumentException(ErrorMessages.IncorrectUsernamePassword);
            }

            userSession.Login(user);

            var result = string.Format(SuccessMessages.UserLoggedIn, username);

            return result;
        }
    }
}
