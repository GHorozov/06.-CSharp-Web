namespace _01.URLDecode
{
    using System;
    using System.Net;

    public class StartUp
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(input);
            Console.WriteLine(decodedUrl);
        }
    }
}
