namespace WebServer.Server.HTTP.Response
{
    using WebServer.Server.Enums;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
        {
            this.StatusCode = HttpResponceCode.NotFound;
        }
    }
}
