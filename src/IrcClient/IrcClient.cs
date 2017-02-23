using System;
using System.Collections.Generic;
using Irsee.IrcClient.Events;

namespace Irsee.IrcClient
{
    public class IrcClient
    {

        public EventDispatcher Dispatcher { get; } = EventDispatcher.CreateDefaultDispatcher();

        public IList<RemoteServer> Servers { get; } = new List<RemoteServer>();

        public IrcClient(RemoteServer server) {
            Servers.Add(server);
            server.IncomingMessageEvent += x => Dispatcher.Dispatch(this, x);
        }

        public async void ConnectAsync() {
            foreach (var server in Servers)
            {
                await server.ConnectAsync();
            }
        }

               
    }
}
