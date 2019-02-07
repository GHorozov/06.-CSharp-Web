namespace _01.SchoolCompetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var results = new Dictionary<string, int>();
            var categories = new Dictionary<string, SortedSet<string>>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var line = input.Split(" ");
                var name = line[0];
                var category = line[1];
                var points = int.Parse(line[2]);

                if (!results.ContainsKey(name))
                {
                    results[name] = 0;
                }

                if (!categories.ContainsKey(name))
                {
                    categories[name] = new SortedSet<string>();
                }

                results[name] += points;
                categories[name].Add(category);
            }

            var orderStudents = results.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToArray();

            foreach (var student in orderStudents)
            {
                var name = student.Key;
                var points = student.Value;
                var studentCategories = categories[student.Key];

                Console.WriteLine($"{name}: {points} [{string.Join(", ", studentCategories)}]");
            }
        }
    }
}
