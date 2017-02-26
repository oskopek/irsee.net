using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class PrivateMessageEventArgs : MessageEventArgs
    {
        public User From { get; }
        public User To { get; }

        public PrivateMessageEventArgs(User from, User to, Message message, bool sent = false) : base(message, sent)
        {
            From = from;
            To = to;
        }

    }
}
