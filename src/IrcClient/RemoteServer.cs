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

        public delegate void MessageListener(Message message);

        public event MessageListener IncomingMessageEvent;

        public RemoteServer(ServerConfiguration configuration)
        {
            Configuration = configuration;
            Connection = new IrcConnection(configuration);
            Connection.IncomingRawMessageEvent += x => IncomingMessageEvent(Message.From(x));
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
            if (Configuration.Password != null)
            {
                await SendMessageAsync(new Message(Command.PASS, Configuration.Password));
            }
            await SendMessageAsync(new Message(Command.NICK, Configuration.User.Nickname));
            await SendMessageAsync(new Message(Command.USER, Configuration.User.Username,
                "hostname", "servername", Configuration.User.Realname));
            await SendMessageAsync(new Message(Command.NICK, Configuration.User.Nickname));
            await NickServAuthenticate(); // TODO: Move this to an END_MOTD event handler
        }

        private async Task NickServAuthenticate()
        {
            if (Configuration.User.NickServPassword != null)
            {
                await SendMessageAsync(new Message(Command.PRIVMSG, "NickServ", "IDENTIFY",
                    Configuration.User.NickServPassword));
            }
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
