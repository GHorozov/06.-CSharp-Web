namespace BankSystem.App
{
    using System;
    using System.Linq;
    using System.Reflection;
    using BankSystem.App.Commands.Contracts;
    using BankSystem.App.Contracts;
    using BankSystem.Data;

    public class CommandParser : ICommandParser
    {
        private BankSystemDbContext context;

        public CommandParser(BankSystemDbContext context)
        {
            this.context = context;
        }

        public ICommand ParseCommand(string commandName, string[] args)
        {
            var allCommands = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsAbstract)
                .ToArray();

            var commandType = allCommands.Where(x => x.Name == commandName + "Command").FirstOrDefault();
            if(commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var command = (ICommand)Activator.CreateInstance(commandType, new object[] { this.context, args });

            return command;
        }
    }
}
