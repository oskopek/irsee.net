using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Irsee.IrcClient
{
    public class RemoteServer : IDisposable
    {
        public ServerConfiguration Configuration { get; }

        private IrcConnection Connection { get; }

        public delegate void RawMessageListener(string rawMessage);

        public event RawMessageListener IncomingRawMessageEvent;

        public RemoteServer(ServerConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Connection = new IrcConnection(configuration);
            this.Connection.IncomingRawMessageEvent += x => IncomingRawMessageEvent(x);
        }
        public async Task ConnectAsync()
        {
            await Connection.ConnectAsync();
            new Task(() => Connection.Listen()).Start();
            await Task.Delay(2000);
            await Authenticate();
        }

        private async Task Authenticate()
        {
            await Connection.SendRawMessageAsync($"NICK {Configuration.User.Nickname}");
            await Connection.SendRawMessageAsync($"USER {Configuration.User.Username} hostname servername {Configuration.User.Realname}");
            await Connection.SendRawMessageAsync($"NICK {Configuration.User.Nickname}");
        }

        public async Task SendMessageAsync(IMessage message)
        {
            await Connection.SendRawMessageAsync(message.RawMessage);
        }

        public void SendMessage(IMessage message)
        {
            SendMessageAsync(message).Wait();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public void Disconnect()
        {
            Dispose();
        }
    }
}
