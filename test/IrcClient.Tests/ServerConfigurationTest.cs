using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Irsee.IrcClient
{
    public class ServerConfigurationTest
    {
        [Fact]
        public void TestSimpleConnection()
        {
            string hostname = "chat.freenode.net";
            ushort port = 6667;
            TcpConnection connection = new TcpConnection(new ServerConfiguration(hostname, port));
            connection.Connect();
            Thread.Sleep(2000);
            connection.SendMessage("NICK irseetest");
            connection.SendMessage("USER irsee myhostname.net servername irseelib");
            connection.ReceiveMessage();
            connection.ReceiveMessage();
            connection.ReceiveMessage();
            connection.SendMessage("PRIVMSG irsee2 Hi there!");
            connection.ReceiveMessage();
            Assert.Contains("Hi there!", connection.ReceiveMessage());
        }
    }
}
