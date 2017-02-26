using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Irsee.IrcClient.Events;

namespace Irsee.IrcClient
{
    public class RemoteServerTest
    {
        private static readonly string PASSWORD = Environment.GetEnvironmentVariable("HELPRPASS");
        private static readonly bool ENABLEDIT = bool.Parse(Environment.GetEnvironmentVariable("ENABLEDIT")??bool.FalseString);
        private const int AFTER_TIMEOUT = 10000;
        private const int DURING_TIMEOUT = 25;


        [SkippableFact]
        public void FreenodeConnection()
        {
            Skip.IfNot(ENABLEDIT, reason: "Integration tests not enabled");
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net");
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingMessageEvent += x => {
                if (x.Command == Command.ERR_NICKNAMEINUSE)
                {
                    PongHandler.UniqueNickErrorHandler(freenode, x);
                }
            };
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < DURING_TIMEOUT; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            string messagesConcat = string.Join(", ", rawMessages);
            Assert.True(rawMessages.Any(m => m.Contains("Test Message 123")), $"Messages: \"{messagesConcat}\"");
            Thread.Sleep(AFTER_TIMEOUT);
        }

        [SkippableFact]
        public void FreenodeConnectionWithSSL()
        {
            Skip.IfNot(ENABLEDIT, reason: "Integration tests not enabled");
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingMessageEvent += x => {
                if (x.Command == Command.ERR_NICKNAMEINUSE)
                {
                    PongHandler.UniqueNickErrorHandler(freenode, x);
                }
            };
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < DURING_TIMEOUT; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            string messagesConcat = string.Join(", ", rawMessages);
            Assert.True(rawMessages.Any(m => m.Contains("Test Message 123")), $"Messages: \"{messagesConcat}\"");
            Thread.Sleep(AFTER_TIMEOUT);
        }

        [SkippableFact]
        public void FreenodeConnectionWithNickServ()
        {
            Skip.IfNot(ENABLEDIT, reason: "Integration tests not enabled");
            Skip.If(PASSWORD == null, reason: "NickServ password wasn't found.");
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR", nickServUsername: "helpr", nickServPassword: PASSWORD);
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true, identifyNickServ: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingMessageEvent += x => {
                if (x.Command == Command.ERR_NICKNAMEINUSE)
                {
                    PongHandler.UniqueNickErrorHandler(freenode, x);
                }
            };
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < DURING_TIMEOUT; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            string messagesConcat = string.Join(", ", rawMessages);
            Assert.True(rawMessages.Any(m => m.Contains("You are now identified for") && m.Contains("helpr")), $"Messages: \"{messagesConcat}\"");
            Thread.Sleep(AFTER_TIMEOUT);
        }
    }
}
