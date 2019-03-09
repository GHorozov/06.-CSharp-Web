namespace MyWebServer.Server.Handlers.Contracts
{
    using MyWebServer.Server.HTTP.Contracts;

    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}
