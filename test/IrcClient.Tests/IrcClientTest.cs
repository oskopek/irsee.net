using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Irsee.IrcClient.Events;

namespace Irsee.IrcClient
{
    public class IrcClientTest
    {
        private static readonly string PASSWORD = Environment.GetEnvironmentVariable("HELPRPASS");
        private static readonly bool ENABLEDIT = bool.Parse(Environment.GetEnvironmentVariable("ENABLEDIT") ?? bool.FalseString);
        private const int AFTER_TIMEOUT = 10000;
        private const int DURING_TIMEOUT = 25;

        [SkippableFact]
        public void FreenodeConnectionWithSASLandCAP()
        {
            Skip.IfNot(ENABLEDIT, reason: "Integration tests not enabled");
            Skip.If(PASSWORD == null, reason: "NickServ password wasn't found.");
            List<string> rawMessages = new List<string>();
            var helpr = new User("helpr-bot", username: "HelpR", realname: "HelpR", nickServUsername: "helpr", nickServPassword: PASSWORD);
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true, identifyNickServ: true, useSASL: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            var client = new IrcClient(freenode);
            client.Dispatcher.AddHandler(Command.ERR_NICKNAMEINUSE, PongHandler.UniqueNickErrorHandler);
            freenode.IncomingMessageEvent += x => rawMessages.Add(x.RawMessage);
            client.ConnectAsync().Wait();
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            for (int i = 0; i < DURING_TIMEOUT; i++)
            {
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            string messagesConcat = string.Join(", ", rawMessages);
            Assert.True(rawMessages.Any(m => m.Contains("SASL authentication successful")), $"Messages: \"{messagesConcat}\"");
            Assert.True(rawMessages.Any(m => m.Contains("You are now logged in as") && m.Contains("helpr")), $"Messages: \"{messagesConcat}\"");
            Thread.Sleep(AFTER_TIMEOUT);
        }
    }
}
