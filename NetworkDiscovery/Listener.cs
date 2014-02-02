using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkDiscovery
{

    public abstract class Listener : UDPInfo
    {
        public delegate void ReceivedDataEventHandler(string Data);
        public event ReceivedDataEventHandler ReceivedData;

        protected UdpClient _client = null;

        public Listener(int port, string address)
            : base(port: port, address: address)
        {
        }

        ~Listener()
        {
            StopListening();
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

        public void Listen(Action<string> callback)
        {
            StopListening();
            initializeClient();
            attachCallback(callback);
            beginReceive();
        }

        private void attachCallback(Action<string> callback)
        {
            ReceivedData += new Listener.ReceivedDataEventHandler(callback);
        }

        private void disconnectCallbacks()
        {
            ReceivedData = null;
        }

        abstract protected bool initializeClient();

        private void beginReceive()
        {
            _client.BeginReceive(new AsyncCallback(receive), _client);
        }

        private void receive(IAsyncResult result)
        {
            // The listener can been disposed and/or nulled between
            // testing if it is nulled/disposed and reading from it.
            // The only way to catch it is by trying to read
            // from it and trapping the error.
            try
            {
                IPEndPoint endPoint = this.EndPoint;
                Byte[] receiveData = _client.EndReceive(result, ref endPoint);
                string receiveString = Encoding.ASCII.GetString(receiveData);

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
                Console.WriteLine("Error:");
                Console.WriteLine(e.Message);
                throw e;
            }
        }


    }
}
