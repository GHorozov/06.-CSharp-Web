namespace MyWebServer.Server.Handlers
{
    using System;
    using System.Linq;
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.Handlers.Contracts;
    using MyWebServer.Server.HTTP;
    using MyWebServer.Server.HTTP.Contracts;
    using MyWebServer.Server.HTTP.Response;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            CoreValidator.ThrowIfNull(func, nameof(func));
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func.Invoke(httpContext.Request);

            httpResponse.HeaderCollection.Add(new HttpHeader("Content-Type", "text/html"));

            return httpResponse;
        }
    }
}
