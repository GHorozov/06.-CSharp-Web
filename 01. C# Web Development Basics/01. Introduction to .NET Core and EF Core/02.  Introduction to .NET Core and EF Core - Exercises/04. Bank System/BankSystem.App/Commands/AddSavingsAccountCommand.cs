namespace BankSystem.App.Commands
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.App.Messages;
    using BankSystem.App.Sessions.Contracts;

    public class AddSavingsAccountCommand : Command
    {
        private const string alfabetAndNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public AddSavingsAccountCommand(BankSystemDbContext context, string[] args)
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            var accountNumber = string.Empty;
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                var randomIndex = random.Next(0, alfabetAndNumbers.Length);
                accountNumber += alfabetAndNumbers[randomIndex];
            }

            if(args.Length == 0 || args.Length > 2)
            {
                throw new ArgumentException(ErrorMessages.IncorrectInput);
            }

            var balance = decimal.Parse(args[0]);
            var interestRate = decimal.Parse(args[1]);

            if (!userSession.IsLoggedIn())
            {
                throw new ArgumentException(ErrorMessages.LoginFirst);
            }

            var user = context
                .Users
                .Where(x => x.Id == userSession.User.Id)
                .SingleOrDefault();


            var savingAccount = new SavingsAccount(user.Id, accountNumber, balance, interestRate);
            context.SavingsAccounts.Add(savingAccount);
            context.SaveChanges();

            var result = string.Format(SuccessMessages.CreatedAccount, accountNumber);

            return result;
        }
    }
}
