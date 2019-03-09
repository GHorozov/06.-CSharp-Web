namespace MyWebServer.Server
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Handlers;
    using MyWebServer.Server.HTTP;
    using MyWebServer.Server.HTTP.Contracts;
    using MyWebServer.Server.Routing.Contracts;

    public class ConnectionHandler
    {
        private readonly Socket client;
        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProccessRequestAsync()
        {
            var request = await this.ReadRequest();

            if(request != null)
            {
                var httpContext = new HttpContext(request);

                IHttpResponse response = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

                ArraySegment<byte> toBytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response.ToString()));

                await this.client.SendAsync(toBytes, SocketFlags.None);

                Console.WriteLine("===Request===");
                Console.WriteLine(request);
                Console.WriteLine();
                Console.WriteLine("===Response===");
                Console.WriteLine(response.ToString());
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            var request = string.Empty;
            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);
            
            while (true)
            {
                int numberBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None);

                if(numberBytesRead == 0)
                {
                    break;
                }

                request += Encoding.UTF8.GetString(data.Array, 0, numberBytesRead);

                if (numberBytesRead < 1023)
                {
                    break;
                }
            }

            if(request.Length == 0)
            {
                return null;
            }

            return request;
        }
    }
}
