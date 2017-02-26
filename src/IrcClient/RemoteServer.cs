using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Irsee.IrcClient.Events;

namespace Irsee.IrcClient
{
    public class RemoteServer : IDisposable
    {
        public ServerConfiguration Configuration { get; }

        private IrcConnection Connection { get; }

        public event ServerEventHandler<Message> IncomingMessageEvent;

        public bool Connected { get
            {
                return Connection.Connected;
            }
        }

        public RemoteServer(ServerConfiguration configuration)
        {
            Configuration = configuration;
            Connection = new IrcConnection(configuration);
            Connection.IncomingRawMessageEvent += msg => IncomingMessageEvent?.Invoke(this, Message.Parse(msg));
        }

        public async Task ConnectAsync()
        {
            await Connection.ConnectAsync();
            new Task(() => Connection.Listen()).Start();
            await Task.Delay(2000); // TODO: Is this needed?
            await Authenticate();
        }

        private async Task Authenticate()
        {
            if (Configuration.UseSASL)
            {
                await SendMessageAsync(new Message(Command.CAP, "LS", "302"));
                // the rest will get handled by the appropriate cap handler
            }
            if (Configuration.Password != null)
            {
                await SendMessageAsync(new Message(Command.PASS, Configuration.Password));
            }
            await SendMessageAsync(new Message(Command.NICK, Configuration.User.Nickname));
            await SendMessageAsync(new Message(Command.USER, Configuration.User.Username,
                "hostname", "servername", Configuration.User.Realname));
            await SendMessageAsync(new Message(Command.NICK, Configuration.User.Nickname));
        }

        internal async Task NickServAuthenticate()
        {
            if (Configuration.IdentifyNickServ && Configuration.User.NickServPassword != null)
            {
                await SendMessageAsync(new Message(Command.PRIVMSG, "NickServ",
                    $"IDENTIFY {Configuration.User.NickServUsername} {Configuration.User.NickServPassword}"));
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
