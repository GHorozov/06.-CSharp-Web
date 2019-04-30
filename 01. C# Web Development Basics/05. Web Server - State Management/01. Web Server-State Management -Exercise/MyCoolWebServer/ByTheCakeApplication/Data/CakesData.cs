namespace MyCoolWebServer.ByTheCakeApplication.Data
{
    using MyCoolWebServer.ByTheCakeApplication.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        private const string DatabasePath = @"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\05. Web Server - State Management\01. Web Server-State Management -Exercise\MyCoolWebServer\ByTheCakeApplication\Data\database.csv";

        public void Add(string name, string price)
        {
            var streamReader = new StreamReader(DatabasePath);
            var id = streamReader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Length;

            streamReader.Dispose();

            var cake = new Cake(id, name, decimal.Parse(price));

            using (var streamWriter = new StreamWriter(DatabasePath, true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }
        }

        public IEnumerable<Cake> All()
        {
            return File
                    .ReadAllLines(DatabasePath)
                    .Where(x => x.Contains(","))
                    .Select(x => x.Split(","))
                    .Select(x => new Cake(int.Parse(x[0]), x[1], decimal.Parse(x[2])));
        }

        public Cake Find(int id)
        {
            return this.All()
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
