using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class ReasonEventArgs : EventArgs
    {

        public string Reason { get; }

        public ReasonEventArgs(string reason)
        {
            Reason = reason;
        }

    }
}
