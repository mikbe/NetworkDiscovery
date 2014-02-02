using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Diagnostics;

namespace NetworkDiscovery
{
    public class BroadcastListener : UDPInfo
    {
        public delegate void ReceivedDataEventHandler(string Data);
        public event ReceivedDataEventHandler ReceivedData;

        private UdpClient _listener = null;

        public BroadcastListener(int port) : base(port: port, address: IPAddress.Any.ToString())
        {
        }

        ~BroadcastListener()
        {
            StopListening();
        }

        public void StopListening()
        {
            if (_listener != null)
            {
                _listener.Close();
                _listener = null;
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
            ReceivedData += new BroadcastListener.ReceivedDataEventHandler(callback);
        }

        private void disconnectCallbacks()
        {
            ReceivedData = null;
        }

        private bool initializeClient()
        {
            bool success = false;
            if (Port != 0 && Address != null)
            {
                _listener = new UdpClient();
                _listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _listener.Client.Bind(EndPoint);
                success = true;
            }

            return success;
        }

        private void beginReceive()
        {
            _listener.BeginReceive(new AsyncCallback(receive), _listener);
        }

        private void receive(IAsyncResult result)
        {
            // The listener can been disposed and nulled between
            // testing if it is null and reading from it.
            // Only way to catch it is by trying to read
            // from it and trapping the error.
            try
            {
                IPEndPoint endPoint = EndPoint; 
                Byte[] receiveData = _listener.EndReceive(result, ref endPoint);
                string receiveString = Encoding.ASCII.GetString(receiveData);

                ReceivedData(receiveString);

                beginReceive();
            }
            catch(NullReferenceException nulled)
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
