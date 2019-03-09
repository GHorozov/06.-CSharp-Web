namespace MyWebServer.Server.Handlers
{
    using System;
    using MyWebServer.Server.HTTP.Contracts;

    public class POSTHandler : RequestHandler
    {
        public POSTHandler(Func<IHttpRequest, IHttpResponse> func) 
            : base(func)
        {
        }
    }
}
