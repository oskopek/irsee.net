using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class Channel : Entity
    {

        public IReadOnlyList<User> Users { get; }
        public string Mode { get; }

        public Channel(string name) : this(name, new List<User>())
        {
            // intentionally empty
        }

        public Channel(string name, List<User> users, string mode = null) : base(name)
        {
            Mode = mode;
            Users = users;
        }

    }
}
