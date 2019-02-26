namespace _03.RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var dict = new Dictionary<string, string>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var lineParts = input.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var path = lineParts[0];
                var method = lineParts[1];

                dict[method] = path;
            }

            var inputRequest = Console.ReadLine().Split(new[] { "/", " "}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var methodRequest = inputRequest[0].ToLower();
            var pathRequest = inputRequest[1];

            Console.WriteLine(new string('-', 20));
            if(dict.ContainsKey(methodRequest) && dict[methodRequest].Contains(pathRequest))
            {
                Console.WriteLine("HTTP/1.1 200 OK");
                Console.WriteLine("Content-Lenght: 2");
                Console.WriteLine("Content-Type: text/plain");
                Console.WriteLine();
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("HTTP/1.1 404 NotFound");
                Console.WriteLine("Content-Lenght: 9");
                Console.WriteLine("Content-Type: text/plain");
                Console.WriteLine();
                Console.WriteLine("NotFound");
            }
        }
    }
}
