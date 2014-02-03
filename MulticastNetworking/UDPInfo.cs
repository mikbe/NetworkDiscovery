using System;
using System.Net;
using System.Net.Sockets;

namespace MulticastNetworking
{
    public class UDPInfo
    {
        protected int       _port;
        protected string    _multicastAddressString;
        protected bool      _active;

        public UDPInfo(int port, string multicastAddressString)
        {
            _port = port;
            _multicastAddressString = multicastAddressString;
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public string MulticastAddressString
        {
            get { return _multicastAddressString; }
            set { _multicastAddressString = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public IPEndPoint MulticastEndPoint
        {
            get { return new IPEndPoint(MulticastIpAddress, _port); }
        }

        public IPEndPoint BroadcastEndPoint
        {
            get { return new IPEndPoint(IPAddress.Any, _port); }
        }

        public IPAddress MulticastIpAddress
        {
            get { return IPAddress.Parse(_multicastAddressString); }
            set { _multicastAddressString = value.ToString(); }
        }
    }
}
