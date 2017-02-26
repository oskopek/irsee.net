using System;
using Irsee.IrcClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Irsee.ChatBot
{
    public delegate void CommandDelegate(string parameters);

    public class ChatBot
    {
        public IDictionary<string, CommandDelegate> CommandTable;

        private IrcClient.IrcClient Client { get; }

        private ServerConfiguration Configuration { get; }

        public ChatBot(string commandChar, ServerConfiguration configuration, params string[] channels)
        {

        }

        public ChatBot(ServerConfiguration configuration, params string[] channels)
        {
            Configuration = configuration;
            Client = new IrcClient.IrcClient(new RemoteServer(Configuration));
        }

        public async Task Reconnect()
        {
            Client.DisconnectAll();
            await Task.Delay(1000); // TODO: Is this needed?
            await Client.ConnectAllAsync();
        }


        
    }
}
