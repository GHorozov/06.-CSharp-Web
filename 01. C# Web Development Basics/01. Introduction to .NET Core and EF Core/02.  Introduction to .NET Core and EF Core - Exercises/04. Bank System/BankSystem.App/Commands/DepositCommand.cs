namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.App.Messages;
    using BankSystem.App.Sessions.Contracts;

    public class DepositCommand : Command
    {
        public DepositCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                throw new ArgumentException(ErrorMessages.IncorrectInput);
            }

            var accountNumber = args[0];
            var money = decimal.Parse(args[1]);

            if (!userSession.IsLoggedIn())
            {
                throw new ArgumentException(ErrorMessages.LoginFirst);
            }

            BaseAccount targetAccount = null;
            var result = string.Empty;
            if(context.CheckingAccounts.Any(x => x.AccountNumber == accountNumber))
            {
                targetAccount = context.CheckingAccounts.Where(x => x.AccountNumber == accountNumber).First();
                targetAccount.DepositMoney(money);
                context.SaveChanges();

                result = string.Format(SuccessMessages.DepositMoney, accountNumber, targetAccount.Balance);
            }
            else if (context.SavingsAccounts.Any(x => x.AccountNumber == accountNumber))
            {
                targetAccount = context.SavingsAccounts.Where(x => x.AccountNumber == accountNumber).First();
                targetAccount.DepositMoney(money);
                context.SaveChanges();

                result = string.Format(SuccessMessages.DepositMoney, accountNumber, targetAccount.Balance);
            }

            if(targetAccount == null)
            {
                throw new ArgumentException(string.Format(ErrorMessages.AccountDontExist, accountNumber));
            }


            return result;
        }
    }
}
