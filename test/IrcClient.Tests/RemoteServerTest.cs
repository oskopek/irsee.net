using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Irsee.IrcClient
{
    public class RemoteServerTest
    {
		[Fact(Skip = "Too unreliable at the moment")]
        public void FreenodeConnectionTest()
        {
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net");
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            Assert.True(rawMessages.Any(m => m.Contains("Test Message 123")));
        }

        [Fact(Skip = "Too unreliable at the moment")]
        public void FreenodeConnectionTestWithSSL()
        {
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            Assert.True(rawMessages.Any(m => m.Contains("Test Message 123")));
        }

        [Fact(Skip = "Too unreliable at the moment and figure out a way to store the password on CI correctly")]
        public void FreenodeConnectionTestWithNickServ()
        {
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR", nickServUsername: "helpr", nickServPassword: Environment.GetEnvironmentVariable("HELPRPASS"));
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true, identifyNickServ: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            Assert.True(rawMessages.Any(m => m.Contains("You are now identified for") && m.Contains("helpr")));
        }
    }
}
