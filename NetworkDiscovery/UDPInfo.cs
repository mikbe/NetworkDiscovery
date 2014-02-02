using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkDiscovery
{
    public class UDPInfo
    {
        private int     _port;
        private string  _address;
        private bool    _active;

        public UDPInfo(int port, string address)
        {
            _port = port;
            _address = address;
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public IPEndPoint EndPoint
        {
            get { return new IPEndPoint(IPAddress, _port); ; }
        }

        public IPAddress IPAddress
        {
            get { return IPAddress.Parse(_address); }
        }
    }
}
