namespace BankSystem.App.IO
{
    using System;
    using BankSystem.App.IO.Contracts;

    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
