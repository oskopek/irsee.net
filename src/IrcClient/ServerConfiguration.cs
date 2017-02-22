using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class ServerConfiguration
    {
        public ServerConfiguration(string hostname, ushort port = 6667)
        {
            this.Hostname = hostname;
            this.Port = port;
        }

        public ushort Port { get; private set; }
        public string Hostname { get; private set; }
    }
}
