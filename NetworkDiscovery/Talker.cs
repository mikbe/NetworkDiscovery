using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkDiscovery
{
    public abstract class Talker : UDPInfo
    {

        public Talker(int port, string address)
            : base(port: port, address: address)
        { }

        public void Say(string message)
        {
            Socket socket = createSocket();
            byte[] encodedMessage = encodeMessage(message);

            socket.SendTo(encodedMessage, EndPoint);

            socket.Close();
            socket.Dispose();
        }

        private byte[] encodeMessage(string message)
        {
            return Encoding.ASCII.GetBytes(message);
        }

        abstract protected Socket createSocket();
    }
}
