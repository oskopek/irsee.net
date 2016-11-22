using System;
using Xunit;

namespace Irsee.IrcClient
{
    public class IrcClientTest
    {
        [Fact]
        public void TestTest()
        {
            Assert.Equal("Test", IrcClient.TestMethod());
        }
    }
}
