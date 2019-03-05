namespace WebServer.Server.HTTP.Response
{
    using System;
    using System.Text;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Contracts;

    public abstract class HttpResponse : IHttpResponse
    {
        private string StatusMessage => this.StatusCode.ToString();

        protected HttpResponse()
        {
            this.HeaderCollection = new HttpHeaderCollection();
        }

        public HttpHeaderCollection HeaderCollection { get; }

        public HttpResponceCode StatusCode { get; protected set; }

        public override string ToString()
        {
            var response = new StringBuilder();
            var statusCodeNumber = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.StatusMessage}");
            response.AppendLine(this.HeaderCollection.ToString());
            response.AppendLine();
             
            return response.ToString().TrimEnd();
        }
    }
}
