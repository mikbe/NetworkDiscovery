using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkDiscovery
{

    public class Message
    {
        private string _type;
        private string _sourceID;
        private string _body;
        private bool _valid;

        public bool Valid
        {
            get { return _valid; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        
        public string SourceID
        {
            get { return _sourceID; }
            set { _sourceID = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        override public string ToString()
        {
            string toString = "";
            if (_type != "" && _body != "")
            {
                toString = _type + "~" + _sourceID + "~" + _body;
            }
            return toString;
        }

        public Message(string data = "", string type = "", string body = "", string sourceID = "")
        {
            if (data != "")
            { parseMessage(data); }
            else if (type != "" && sourceID != "" && body != "")
            {
                buildMessage(type, sourceID, body);
            }
            else
            {
                _valid = false;
            }
        }

        private void buildMessage(string type, string sourceID, string body)
        {
            _type = type;
            _sourceID = sourceID;
            _body = body;
            _valid = true;
        }

        private void parseMessage(string data)
        {
            string[] msgParts = data.Split('~');
            if (msgParts.Length == 3)
            {
                _type       = msgParts[0];
                _sourceID   = msgParts[1];
                _body       = msgParts[2];
                _valid      = true;
            }
            else
            {
                _valid = false;
            }

        }
    }
}