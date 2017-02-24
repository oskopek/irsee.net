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
        public bool UseSASL { get; }
        public bool IdentifyNickServ { get; }

        public ServerConfiguration(User user, string hostname, ushort port = 6667, string password = null, bool useSSL = false,
            bool identifyNickServ = false, bool useSASL = true)
        {
            User = user;
            Hostname = hostname;
            Port = port;
            Password = password;
            UseSSL = useSSL;
            IdentifyNickServ = identifyNickServ;

            UseSASL = useSASL;
            if (!identifyNickServ && UseSASL)
            {
                Console.Error.WriteLine("SASL cannot be enabled while IdentifyNickServ is disabled. Disabling SASL.");
                UseSASL = false;
            }

        }

    }
}
