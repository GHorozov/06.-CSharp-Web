namespace WebServer.Server.HTTP.Contracts
{
    using WebServer.Server.Enums;

    public interface IHttpResponse
    {
        HttpHeaderCollection HeaderCollection { get; }

        HttpResponceCode StatusCode { get; }
    }
}
