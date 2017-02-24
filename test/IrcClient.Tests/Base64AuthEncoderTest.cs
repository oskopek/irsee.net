using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Irsee.IrcClient
{
    public class Base64AuthEncoderTest
    {
        [Fact]
        public void EncodeStringSimpleTest() {
            Assert.Equal("amlsbGVzAGppbGxlcwBzZXNhbWU=", Base64AuthEncoder.EncodeString("jilles\0jilles\0sesame"));
        }

        [Fact]
        public void EncodeSimpleTest()
        {
            Assert.Equal("amlsbGVzAGppbGxlcwBzZXNhbWU=", Base64AuthEncoder.Encode("jilles\0jilles\0sesame").ToList()[0].LastParameter);
        }

        [Fact]
        public void EncodeEmptyTest()
        {
            Assert.Equal("+", Base64AuthEncoder.Encode("").ToList()[0].LastParameter);
        }

        [Fact]
        public void EncodeLongTest()
        {
            List<Message> res = Base64AuthEncoder.Encode("jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame").ToList();
            Assert.Equal(2, res.Count);
            Assert.Equal("amlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUA", res[0].LastParameter);
            Assert.Equal("amlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUA", res[1].LastParameter);
        }

        [Fact]
        public void EncodeLongExactTest()
        {
            List<Message> res = Base64AuthEncoder.Encode("jillesjillessesame").ToList();
            Assert.Equal("amlsbGVzAGppbGxlcwBzZXNhbWU=", res[0].LastParameter);
            Assert.Equal("+", res[1].LastParameter);
            Assert.Equal(2, res.Count);
        }

    }
}
