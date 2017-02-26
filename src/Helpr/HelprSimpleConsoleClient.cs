using System;
using Irsee.IrcClient;
using System.Threading;
using System.Collections.Generic;

namespace Irsee.Helpr
{
    public class HelprSimpleConsoleClient
    {
        public static void Main(string[] args)
        {
            var helpr = new User("helpr-bot", "HelpR", "HelpR", nickServUsername: "helpr");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            var client = new IrcClient.IrcClient(freenode);
            freenode.IncomingMessageEvent += (_, x) => Console.WriteLine(x.RawMessage);
            freenode.ConnectAsync().Wait();
            
            while (freenode.Connected)
            {
                string line = Console.ReadLine();
                freenode.SendMessageAsync(new SimpleMessage(line));
            }
            freenode.Disconnect();
            Console.WriteLine("Disconnected");
            Console.ReadLine();
        }
    }
}
