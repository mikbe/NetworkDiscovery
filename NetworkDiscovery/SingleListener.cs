using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkDiscovery
{
    /// <summary>
    /// Opens a broadcast listener on a port that no one else can listen on
    /// </summary>
    public class SingleListener : Listener
    {
        public SingleListener(int port)
            : base(port: port, address: IPAddress.Any.ToString())
        { }

        override protected bool initializeClient()
        {
            bool success = false;
            if (_port != 0 && _address != null)
            {
                try
                {
                    _client = new UdpClient(EndPoint);
                    success = true;
                }
                catch (Exception e)
                {
                }

            }

            return success;
        }
    }
}
