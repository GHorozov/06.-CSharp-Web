namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.App.Sessions.Contracts;
    using BankSystem.App.Messages;

    public class AddInterestCommand : Command
    {
        public AddInterestCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            if (args.Length != 1 )
            {
                throw new ArgumentException(ErrorMessages.IncorrectInput);
            }

            var accountNumber = args[0];

            if (!userSession.IsLoggedIn())
            {
                throw new ArgumentException(ErrorMessages.LoginFirst);
            }

            SavingsAccount targetAccount = null;
            var result = string.Empty;
            if (context.SavingsAccounts.Any(x => x.AccountNumber == accountNumber))
            {
                targetAccount = context.SavingsAccounts.Where(x => x.AccountNumber == accountNumber).First();
                targetAccount.AddInterest();
                context.SaveChanges();

                result = string.Format(SuccessMessages.AddInterest, accountNumber, targetAccount.Balance);
            }

            if (targetAccount == null)
            {
                throw new ArgumentException(string.Format(ErrorMessages.AccountDontExist, accountNumber));
            }

            return result;
        }
    }
}
