namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.App.Messages;
    using BankSystem.App.Sessions.Contracts;

    public class DeductFeeCommand : Command
    {
        public DeductFeeCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(ErrorMessages.IncorrectInput);
            }

            var accountNumber = args[0];

            if (!userSession.IsLoggedIn())
            {
                throw new ArgumentException(ErrorMessages.LoginFirst);
            }

            CheckingAccount targetAccount = null;
            var result = string.Empty;
            if (context.CheckingAccounts.Any(x => x.AccountNumber == accountNumber))
            {
                targetAccount = context.CheckingAccounts.Where(x => x.AccountNumber == accountNumber).First();
                targetAccount.DeductFee();
                context.SaveChanges();

                result = string.Format(SuccessMessages.DeductFee, accountNumber, targetAccount.Balance);
            }

            if (targetAccount == null)
            {
                throw new ArgumentException(string.Format(ErrorMessages.AccountDontExist, accountNumber));
            }

            return result;
        }
    }
}
