using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkDiscovery
{
    public class PortScanner
    {
        /// <summary>
        /// Since we allow multiple endpoints per port we can't just look to
        /// see if a port is in use or not with GetActiveUdpListeners().
        /// We have to actually try to open a port and see if it fails.
        /// </summary>
        /// <param name="startPort"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// 
        public static int FindOpenPort(int startPort, int count)
        {
            for (int port = startPort; port < startPort + count; port++)
            {
                //Console.WriteLine("testing: " + port.ToString());
                BroadcastListener testListener = new BroadcastListener(port);
                testListener.Listen(null);
                if (testListener.Active)
                {
                    //Console.WriteLine("found open: " + port.ToString());
                    testListener.StopListening();
                    return port;
                }
                testListener.StopListening();
            }
            return 0;
        }


        //public static int NotUsefulFindOpenPort(int startPort, int count)
        //{
        //    IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        //    IPEndPoint[] endpoints = ipProperties.GetActiveUdpListeners();

        //    List<int> usedPorts = endpoints.Select(endpoint => endpoint.Port).ToList<int>();

        //    foreach (IPEndPoint endpoint in endpoints)
        //    {
        //        Console.WriteLine("endpoint: " + endpoint.Port.ToString());
            
        //    }
        //    for (int port = startPort; port < startPort + count; port++)
        //    {
        //        Console.WriteLine("testing: " + port.ToString());
        //        if (!usedPorts.Contains(port)) return port;
        //    }

        //    return 0;
        //}
    }
}
