namespace MyWebServer.Server.Handlers
{
    using System;
    using MyWebServer.Server.HTTP.Contracts;

    public class GETHandler : RequestHandler
    {
        public GETHandler(Func<IHttpRequest, IHttpResponse> func)
            : base(func)
        {
        }
    }
}
