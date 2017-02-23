using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class ServerConfiguration
    {
        public User User { get; private set; }

        public ServerConfiguration(User user, string hostname, ushort port = 6667)
        {
            this.User = user;
            this.Hostname = hostname;
            this.Port = port;
        }

        public ushort Port { get; private set; }
        public string Hostname { get; private set; }
    }
}
