namespace MyWebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using MyWebServer.Server.Enums;
    using MyWebServer.Server.Handlers;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes { get; }

        void AddRoute(string route, RequestHandler httpHandler);
    }
}
