using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Irsee.IrcClient
{
    public class MessageTest
    {
        [Fact]
        public void FromEmptyTest()
        {
            Assert.Throws<ArgumentException>(() => Message.From(null));
            Assert.Throws<ArgumentException>(() => Message.From(""));
            Assert.Throws<ArgumentException>(() => Message.From(": "));
        }

        [Fact]
        public void FromSimpleTest()
        {
            Message expected = new Message(Command.PING, new List<string> { "12345312" }, "kornbluth.freenode.net");
            string rawMessage = ":kornbluth.freenode.net PING :12345312";
            Message msg = Message.From(rawMessage);
            Assert.NotNull(msg);
            Assert.Equal(rawMessage, msg.RawMessage);
            Assert.Equal(expected.RawMessage, msg.RawMessage);
            Assert.Equal(expected.Prefix, msg.Prefix);
            Assert.Equal(expected.Command, msg.Command);
            Assert.Equal(expected.Parameters, msg.Parameters);
            Assert.Equal(expected, msg);
            Assert.True(expected == msg);
            Assert.False(expected != msg);
            Assert.Equal(expected.GetHashCode(), msg.GetHashCode());
        }

        [Fact]
        public void FromComplexTest()
        {
            Message expected = new Message(Command.PRIVMSG, new List<string> { "irseebot3", "Test 123 123" }, "kornbluth.freenode.net");
            string rawMessage = ":kornbluth.freenode.net PRIVMSG irseebot3 :Test 123 123";
            Message msg = Message.From(rawMessage);
            Assert.NotNull(msg);
            Assert.Equal(rawMessage, msg.RawMessage);
            Assert.Equal(expected.RawMessage, msg.RawMessage);
            Assert.Equal(expected.Prefix, msg.Prefix);
            Assert.Equal(expected.Command, msg.Command);
            Assert.Equal(expected.Parameters, msg.Parameters);
            Assert.Equal(expected, msg);
            Assert.True(expected == msg);
            Assert.False(expected != msg);
            Assert.Equal(expected.GetHashCode(), msg.GetHashCode());
        }
    }
}
