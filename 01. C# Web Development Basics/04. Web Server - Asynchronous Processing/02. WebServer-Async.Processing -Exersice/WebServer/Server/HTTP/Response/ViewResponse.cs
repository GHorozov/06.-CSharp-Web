namespace WebServer.Server.HTTP.Response
{
    using System;
    using WebServer.Server.Contracts;
    using WebServer.Server.Enums;
    using WebServer.Server.Exeptions;

    public class ViewResponse : HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpResponceCode statusCode, IView view)
        {
            this.ValidateStatusCode(statusCode);

            this.view = view;
            this.StatusCode = statusCode;
        }

        private void ValidateStatusCode(HttpResponceCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;

            if(statusCodeNumber > 300 && statusCodeNumber < 399)
            {
                throw new InvalidResponseException("Invalid status code for correct response.");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}{Environment.NewLine}{this.view.View()}";
        }

    }
}
