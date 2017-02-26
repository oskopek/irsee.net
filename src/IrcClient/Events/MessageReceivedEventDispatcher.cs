using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    class MessageReceivedEventDispatcher : GenericDispatcher<Command, Message, RemoteServer>
    {

        public void Dispatch(RemoteServer source, Message message)
        {
            Dispatch(message.Command, source, message);
        }

        public static MessageReceivedEventDispatcher CreateDefaultDispatcher()
        {
            MessageReceivedEventDispatcher dispatcher = new MessageReceivedEventDispatcher();
            //dispatcher.AddHandler(Command.ERR_NICKNAMEINUSE, ServerEventHandlers.UniqueNickErrorHandler);
            dispatcher.AddHandler(Command.CAP, new Dispatcher(ServerEventHandlers.CapSaslHandler));
            dispatcher.AddHandler(Command.PING, new Dispatcher(ServerEventHandlers.PingAutoResponder));
            dispatcher.AddHandler(Command.AUTHENTICATE, new Dispatcher(ServerEventHandlers.AuthenticateSaslHandler));
            dispatcher.AddHandler(Command.RPL_ENDOFMOTD, new Dispatcher(ServerEventHandlers.MOTDEndHandler));
            // dispatcher.AddHandler(Command.PRIVMSG, ServerEventHandlers.AutoResponder);
            return dispatcher;
        }
    }
}
