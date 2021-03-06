﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Diagnostics;

namespace NetworkDiscovery
{
    public class BroadcastListener : Listener
    {

        public BroadcastListener(int port) : base(port: port, address: IPAddress.Any.ToString())
        {
        }

        ~BroadcastListener()
        {
            StopListening();
        }

        override protected bool initializeClient()
        {
            bool success = false;
            //Console.WriteLine("broadcastListener.address: " + this.Address);
            if (_port != 0 && _address != null)
            {
                try
                {
                    _client = new UdpClient();
                    _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    _client.Client.Bind(this.EndPoint);
                    success = true;
                    //Console.WriteLine("Worked: " + _port.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine("error: " + e.Message);
                }
                
            }

            return success;
        }


    }
}
