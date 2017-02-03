using System;

namespace Irsee.IrcClient
{
    public class Configuration
    {
        public Configuration(User user, string hostname, UInt16 port = 6667, string password = null)
        {
            this.User = user;
            this.Hostname = hostname;
            this.Port = port;
            this.Password = password;
        }

         public User User { get; private set; }
         public string Password { get; private set; }
         public UInt16 Port { get; private set; }
         public string Hostname { get; private set; }
    }   
}
