namespace BankSystem.App
{
    using BankSystem.Data;
    using BankSystem.App.Contracts;
    using BankSystem.App.Sessions;
    using BankSystem.App.Sessions.Contracts;
    using Microsoft.EntityFrameworkCore;
    using BankSystem.App.IO;
    using BankSystem.App.IO.Contracts;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            BankSystemDbContext context = new BankSystemDbContext();

            InitialteDatabase(context);

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            ICommandParser commandParser = new CommandParser(context);
            IUserSession userSession = new UserSession();

            var engine = new Engine(context, commandParser, userSession, reader, writer);
            engine.Run();
        }

        private static void InitialteDatabase(BankSystemDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
