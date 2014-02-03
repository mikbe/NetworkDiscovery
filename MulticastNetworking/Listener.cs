using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastNetworking
{
    public class Listener : UDPInfo
    {
        public delegate void ReceivedDataEventHandler(string Data);
        public event ReceivedDataEventHandler ReceivedData;

        private UdpClient _client = null;
        int _ttl;

        public Listener(int port = 0, string multicastAddressString = "", int TTL = 0) : base(port: port, multicastAddressString: multicastAddressString)
        {
            if (TTL == 0)
            {
                _ttl = Properties.Settings.Default.TTL;
            }
            else
            {
                _ttl = TTL;
            }

            if (multicastAddressString == "")
            {
                _multicastAddressString = Properties.Settings.Default.MulticastIP;
            }

            if (port == 0)
            {
                _port = Properties.Settings.Default.Port;
            }

        }

        ~Listener()
        {
            StopListening();
        }

        private void initializeClient()
        {
            _client = new UdpClient();
            _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _client.JoinMulticastGroup(MulticastIpAddress);
            _client.Client.Bind(BroadcastEndPoint);
        }

        public void StopListening()
        {
            if (_client != null)
            {
                _client.Close();
                _client = null;
                disconnectCallbacks();
            }
            Active = false;
        }

        /// <summary>
        /// Starts the listener in a non-blocking thread.
        /// </summary>
        /// <param name="callback"></param>
        public void Listen(Action<string> callback)
        {
            StopListening();
            initializeClient();
            attachCallback(callback);
            beginReceive();
            _active = true;
        }

        private void attachCallback(Action<string> callback)
        {
            if (callback == null) return;
            ReceivedData += new Listener.ReceivedDataEventHandler(callback);
        }

        private void disconnectCallbacks()
        {
            ReceivedData = null;
        }

        private void beginReceive()
        {
            _client.BeginReceive(new AsyncCallback(receive), _client);
        }

        protected void receive(IAsyncResult result)
        {
            // The listener can been disposed and/or nulled between
            // testing if it is nulled/disposed and reading from it.
            // The only way to catch it is by trying to read
            // from it and trapping the error.
            try
            {
                IPEndPoint endPoint = BroadcastEndPoint;
                Byte[] receiveData = _client.EndReceive(result, ref endPoint);
                string receiveString = Encoder.DecodeMessage(receiveData);

                ReceivedData(receiveString);

                beginReceive();
            }
            catch (NullReferenceException nulled)
            {
            }
            catch (ObjectDisposedException disposed)
            {
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error:");
                //Console.WriteLine(e.Message);
                throw e;
            }
        }


    }

}