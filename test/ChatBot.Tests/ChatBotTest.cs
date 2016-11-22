using System;
using Xunit;

namespace Irsee.ChatBot
{
    public class ChatBotTest
    {
        [Fact]
        public void Test_Main()
        {
            ChatBot.Main(new string[0]);
            Assert.True(true);
        }
    }
}
