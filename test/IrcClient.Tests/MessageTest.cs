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
            Message expected = new Message("kornbluth.freenode.net", Command.PING, "12345312");
            string rawMessage = ":kornbluth.freenode.net PING :12345312";
            Message msg = Message.From(rawMessage);
            AssertDeepEquals(expected, msg, rawMessage);
        }

        [Fact]
        public void FromComplexTest()
        {
            Message expected = new Message("kornbluth.freenode.net", Command.PRIVMSG, "irseebot3", "Test 123 123");
            string rawMessage = ":kornbluth.freenode.net PRIVMSG irseebot3 :Test 123 123";
            Message msg = Message.From(rawMessage);
            AssertDeepEquals(expected, msg, rawMessage);
        }

        [Fact]
        public void FromReplyTest()
        {
            Message expected = new Message("hobana.freenode.net", Command.ERR_NOSUCHNICK, "oskopek2", "oskopek3", "No such nick / channel");
            string rawMessage = ":hobana.freenode.net 401 oskopek2 oskopek3 :No such nick / channel";
            Message msg = Message.From(rawMessage);
            AssertDeepEquals(expected, msg, rawMessage);
        }

        [Fact]
        public void FromReplyUnrecognizedTest()
        {
            Message expected = new Message("hobana.freenode.net", (Command) 342, "oskopek2", "oskopek3", "Oh well");
            string rawMessage = ":hobana.freenode.net 342 oskopek2 oskopek3 :Oh well";
            Message msg = Message.From(rawMessage);
            AssertDeepEquals(expected, msg, rawMessage);
        }

        private void AssertDeepEquals(Message expected, Message msg, string rawMessage)
        {
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
