namespace BankSystem.App.Contracts
{
    using BankSystem.App.Commands.Contracts;

    public interface ICommandParser
    {
        ICommand ParseCommand(string command, string[] args);
    }
}
