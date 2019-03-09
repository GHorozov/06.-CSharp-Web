namespace MyWebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using Handlers;
    using Routing.Contracts;
    using MyWebServer.Server.Common;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler requestHandler, IEnumerable<string> parameters)
        {
            CoreValidator.ThrowIfNull(requestHandler, nameof(requestHandler));
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));

            this.RequestHandler = requestHandler;
            this.Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; }

        public RequestHandler RequestHandler { get; }
    }
}
