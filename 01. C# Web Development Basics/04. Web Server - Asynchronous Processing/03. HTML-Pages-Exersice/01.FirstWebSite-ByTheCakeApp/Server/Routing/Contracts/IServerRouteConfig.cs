namespace MyWebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using MyWebServer.Server.Enums;

    public interface IServerRouteConfig
    {
        Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; }
    }
}
