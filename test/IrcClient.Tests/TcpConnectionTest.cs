using System;
using Xunit;

namespace Irsee.IrcClient
{
    public class TcpConnectionTest
    {
        [Fact]
        public void TestSimpleConnection()
        {
            string hostname = "leguin.freenode.net";
            ushort port = 6667;
            TcpConnection connection = new TcpConnection(new ServerConfiguration(hostname, port));
            connection.Connect();
            connection.ReceiveMessage();
            connection.ReceiveMessage();
            connection.ReceiveMessage();
            connection.SendMessage("NICK irseetest3");
            connection.SendMessage("USER irsee myhosnet servername irseelib");
            connection.SendMessage("NICK irseetest3");
            string lastline = "";
            while (!lastline.Contains("End of /MOTD"))
            {
                lastline = connection.ReceiveMessage();
            }
            connection.ReceiveMessage();
            connection.SendMessage("PRIVMSG irseetest3 :Hi there!");
            string receivedMsg = connection.ReceiveMessage();
            Assert.Contains("irseetest3 :Hi there!", receivedMsg);
            connection.SendMessage("QUIT");
        }
    }
}
