using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkDiscovery;
using System.Threading;

namespace ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ListenerTalkerDemo();
            DiscovererDemo();

            Thread.Sleep(100);
            Console.WriteLine("Press [Enter] to end.");
            Console.ReadLine();
        }

        static void DiscovererDemo()
        {
            Discoverer discoverer1 = new Discoverer();
            Discoverer discoverer2 = new Discoverer();

            Console.WriteLine("1.id: " + discoverer1.ID);
            Console.WriteLine("2.id: " + discoverer2.ID);

            discoverer1.PublishSelf("Found:" + discoverer1.ID);
            discoverer2.PublishSelf("Found:" + discoverer2.ID);

            // This should only respond once.
            discoverer2.Discover(discovered_Callback);

            // This should not respond.
            discoverer1.Discover(discovered_Callback);

            // Now it should respond

            //discoverer1.Discover(discovered_Callback);

            Thread.Sleep(100);
        }

        static void discovered_Callback(string message)
        {
            Console.WriteLine("Discovered: " + message);
        }

        static void ListenerTalkerDemo()
        {
            int port = 12399;

            BroadcastListener listener = new BroadcastListener(port);
            listener.Listen(listener_ReceivedData);

            BroadcastTalker talker = new BroadcastTalker(port);

            for (int i = 0; i < 10; i++)
            {
                talker.say(i.ToString());
            }
            Thread.Sleep(100);

            listener.StopListening();
            listener = null;
        }

        static void listener_ReceivedData(string Data)
        {
            Console.WriteLine("Rx: " + Data);
        }
    }
}
