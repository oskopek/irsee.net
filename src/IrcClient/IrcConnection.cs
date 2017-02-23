using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class IrcConnection : IDisposable
    {
        private TcpConnection Connection { get; set; }
        private StreamWriter Writer { get; set; }
        private StreamReader Reader { get; set; }

        public delegate void RawMessageListener(string rawMessage);

        public event RawMessageListener IncomingRawMessageEvent;

        public IrcConnection(ServerConfiguration configuration)
        {
            this.Connection = new TcpConnection(configuration);
        }

        public async Task ConnectAsync()
        {
            NetworkStream stream = await Connection.ConnectAsync();
            Writer = new StreamWriter(stream);
            Writer.NewLine = "\r\n"; // CRLF, RFC 1459 sec 2.3
            Reader = new StreamReader(stream);
        }

        public void Connect()
        {
            ConnectAsync().Wait();
        }

        public async Task SendRawMessageAsync(string message)
        {
            await Writer?.WriteLineAsync(message);
            await Writer?.FlushAsync();
        }

        public void SendRawMessage(string message)
        {
            SendRawMessageAsync(message).Wait();
        }

        public void Listen()
        {
            if (Reader == null)
            {
                throw new InvalidOperationException("Cannot listen on an unconnected connection.");
            }
            try
            {
                while (!Reader.EndOfStream)
                {
                    string rawMessage = Reader.ReadLine();
                    IncomingRawMessageEvent(rawMessage);
                }
            } catch (IOException e)
            {
                // TODO: Log, at least
                return;
            }
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }

        public void Disconnect()
        {
            Dispose();
        }
    }   
}
