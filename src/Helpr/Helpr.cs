using System;
using Irsee.IrcClient;
using System.Threading;
using System.Collections.Generic;

namespace Irsee.Helpr
{
    public class Helpr
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var helpr = new User("helpr-bot", "HelpR", "HelpR", nickServUsername: "helpr");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net", port: 6697, useSSL: true);
            var freenode = new RemoteServer(freenodeConfiguration);
            var client = new IrcClient.IrcClient(freenode);
            freenode.IncomingMessageEvent += x => Console.WriteLine(x.RawMessage);
            client.Dispatcher.AddHandler(Command.PRIVMSG, (_, _2) => Console.WriteLine("PRIVMSG obtained!"));
            freenode.ConnectAsync().Wait();
            Console.WriteLine("Connect finished");
            Thread.Sleep(1000);
            freenode.SendMessageAsync(new SimpleMessage($"PRIVMSG {helpr.Nickname} :Test Message 123")).Wait();
            Console.WriteLine("Send message finished");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Sleep...");
                Thread.Sleep(1000);
            }
            freenode.Disconnect();
            Console.WriteLine("Disconnected");
            Console.ReadLine();
        }
    }
}
