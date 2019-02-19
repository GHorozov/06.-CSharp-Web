namespace BankSystem.App.Commands
{
    using System;
    using BankSystem.Data;
    using BankSystem.App.Messages;
    using BankSystem.App.Sessions.Contracts;

    public class LogoutCommand : Command
    {
        public LogoutCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            var result = string.Empty;
            var username = string.Empty;
            if (userSession.IsLoggedIn())
            {
                username = userSession.User.Username;
                userSession.Logout();
            }
            else
            {
                throw new ArgumentException(ErrorMessages.LoginFirst);
            }

            result = string.Format(SuccessMessages.UserLogout, username);

            return result;
        }
    }
}
