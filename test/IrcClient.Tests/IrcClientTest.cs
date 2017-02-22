using System;
using Xunit;

namespace Irsee.IrcClient
{
    public class IrcClientTest
    {
        [Fact]
        public void TestTest()
        {
            new IrcClient().Connect();
            Assert.Equal("Test", "Test");
        }

        [Fact]
        public void FreenodeConnection()
        {
            //Given
            string freenodeServer = "chat.freenode.net";
            //When

            //Then
            Assert.True(true);
        }
    }
}
