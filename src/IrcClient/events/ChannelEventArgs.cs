using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class ChannelEventArgs : EntityEventArgs
    {
        public Channel Channel { get
            {
                return (Channel) Entity; // TODO: Get rid of this
            } }
        public ChannelEventArgs(Channel channel, string reason = null) : base(channel, reason)
        {
            // intentionally empty
        }
    }
}
