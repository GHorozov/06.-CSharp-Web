namespace MyWebServer.Server.Handlers
{
    using System;
    using System.Text.RegularExpressions;
    using Contracts;
    using MyWebServer.Server.Common;
    using MyWebServer.Server.HTTP.Contracts;
    using MyWebServer.Server.HTTP.Response;
    using MyWebServer.Server.Routing.Contracts;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            foreach (var kvp in this.serverRouteConfig.Routes[httpContext.Request.RequestMethod])
            {
                string pattern = kvp.Key;
                Regex regex = new Regex(pattern);
                Match match = regex.Match(httpContext.Request.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (var parameter in kvp.Value.Parameters)
                {
                    httpContext.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            return new NotFoundResponse();
        }
    }
}
