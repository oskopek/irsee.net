using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Irsee.IrcClient.Events;
using System.Threading.Tasks;
using System.Linq;

namespace Irsee.IrcClient
{
    public delegate void ServerEventHandler<T>(RemoteServer server, T args) where T : EventArgs;

    public class IrcClient
    {

        private MessageReceivedEventDispatcher Dispatcher { get; } = MessageReceivedEventDispatcher.CreateDefaultDispatcher();

        protected IList<RemoteServer> Servers { get; }

        public IrcClient(params RemoteServer[] servers)
        {
            Servers = new List<RemoteServer>(servers);
            foreach (var server in servers)
            {
                server.IncomingMessageEvent += (remote, msg) => HandleIncomingMessage(remote as RemoteServer, new MessageEventArgs(msg, sent: false));
            }
        }

        public async Task ConnectAllAsync() {
            await Task.WhenAll(from server in Servers select server.ConnectAsync());
        }

        public event ServerEventHandler<MessageEventArgs> MessageEvent;

        private void HandleIncomingMessage(RemoteServer server, MessageEventArgs args)
        {
            // TODO!!!: Refactor so that the new event args can parse themselves from server and mesasge args
            // use the event dispatcher with a callback to the client

            Message msg = args.Message;
            OnMessage(server, args);
            
            if (msg.Command == Command.PRIVMSG)
            {
                // TODO
            }
            if (msg.Command.IsErrorCode())
            {
                OnError(server, new ReasonEventArgs(msg.LastParameter)); 
            }
            if (msg.Command == Command.NOTICE)
            {
                OnNotice(server, new ReasonEventArgs(msg.LastParameter));
            }
            if (msg.Command == Command.NICK)
            {
                // TODO: get old user and nick from from and new nick as lastparam
                //OnNickChanged(server, new NickChangedEventArgs())
            }
            if (msg.Command == Command.QUIT)
            {
                // TODO: get user from from and msg as lastparam.. get chan from list of chans
                //OnUserQuit(server, new ChannelUserEventArgs());
            }
            if (msg.Command == Command.TOPIC)
            {
                // get old topic from chan, get new from lastparam
            }
            // TODO: OnQuit, OnConnected, OnKicked, OnParted, OnJoined, OnUserJoined/Parted/Kicked
        }

        protected virtual void OnMessage(RemoteServer server, MessageEventArgs args)
        {
            MessageEvent?.Invoke(server, args);
            if (args.Sent)
            {
                MessageSentEvent?.Invoke(server, args);
            } else
            {
                MessageReceivedEvent?.Invoke(server, args);
            }
        }

        public event ServerEventHandler<MessageEventArgs> MessageReceivedEvent;
        public event ServerEventHandler<MessageEventArgs> MessageSentEvent;

        public event ServerEventHandler<PrivateMessageEventArgs> PrivateMessageEvent;

        protected virtual void OnPrivateMessage(RemoteServer server, PrivateMessageEventArgs args)
        {
            PrivateMessageEvent?.Invoke(server, args);
            if (args.Sent)
            {
                PrivateMessageSentEvent?.Invoke(server, args);
            }
            else
            {
                PrivateMessageReceivedEvent?.Invoke(server, args);
            }
        }

        public event ServerEventHandler<PrivateMessageEventArgs> PrivateMessageReceivedEvent;
        public event ServerEventHandler<PrivateMessageEventArgs> PrivateMessageSentEvent;

        public event ServerEventHandler<ChannelTopicChangedEventArgs> ChannelTopicChangedEvent;

        protected virtual void OnChannelTopicChanged(RemoteServer server, ChannelTopicChangedEventArgs args)
        {
            ChannelTopicChangedEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelUserEventArgs> UserJoinedEvent;

        protected virtual void OnUserJoined(RemoteServer server, ChannelUserEventArgs args)
        {
            UserJoinedEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelUserEventArgs> UserPartedEvent;

        protected virtual void OnUserParted(RemoteServer server, ChannelUserEventArgs args)
        {
            UserPartedEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelUserEventArgs> UserKickedEvent;

        protected virtual void OnUserKicked(RemoteServer server, ChannelUserEventArgs args)
        {
            UserKickedEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelUserEventArgs> UserQuitEvent;

        protected virtual void OnUserQuit(RemoteServer server, ChannelUserEventArgs args)
        {
            UserQuitEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelEventArgs> JoinedChannelEvent;

        protected virtual void OnJoinedChannel(RemoteServer server, ChannelEventArgs args)
        {
            JoinedChannelEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelEventArgs> PartedChannelEvent;

        protected virtual void OnPartedChannel(RemoteServer server, ChannelEventArgs args)
        {
            PartedChannelEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ChannelEventArgs> KickedFromChannelEvent;

        protected virtual void OnKickedFromChannel(RemoteServer server, ChannelEventArgs args)
        {
            KickedFromChannelEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ModeChangedEventArgs> ModeChangedEvent;

        protected virtual void OnModeChanged(RemoteServer server, ModeChangedEventArgs args)
        {
            ModeChangedEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<NickChangedEventArgs> NickChangedEvent;

        protected virtual void OnNickChanged(RemoteServer server, NickChangedEventArgs args)
        {
            NickChangedEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<EventArgs> ConnectedEvent;

        protected virtual void OnConnected(RemoteServer server)
        {
            ConnectedEvent?.Invoke(server, EventArgs.Empty);
        }

        public event ServerEventHandler<EventArgs> QuitEvent;

        protected virtual void OnQuit(RemoteServer server)
        {
            QuitEvent?.Invoke(server, EventArgs.Empty);
        }

        public event ServerEventHandler<ReasonEventArgs> ErrorEvent;

        protected virtual void OnError(RemoteServer server, ReasonEventArgs args)
        {
            ErrorEvent?.Invoke(server, args);
        }

        public event ServerEventHandler<ReasonEventArgs> NoticeEvent;

        protected virtual void OnNotice(RemoteServer server, ReasonEventArgs args)
        {
            NoticeEvent?.Invoke(server, args);
        }

    }
}
