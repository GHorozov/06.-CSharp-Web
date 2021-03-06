﻿namespace WebServer.Server.HTTP
{
    using WebServer.Server.Common;
    using WebServer.Server.HTTP.Contracts;

    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.request = new HttpRequest(requestString);
        }

        public IHttpRequest Request => this.request;
    }
}
