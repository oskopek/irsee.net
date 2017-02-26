using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class MessageEventArgs : EventArgs
    {
        public Message Message { get; }

        public bool Sent { get; }

        public MessageEventArgs(Message message, bool sent = false)
        {
            Message = message;
            Sent = sent;
        }

    }
}
