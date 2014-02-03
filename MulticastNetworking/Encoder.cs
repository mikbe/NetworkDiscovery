using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MulticastNetworking
{
    public class Encoder
    {
        public static string DecodeMessage(byte[] encodedMessage)
        {
            return Encoding.ASCII.GetString(encodedMessage);
        }

        public static byte[] EncodeMessage(string messageString)
        {
            return Encoding.ASCII.GetBytes(messageString);
        }
    }
}
