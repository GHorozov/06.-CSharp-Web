namespace BankSystem.App.Commands
{
    using System;
    using BankSystem.App.Sessions.Contracts;
    using BankSystem.Data;

    public class ExitCommand : Command
    {
        public ExitCommand(BankSystemDbContext context, string[] args) 
            : base(context, args)
        {
        }

        public override string Execute(IUserSession userSession)
        {
            Environment.Exit(0);

            return string.Empty;
        }
    }
}
