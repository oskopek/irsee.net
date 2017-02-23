using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class PongHandler
    {
        public EventHandler<Message> Delegate()
        {
            return async (obj, e) => {
                var client = obj as IrcClient;
                await client.Servers[0].SendMessageAsync(new Message(Command.PONG, e.Parameters[0]));
            };
        }

        public EventHandler<Message> AutoResponder
        {
            get
            {
                return async (obj, e) =>
                {
                    var client = obj as IrcClient;
                    await client.Servers[0].SendMessageAsync(new Message(Command.PRIVMSG, e.Parameters[0], "Hello there!"));
                };
            }
        }
    }
}
