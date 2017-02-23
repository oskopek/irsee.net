﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Irsee.IrcClient
{
    public class RemoteServerTest
    {
		[Fact]
        public void FreenodeConnectionTest()
        {
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", "HelpR", "HelpR");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net");
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingRawMessageEvent += x => rawMessages.Add(x);
            freenode.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            Assert.Contains("Test Message 123", rawMessages[rawMessages.Count - 1]);
        }
    }
}
