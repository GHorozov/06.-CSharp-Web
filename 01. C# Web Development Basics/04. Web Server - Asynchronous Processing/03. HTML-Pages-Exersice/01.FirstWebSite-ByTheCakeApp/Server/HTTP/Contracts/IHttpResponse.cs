namespace MyWebServer.Server.HTTP.Contracts
{
    using MyWebServer.Server.Enums;

    public interface IHttpResponse
    {
        HttpHeaderCollection HeaderCollection { get; }

        HttpResponceCode StatusCode { get; }
    }
}
