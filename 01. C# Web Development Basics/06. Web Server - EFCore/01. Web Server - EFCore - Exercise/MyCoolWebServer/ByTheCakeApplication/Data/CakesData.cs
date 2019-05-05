namespace MyCoolWebServer.ByTheCakeApplication.Data
{
    using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        private const string DatabasePath = @"C:\GitHub\06.-CSharp-Web\01. C# Web Development Basics\06. Web Server - EFCore\01. Web Server - EFCore - Exercise\MyCoolWebServer\ByTheCakeApplication\Resources\database.csv";

        public void Add(string name, string price)
        {
            var streamReader = new StreamReader(DatabasePath);
            var id = streamReader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Length;

            streamReader.Dispose();

            var cake = new AddProductViewModel(id, name, decimal.Parse(price));

            using (var streamWriter = new StreamWriter(DatabasePath, true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }
        }

        public IEnumerable<AddProductViewModel> All()
        {
            return File
                    .ReadAllLines(DatabasePath)
                    .Where(x => x.Contains(","))
                    .Select(x => x.Split(","))
                    .Select(x => new AddProductViewModel(int.Parse(x[0]), x[1], decimal.Parse(x[2])));
        }

        public AddProductViewModel Find(int id)
        {
            return this.All()
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
