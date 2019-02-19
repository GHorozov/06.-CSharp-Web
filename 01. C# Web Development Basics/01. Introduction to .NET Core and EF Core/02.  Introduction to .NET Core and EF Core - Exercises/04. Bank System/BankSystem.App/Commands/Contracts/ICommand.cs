namespace BankSystem.App.Commands.Contracts
{
    using BankSystem.App.Sessions.Contracts;

    public interface ICommand
    {
        string Execute(IUserSession userSession);
    }
}
