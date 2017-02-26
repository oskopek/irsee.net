using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class ChannelTopicChangedEventArgs : StringInChannelChangedEventArgs
    {
        public ChannelTopicChangedEventArgs(Channel channel, string oldTopic, string newTopic) : base(channel, oldTopic, newTopic)
        {
            // intentionally empty
        }
    }
}
