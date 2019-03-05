namespace WebServer.Server.Handlers
{
    using System;
    using WebServer.Server.HTTP.Contracts;

    public class GETHandler : RequestHandler
    {
        public GETHandler(Func<IHttpRequest, IHttpResponse> func)
            : base(func)
        {
        }
    }
}
