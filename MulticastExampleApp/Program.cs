using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MulticastNetworking;

namespace MulticastExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //MulticastDemo();
            //ScanAddresses();

            TestForListeners();

            Thread.Sleep(100);
            Console.WriteLine("Press [Enter] to end.");
            Console.ReadLine();
        }

        static void TestForListeners()
        {
            Listener listener1 = new Listener();
            listener1.Listen(listener_ReceivedData);
        }

        static void ScanAddresses()
        {
            string address = Scanner.FindOpenAddress();
            Console.WriteLine("free: " + address);
        }

        static void MulticastDiscoveryDemo()
        { 
        
            // Normal execution

            // Register a server

            // Open a client


            // Client first, then server
        

            // Server crashes, client reconnects


        }

        static void MulticastDemo()
        {
            Listener listener1 = new Listener();
            listener1.Listen(listener_ReceivedData);

            Listener listener2 = new Listener();
            listener2.Listen(listener_ReceivedData);

            Talker talker = new Talker();
            talker.Say("Multicast send");
            talker.Say("Say again.");

            Thread.Sleep(100);
        }

        static void listener_ReceivedData(string Data)
        {
            Console.WriteLine("Rx: " + Data);
        }
    }
}
