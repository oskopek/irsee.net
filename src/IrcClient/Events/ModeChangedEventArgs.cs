using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class ModeChangedEventArgs : StringInChannelChangedEventArgs
    {
        public ModeChangedEventArgs(Channel channel, string oldMode, string newMode) : base(channel, oldMode, newMode)
        {
            // intentionally empty
        }
    }
}
