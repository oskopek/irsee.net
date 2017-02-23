using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class SimpleMessage : IMessage
    {
        public string RawMessage { get; }
        
        public SimpleMessage(string message)
        {
            this.RawMessage = message;
        }
    }
}
