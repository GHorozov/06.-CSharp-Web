namespace _01.FirstWebSite_ByTheCakeApp.Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class CakeList
    {
        private static string dbFilePath = @"..\..\..\Application\Data\database.csv";
        private static List<Cake> cakes = new List<Cake>();

        public static void AddCake(Cake cake)
        {
            if (!File.Exists(dbFilePath))
            {
                var myFile =File.Create(dbFilePath);
                myFile.Close();
            }

            File.AppendAllText(dbFilePath, $"{cake.Name}:{cake.Price}{Environment.NewLine}");
            cakes.Add(cake);
        }

        public static string GetCakeByName(string cakeName)
        {
            var sb = new StringBuilder();

            if (!File.Exists(dbFilePath))
            {
                var myFile = File.Create(dbFilePath);
                myFile.Close();
            }

            if(cakes.Count == 0)
            {
                var dataFromFile = File.ReadAllLines(dbFilePath);

                for (int i = 0; i < dataFromFile.Length; i++)
                {
                    var line = dataFromFile[i].Split(":").ToArray();
                    var name = line[0];
                    var price = decimal.Parse(line[1]);

                    cakes.Add(new Cake(name, price));
                }
            }
           
            var result = cakes.Where(x => x.Name.ToLower() == cakeName.ToLower()).ToArray();
            foreach (var cake in result)
            {
                sb.AppendLine($"{cake.Name} ${cake.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
