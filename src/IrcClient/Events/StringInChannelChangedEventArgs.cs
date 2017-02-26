using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class StringInChannelChangedEventArgs : ChannelEventArgs
    {
        public string Old { get; }
        public string New { get; }

        public StringInChannelChangedEventArgs(Channel channel, string Old, string New) : base(channel)
        {
            this.Old = Old;
            this.New = New;
        }
    }
}
