using System;
using Irsee.IrcClient;
using System.Threading;

namespace Irsee.Helpr
{
    public class Helpr
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string hostname = "arcor.de.eu.dal.net";
            ushort port = 6667;
            TcpConnection connection = new TcpConnection(new ServerConfiguration(hostname, port));
            connection.Connect();
            Console.WriteLine(connection.ReceiveMessage());
            Console.WriteLine(connection.ReceiveMessage());
            Console.WriteLine(connection.ReceiveMessage());
//            Console.WriteLine(connection.ReceiveMessage());
            connection.SendMessage("NICK irseetest2");
            Thread.Sleep(100);
            connection.SendMessage("USER irsee myhosnet servername irseelib");
            Thread.Sleep(100);
            connection.SendMessage("NICK irseetest2");
            Console.WriteLine("SENT");
            Console.WriteLine(connection.ReceiveMessage());
            connection.SendMessage("PRIVMSG irseetest2 Hi there!");
            Console.WriteLine(connection.ReceiveMessage());
            connection.SendMessage("QUIT");
            Console.WriteLine("END");
            
            Console.ReadLine();
        }
    }
}
