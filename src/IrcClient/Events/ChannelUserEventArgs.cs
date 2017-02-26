using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class ChannelUserEventArgs : ReasonEventArgs
    {

        private UserEventArgs UserEventArgs { get; }
        private ChannelEventArgs ChannelEventArgs { get; }

        public Channel Channel
        {
            get
            {
                return ChannelEventArgs.Channel;
            }
        }

        public User User
        {
            get
            {
                return UserEventArgs.User;
            }
        }
        public ChannelUserEventArgs(Channel channel, User user, string reason = null) : base(reason)
        {
            UserEventArgs = new UserEventArgs(user, reason);
            ChannelEventArgs = new ChannelEventArgs(channel, reason);
        }
    }
}
