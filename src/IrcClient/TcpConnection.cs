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
        private readonly TcpClient client;
        private readonly Encoding encoding = Encoding.UTF8;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;

        public TcpConnection(ServerConfiguration configuration)
        {
            this.configuration = configuration;
            client = new TcpClient();
        }

        public void Connect()
        {
            client.ConnectAsync(configuration.Hostname, configuration.Port).Wait();
            stream = client.GetStream();
            reader = new BinaryReader(stream, encoding, true);
            writer = new BinaryWriter(stream, encoding, true);
        }

        public void SendMessage(string message)
        {
            writer.Write(message + "\r\n");
        }

        public bool HasMessageToRead()
        {
            return stream.DataAvailable;
        }

        public string ReceiveMessage()
        {
            string str = "";
            char buffer;
            while ((buffer = reader.ReadChar()) != '\n')
            {
                str += buffer;
            }
            return str;
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
