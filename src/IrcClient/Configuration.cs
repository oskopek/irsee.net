using System;

namespace Irsee.IrcClient
{
    public class Configuration
    {
        public Configuration(User user, string password = null)
        {
            this.User = user;
            this.Password = password;
        }

         public User User { get; private set; }
         public string Password { get; private set; }
         
    }   
}
