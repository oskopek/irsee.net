using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class ServerConfiguration
    {
        public User User { get; private set; }

        public ushort Port { get; private set; }
        public string Hostname { get; private set; }

        public string Password { get; private set; }

        public ServerConfiguration(User user, string hostname, ushort port = 6667, string password = null)
        {
            User = user;
            Hostname = hostname;
            Port = port;
            Password = password;
        }

    }
}
