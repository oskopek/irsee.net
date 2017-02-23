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
            List<String> rawMessages = new List<string>();
            Console.WriteLine("Hello World!");
            var helpr = new User("helpr-bot", "HelpR", "HelpR");
            var freenodeConfiguration = new ServerConfiguration(helpr, "leguin.freenode.net");
            var freenode = new RemoteServer(freenodeConfiguration);
            freenode.IncomingRawMessageEvent += x => rawMessages.Add(x);
            freenode.IncomingRawMessageEvent += x => Console.WriteLine(x);
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
            Console.WriteLine(rawMessages[rawMessages.Count - 1].Contains("Test Message 123"));
            Console.ReadLine();
        }
    }
}
