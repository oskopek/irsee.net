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
            Assert.Equal(399, res[0].LastParameter.Length);
            string line1 = "amlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGV";
            Assert.Equal(line1, res[0].LastParameter);
            string line2 = "AGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWU=";
            Assert.Equal(line2, res[1].LastParameter);
        }

        [Fact]
        public void EncodeLongExactTest()
        {
            List<Message> res = Base64AuthEncoder.Encode("jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jilles\0jilles\0sesame\0jill").ToList();
            Assert.Equal(2, res.Count);
            Assert.Equal(400, res[0].LastParameter.Length);
            string line1 = "amlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbGVzAGppbGxlcwBzZXNhbWUAamlsbA==";
            Assert.Equal(line1, res[0].LastParameter);
            Assert.Equal("+", res[1].LastParameter);
        }

    }
}
