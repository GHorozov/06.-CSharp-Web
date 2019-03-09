namespace MyWebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using MyWebServer.Server.Handlers;

    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler RequestHandler { get; }
    }
}
