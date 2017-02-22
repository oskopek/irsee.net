using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System;

namespace Irsee.IrcClient
{
    public class TcpConnection : IDisposable
    {
        private readonly ServerConfiguration configuration;
        private TcpClient client;

        public TcpConnection(ServerConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<NetworkStream> ConnectAsync()
        {
            client = new TcpClient();
            await client.ConnectAsync(configuration.Hostname, configuration.Port);
            return client.GetStream();
        }

        public NetworkStream Connect()
        {
            var task = ConnectAsync();
            task.Wait();
            return task.Result;
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void Disconnect()
        {
            Dispose();
        }

    }   
}
