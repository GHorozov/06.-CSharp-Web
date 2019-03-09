namespace MyWebServer.Server.Exeptions
{
    using System;

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) 
            :base(message)
        {
        }
    }
}
