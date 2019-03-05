namespace WebServer.Server.Handlers
{
    using System;
    using WebServer.Server.HTTP.Contracts;

    public class POSTHandler : RequestHandler
    {
        public POSTHandler(Func<IHttpRequest, IHttpResponse> func) 
            : base(func)
        {
        }
    }
}
