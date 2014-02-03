using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastNetworking
{

    public class MulticastTalker : MulticastInfo
    {

        private int _ttl;

        public MulticastTalker(int port = 0, string address = "", int TTL = 0)
            : base(port: port, multicastAddressString: address)
        {
            if (TTL == 0)
            {
                _ttl = Properties.Settings.Default.TTL;
            }
            else
            {
                _ttl = TTL;
            }

            if (address == "")
            {
                _multicastAddressString = Properties.Settings.Default.MulticastIP;
            }
            
            if (port == 0)
            {
                _port = Properties.Settings.Default.Port;
            }
        }

        public void Say(string message)
        {
            UdpClient client = new UdpClient();
            client.JoinMulticastGroup(MulticastIpAddress);

            byte[] encodedMessage = Encoder.EncodeMessage(message);
            client.Send(encodedMessage, encodedMessage.Length, MulticastEndPoint);
            client.Close();
        }

    }
}
