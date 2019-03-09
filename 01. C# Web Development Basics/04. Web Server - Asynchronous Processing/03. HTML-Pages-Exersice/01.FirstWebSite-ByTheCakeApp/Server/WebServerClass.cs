namespace MyWebServer.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using MyWebServer.Server.Routing;
    using MyWebServer.Server.Contracts;
    using MyWebServer.Server.Routing.Contracts;

    public class WebServerClass : IRunnable
    {
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener tcpListener;
        private bool isRunning;

        public WebServerClass(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), this.port);
            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }

        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started. Listening to TCP client at 127.0.0.1:{this.port}");
            Task.Run(this.ListenLoop).Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();
                ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                await connectionHandler.ProccessRequestAsync();
            }
        }
    }
}
