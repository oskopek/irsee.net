using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class IrcConnection
    {
        private TcpConnection Connection { get; set; }
        private StreamWriter Writer { get; set; }
        private StreamReader Reader { get; set; }

        public IrcConnection(ServerConfiguration configuration)
        {
            this.Connection = new TcpConnection(configuration);
        }

        public async Task ConnectAsync()
        {
            NetworkStream stream = await Connection.ConnectAsync();
            Writer = new StreamWriter(stream);
            Reader = new StreamReader(stream);
        }

        public async void Connect()
        {
            await ConnectAsync();
        }

        public async Task SendRawMessageAsync(string message)
        {
            await Writer.WriteLineAsync(message);
            await Writer.FlushAsync();
        }

        public void SendRawMessage(string message)
        {
            SendRawMessageAsync(message).Wait();
        }
        
    }   
}
