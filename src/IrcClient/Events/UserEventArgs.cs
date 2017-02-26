using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class UserEventArgs : EntityEventArgs
    {
        public User User
        {
            get
            {
                return (User)Entity; // TODO: Get rid of this
            }
        }
        public UserEventArgs(User user, string reason = null) : base(user, reason)
        {
            // intentionally empty
        }
    }
}
