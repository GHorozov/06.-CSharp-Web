namespace BankSystem.App
{
    using System;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.App.Contracts;
    using BankSystem.App.IO.Contracts;
    using BankSystem.App.Sessions.Contracts;

    public class Engine
    {
        private BankSystemDbContext context;
        private IReader reader;
        private IWriter writer;
        private ICommandParser commandParser;
        private IUserSession userSession;

        public Engine(BankSystemDbContext context, ICommandParser commandParser, IUserSession userSession, IReader reader, IWriter writer)
        {
            this.context = context;
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.userSession = userSession;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    this.writer.Write("Enter command: ");
                    var input = this.reader.ReadLine().Split(" ").ToArray();
                    var command = string.Empty;
                    string[] args = null;
                    if (input[0] == "Add")
                    {
                        command = input[0] + input[1];
                        args = input.Skip(2).ToArray();
                    }
                    else
                    {
                        command = input[0];
                        args = input.Skip(1).ToArray();
                    }
                    
                    var resultCommand = this.commandParser.ParseCommand(command, args);
                    var resultMessage = resultCommand.Execute(this.userSession);
                    this.writer.WriteLine(resultMessage);
                }
                catch (ArgumentException ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
