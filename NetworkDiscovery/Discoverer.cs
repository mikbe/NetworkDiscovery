using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkDiscovery
{
    public class Discoverer
    {
        public delegate void DiscoveredEventHandler(string Message);
        public event DiscoveredEventHandler Discovered;

        private BroadcastListener _listener;
        private int _port;

        private string _replyMessage;
        private string _ID;

        public string ID
        {
            get { return _ID; }
        }

        public Discoverer(int port = 0)
        {
            _port = port == 0 ? readDefaultPortNumber() : port;
            _ID = this.GetHashCode().ToString();
        }

        ~Discoverer()
        {
            stopListener();
        }

        private void stopListener()
        {
            if (_listener != null)
            {
                _listener.StopListening();
                _listener = null;
                disconnectCallbacks();
            }
        }

        private void attachCallback(Action<string> callback)
        {
            Discovered += new Discoverer.DiscoveredEventHandler(callback);
        }

        private void disconnectCallbacks()
        {
            Discovered = null;
        }


        private void startListener()
        {
            stopListener();
            _listener = new BroadcastListener(_port);
            _listener.Listen(listener_ReceivedData);       
        }

        private void listener_ReceivedData(string data)
        {
            Message msg = new Message(data: data);

            if (!msg.Valid) return;
            if (msg.SourceID == _ID) return;

            switch(msg.Type)
            {
                case "discover":
                    replytoDiscoverMessage();
                    break;
                case "discovered":
                    Discovered(msg.Body);
                    break;
            }
        }

        private void replytoDiscoverMessage()
        {
            BroadcastTalker talker = new BroadcastTalker(_port);
            Message reply = new Message(type: "discovered", sourceID:_ID, body: _replyMessage);
            talker.say(reply.ToString());
            talker = null;
        }

        private int readDefaultPortNumber()
        {
           return Properties.Settings.Default.DefaultPort;
        }

        public void Discover(Action<string> callback)
        {
            attachCallback(callback);
            BroadcastTalker talker = new BroadcastTalker(_port);
            Message reply = new Message(type: "discover", sourceID: _ID, body: "hi");

            talker.say(reply.ToString());
            talker = null;            
        }

        public void PublishSelf(string replyMessage)
        {
            _replyMessage = replyMessage;
            startListener();
        }

        

    }
}
