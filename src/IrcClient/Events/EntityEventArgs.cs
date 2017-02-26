using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class EntityEventArgs : ReasonEventArgs
    {
        public Entity Entity { get; }

        public EntityEventArgs(Entity entity, string reason = null) : base (reason)
        {
            Entity = entity;
        }
    }
}
