namespace _03.SimpleWebServer
{
    using System;
    using System.Net;
    using System.Text;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var port = 1337;
            var ipAddress = IPAddress.Parse("127.0.0.1");

            var tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();

            Task.Run(async () => await Connect(tcpListener))
                .GetAwaiter()
                .GetResult();
        }

        public static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();

                var buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                var clientMessage = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(clientMessage.Trim('\0'));

                var responseMessage = Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello, from server!");
                await client.GetStream().WriteAsync(responseMessage, 0, responseMessage.Length);

                client.Dispose();
            }
        }
    }
}
