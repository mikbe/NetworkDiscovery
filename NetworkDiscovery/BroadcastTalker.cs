using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkDiscovery
{
    public class BroadcastTalker : Talker
    {

        public BroadcastTalker(int port) : base(port: port, address: IPAddress.Broadcast.ToString())
        { }

        override protected Socket createSocket()
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
