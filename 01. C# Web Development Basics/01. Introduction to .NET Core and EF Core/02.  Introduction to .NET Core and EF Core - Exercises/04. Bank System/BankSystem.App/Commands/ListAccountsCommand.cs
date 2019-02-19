namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using BankSystem.Data;
    using BankSystem.App.Sessions.Contracts;
    using BankSystem.App.Messages;

    public class ListAccountsCommand : Command
    {
        public ListAccountsCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            if (!userSession.IsLoggedIn())
            {
                throw new ArgumentException(ErrorMessages.LoginFirst);
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Accounts for user {userSession.User.Username}");
            sb.AppendLine("Saving Accounts:");
            foreach (var savingAccount in context.SavingsAccounts.Where(x => x.UserId == userSession.User.Id).ToArray())
            {
                sb.AppendLine($"--{savingAccount.AccountNumber} {savingAccount.Balance:f2}");
            }
            sb.AppendLine("Checking Accounts: ");
            foreach (var checkingAccount in context.CheckingAccounts.Where(x => x.UserId == userSession.User.Id).ToArray())
            {
                sb.AppendLine($"--{checkingAccount.AccountNumber} {checkingAccount.Balance:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
