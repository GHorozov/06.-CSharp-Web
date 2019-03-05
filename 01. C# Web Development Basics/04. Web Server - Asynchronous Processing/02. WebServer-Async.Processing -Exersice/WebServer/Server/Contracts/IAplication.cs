namespace WebServer.Server.Contracts
{
    using WebServer.Server.Routing.Contracts;

    public interface IAplication
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
