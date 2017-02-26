using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class EventDispatcher
    {
        private IDictionary<Command, List<EventHandler<Message>>> handlers { get; set; }
            = new Dictionary<Command, List<EventHandler<Message>>>();

        public void AddHandler(Command command, EventHandler<Message> handler)
        {
            List<EventHandler<Message>> handlerList;
            if (!TryGetValue(command, out handlerList))
            {
                handlerList = new List<EventHandler<Message>>(1);
            }
            handlerList.Add(handler);
            handlers[command] = handlerList;
        }

        public void Dispatch(Message message)
        {
            Dispatch(this, message);
        }

        public void Dispatch(object source, Message message)
        {
            List<EventHandler<Message>> handlers;
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

        public List<EventHandler<Message>> this[Command index]
        {
            get { return handlers[index]; }
            set { handlers[index] = value; }
        }

        public bool TryGetValue(Command command, out List<EventHandler<Message>> handlerList)
        {
            return handlers.TryGetValue(command, out handlerList);
        }

        public bool RemoveHandler(Command command, EventHandler<Message> handler)
        {
            List<EventHandler<Message>> handlerList;
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

        public static EventDispatcher CreateDefaultDispatcher()
        {
            EventDispatcher dispatcher = new EventDispatcher();
            // TODO: Add default handlers
            //dispatcher.AddHandler(Command.ERR_NICKNAMEINUSE, PongHandler.UniqueNickErrorHandler);
            dispatcher.AddHandler(Command.CAP, PongHandler.CapSaslHandler);
            dispatcher.AddHandler(Command.PING, PongHandler.PingAutoResponder);
            dispatcher.AddHandler(Command.AUTHENTICATE, PongHandler.AuthenticateSaslHandler);
            dispatcher.AddHandler(Command.RPL_ENDOFMOTD, PongHandler.MOTDEndHandler);
            // dispatcher.AddHandler(Command.PRIVMSG, new PongHandler().AutoResponder);
            return dispatcher;
        }
    }
}
