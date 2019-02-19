namespace BankSystem.App.Commands
{
    using BankSystem.Data;
    using BankSystem.App.Commands.Contracts;
    using BankSystem.App.Sessions.Contracts;

    public abstract class Command : ICommand
    {
        protected BankSystemDbContext context;
        protected string[] args;

        public Command(BankSystemDbContext context, string[] args)
        {
            this.context = context;
            this.args = args;
        }

        public abstract string Execute(IUserSession userSession);
    }
}
