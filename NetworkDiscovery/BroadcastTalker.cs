using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkDiscovery
{
    public class BroadcastTalker : UDPInfo
    {

        public BroadcastTalker(int port) : base(port: port, address: IPAddress.Broadcast.ToString())
        { }

        public void say(string message)
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

        private Socket createSocket()
        {
            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp
            );
            socket.EnableBroadcast = true;
            return socket;
        }
    }
}
