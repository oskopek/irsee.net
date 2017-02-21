using System;
using Xunit;

namespace Irsee.IrcClient
{
    public class TcpConnectionTest
    {
        [Fact]
        public void TestSimpleConnection()
        {
            string hostname = "chat.freenode.net";
            ushort port = 6667;
            TcpConnection connection = new TcpConnection(new ServerConfiguration(hostname, port));
        }
    }
}
