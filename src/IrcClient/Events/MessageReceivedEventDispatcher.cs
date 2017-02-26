using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    class MessageReceivedEventDispatcher
    {
        private IDictionary<Command, List<ServerEventHandler<Message>>> handlers { get; set; }
            = new Dictionary<Command, List<ServerEventHandler<Message>>>();

        public void AddHandler(Command command, ServerEventHandler<Message> handler)
        {
            List<ServerEventHandler<Message>> handlerList;
            if (!TryGetValue(command, out handlerList))
            {
                handlerList = new List<ServerEventHandler<Message>>(1);
            }
            handlerList.Add(handler);
            handlers[command] = handlerList;
        }

        public void Dispatch(RemoteServer source, Message message)
        {
            List<ServerEventHandler<Message>> handlers;
            // Console.WriteLine($"DEBUG: Dispatching {message}...");
            if (TryGetValue(message.Command, out handlers))
            {
                foreach (var handler in handlers)
                {
                    Console.WriteLine($"DEBUG: Dispatching {message} to {handler}");
                    handler(source, message);
                }
            }
        }

        public List<ServerEventHandler<Message>> this[Command index]
        {
            get { return handlers[index]; }
            set { handlers[index] = value; }
        }

        public bool TryGetValue(Command command, out List<ServerEventHandler<Message>> handlerList)
        {
            return handlers.TryGetValue(command, out handlerList);
        }

        public bool RemoveHandler(Command command, ServerEventHandler<Message> handler)
        {
            List<ServerEventHandler<Message>> handlerList;
            if (TryGetValue(command, out handlerList))
            {
                handlerList.Remove(handler);
                handlers[command] = handlerList;
                return true;
            }
            return false;
        }

        public bool RemoveAllHandlers(Command command)
        {
            return handlers.Remove(command);
        }

        public static MessageReceivedEventDispatcher CreateDefaultDispatcher()
        {
            MessageReceivedEventDispatcher dispatcher = new MessageReceivedEventDispatcher();
            //dispatcher.AddHandler(Command.ERR_NICKNAMEINUSE, ServerEventHandlers.UniqueNickErrorHandler);
            dispatcher.AddHandler(Command.CAP, ServerEventHandlers.CapSaslHandler);
            dispatcher.AddHandler(Command.PING, ServerEventHandlers.PingAutoResponder);
            dispatcher.AddHandler(Command.AUTHENTICATE, ServerEventHandlers.AuthenticateSaslHandler);
            dispatcher.AddHandler(Command.RPL_ENDOFMOTD, ServerEventHandlers.MOTDEndHandler);
            // dispatcher.AddHandler(Command.PRIVMSG, ServerEventHandlers.AutoResponder);
            return dispatcher;
        }
    }
}
