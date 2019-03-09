namespace MyWebServer.Server.Contracts
{
    using MyWebServer.Server.Routing.Contracts;

    public interface IAplication
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
