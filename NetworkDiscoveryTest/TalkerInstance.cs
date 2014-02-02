using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetworkDiscoveryTest
{
    [TestClass]
    public class TalkerInstance
    {
        [TestMethod]
        public void BroadcastsTheMessage()
        {
            NetworkDiscovery.Talker talker = new NetworkDiscovery.Talker();
            string message = "Some message";

            bool spoke = talker.say(message);
            Assert.IsTrue(spoke);
        }

        [TestMethod]
        public void BroadcastsTheMessageOnAllNetworkInterfaces()
        {

        }

    }
}
