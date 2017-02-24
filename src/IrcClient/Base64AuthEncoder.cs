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
            while (offset + chunkSize - 1 < base64.Length && offset + chunkSize != base64.Length)
            {
                yield return new Message(Command.AUTHENTICATE, base64.Substring(offset, chunkSize - 1));
                offset += chunkSize;
            }

            if (offset + chunkSize == base64.Length) // the last 400 long message
            {
                yield return new Message(Command.AUTHENTICATE, base64.Substring(offset, chunkSize));
                offset += chunkSize;
            }

            if (offset < base64.Length)
            {
                yield return new Message(Command.AUTHENTICATE, base64.Substring(offset));
            } else if (offset == base64.Length)
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
