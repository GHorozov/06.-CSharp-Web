namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using WebServer.Server.Enums;
    using WebServer.Server.Handlers;
    using WebServer.Server.Routing.Contracts;

    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.Routes = new Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>>();

            var availableRequestMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod requestMethod in availableRequestMethods)
            {
                this.Routes.Add(requestMethod, new Dictionary<string, IRoutingContext>());
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        public Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (KeyValuePair<HttpRequestMethod, Dictionary<string, RequestHandler>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair <string, RequestHandler> requestHandler in kvp.Value)
                {
                    List<string> args = new List<string>();

                    var parseRegex = this.ParseRoute(requestHandler.Key, args);

                    IRoutingContext routingContext = new RoutingContext(requestHandler.Value, args);

                    this.Routes[kvp.Key].Add(parseRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, List<string> args)
        {
            var parseRegex = new StringBuilder();
            parseRegex.Append("^");

            if(requestHandlerKey == "/")
            {
                parseRegex.Append($"{requestHandlerKey}$");
                return parseRegex.ToString();
            }

            var tokens = requestHandlerKey.Split("/");
            this.ParseTokens(args, tokens, parseRegex);

            return parseRegex.ToString();
        }

        private void ParseTokens(List<string> args, string[] tokens, StringBuilder parseRegex)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = i == tokens.Length - 1 ? "$" : "/";
                if(!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parseRegex.Append($"{tokens[i]}{end}");
                    continue;
                }

                var pattern = "<\\w+>";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(tokens[i]);

                if (!match.Success)
                {
                    throw new InvalidOperationException($"Current token '{tokens[i]}' is not valid.");
                }

                var paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                args.Add(paramName);
                parseRegex.Append($"{tokens[i].Substring(1, tokens[i].Length - 2)}{end}");
            }
        }
    }
}
