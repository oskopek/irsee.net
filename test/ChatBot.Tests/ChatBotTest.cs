using System;
using Xunit;

namespace Irsee.ChatBot
{
    public class ChatBotTest
    {
        [Fact]
        public void TestTest()
        {
            Assert.Equal("Test", ChatBot.TestMethod());
        }
    }
}
