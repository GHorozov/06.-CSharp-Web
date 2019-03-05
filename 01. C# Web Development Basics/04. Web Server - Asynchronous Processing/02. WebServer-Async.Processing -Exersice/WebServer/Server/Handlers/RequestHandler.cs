namespace WebServer.Server.Handlers
{
    using System;
    using WebServer.Server.Common;
    using WebServer.Server.Handlers.Contracts;
    using WebServer.Server.HTTP;
    using WebServer.Server.HTTP.Contracts;

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
