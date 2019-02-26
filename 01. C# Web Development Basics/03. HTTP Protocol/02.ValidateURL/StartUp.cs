namespace _02.ValidateURL
{
    using System;
    using System.Net;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var inputUrl = Console.ReadLine();

            if (string.IsNullOrEmpty(inputUrl))
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var decodedUrl = WebUtility.UrlDecode(inputUrl);
            var parsedUrl = new Uri(decodedUrl);

            
            var isValidHostHttpsPort443 = parsedUrl.Scheme == "https" && parsedUrl.Port == 443;

            if (parsedUrl.Scheme == "http"
                && parsedUrl.Host != string.Empty
                && parsedUrl.Port == 80
                && parsedUrl.AbsolutePath != string.Empty
                && parsedUrl.AbsolutePath.Contains("/"))
            {
                PrintParts(parsedUrl);
            }
            else if(parsedUrl.Scheme == "https"
                && parsedUrl.Host != string.Empty
                && parsedUrl.Port == 443
                || parsedUrl.Port == 447
                && parsedUrl.AbsolutePath != string.Empty
                && parsedUrl.AbsolutePath.Contains("/"))
            {
                PrintParts(parsedUrl);
            }
            else
            {
                Console.WriteLine("Invalid URL");
                return;
            }
        }

        private static void PrintParts(Uri parsedUrl)
        {
            Console.WriteLine($"Protocol: {parsedUrl.Scheme}");
            Console.WriteLine($"Host: {parsedUrl.Host}");
            Console.WriteLine($"Port: {parsedUrl.Port}");
            Console.WriteLine($"Path: {parsedUrl.AbsolutePath}");

            var query = parsedUrl.Query;
            if (query != string.Empty)
            {
                Console.WriteLine($"Query: {query}");
            }

            var fragment = parsedUrl.Fragment;
            if (fragment != string.Empty)
            {
                Console.WriteLine($"Fragment: {fragment}");
            }
        }
    }
}
