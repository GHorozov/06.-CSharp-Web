namespace WebServer.Server.HTTP
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Collections.Generic;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.Common;
    using WebServer.Server.Enums;
    using WebServer.Server.Exeptions;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.HeaderCollection = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public Dictionary<string, string> FormData { get; private set; }

        public HttpHeaderCollection HeaderCollection { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            var requestLines = requestString.Split(Environment.NewLine).ToArray();

            if (!requestString.Any())
            {
                throw new BadRequestException("Request is not valid.");
            }

            var requestLine = requestLines[0].Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url.Split(new[] { "?", "#" }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();

            if (this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines[requestLines.Length - 1], this.FormData);
            }
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            try
            {
                return Enum.Parse<HttpRequestMethod>(method, true);
            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid request method");
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }

            var query = this.Url.Split("?")[1];
            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, Dictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }

            var queryPairs = query.Split("&");
            foreach (var pair in queryPairs)
            {
                var queryArgs = pair.Split("=");
                if (queryArgs.Length != 2)
                {
                    continue;
                }

                var key = WebUtility.UrlDecode(queryArgs[0]);
                var value = WebUtility.UrlDecode(queryArgs[1]);
                dict.Add(key, value);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            var endIndex = Array.IndexOf(requestLines, string.Empty);
            for (int i = 1; i < endIndex; i++)
            {
                var headersArgs = requestLines[i].Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if(headersArgs.Length != 2)
                {
                    throw new BadRequestException("Invalid headers arguments.");
                }

                var header = new HttpHeader(headersArgs[0], headersArgs[1].Trim());
                this.HeaderCollection.Add(header);
            }

            if (!this.HeaderCollection.ContainsKey("Host"))
            {
                throw new BadRequestException("Host header is not present in collection with headers.");
            }
        }
    }
}
