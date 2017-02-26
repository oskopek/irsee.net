using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class NickChangedEventArgs : UserEventArgs
    {
        public string OldNick { get; }
        public string NewNick { get; }

        public NickChangedEventArgs(User user, string oldNick, string newNick) : base(user)
        {
            OldNick = oldNick;
            NewNick = newNick;
        }
    }
}
