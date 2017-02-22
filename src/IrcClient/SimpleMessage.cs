using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient
{
    public class SimpleMessage : IMessage
    {
        public string Message { get; }
        
        public SimpleMessage(string message)
        {
            this.Message = message;
        }
        public string ToRawMessage()
        {
            return Message;
        }
    }
}
