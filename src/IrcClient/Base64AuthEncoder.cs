using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Irsee.IrcClient
{
    public class Base64AuthEncoder
    {

        private static Encoding Encoding = Encoding.UTF8;

        public static IEnumerable<Message> Encode(string message)
        {
            string base64 = EncodeString(message);
            int offset = 0;
            int chunkSize = 400;
            while (offset + chunkSize - 1 < message.Length)
            {
                yield return new Message(Command.AUTHENTICATE, base64.Substring(offset, chunkSize - 1));
                offset += chunkSize;
            }
            if (offset < message.Length)
            {
                yield return new Message(Command.AUTHENTICATE, base64.Substring(offset));
            } else if (offset == message.Length)
            {
                yield return new Message(Command.AUTHENTICATE, "+");
            }
        }


        public static string EncodeString(string str)
        {
            byte[] encoded = Encoding.GetBytes(str);
            return Convert.ToBase64String(encoded);
        }
    }
}
