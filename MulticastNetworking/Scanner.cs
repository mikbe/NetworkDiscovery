using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;


namespace MulticastNetworking
{
    public class Scanner
    {
        private Scanner()
        { }

        public static string FindOpenAddress()
        {

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endpoints = ipProperties.GetActiveUdpListeners();

            List<string> usedAddresses = endpoints.Select(endpoint => endpoint.Address.ToString()).Distinct<string>().ToList<string>();

            foreach (string address in usedAddresses)
            {
                Console.WriteLine("used address: " + address);
            }

            IPAddress testAddress = IPAddress.Parse(Properties.Settings.Default.MulticastIP);
            string addressString = "";

            for (byte octet = 1; octet < 255; octet++)
            {
                testAddress = buildIPAddress(testAddress, octet);
                addressString = testAddress.ToString();
                if (!usedAddresses.Contains(addressString)) return addressString;
            }

            return addressString;
        }

        private static IPAddress buildIPAddress(IPAddress address, byte octet)
        {
            byte[] bytes = address.GetAddressBytes();
            bytes[3] = octet;

            return new IPAddress(bytes);
        }
    }
}
