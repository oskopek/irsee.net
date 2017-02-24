using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class ServerConfiguration
    {
        public User User { get; }

        public ushort Port { get; }
        public string Hostname { get; }

        public string Password { get; }

        public bool UseSSL { get; }

        public ServerConfiguration(User user, string hostname, ushort port = 6667, string password = null, bool useSSL = false)
        {
            User = user;
            Hostname = hostname;
            Port = port;
            Password = password;
            UseSSL = useSSL;
        }

    }
}
