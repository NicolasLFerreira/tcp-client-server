using System;
using System.Net;

namespace NetworkTestField
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the Name of HOST   
            string hostName = Dns.GetHostName();
            Console.WriteLine(hostName);

            // Get the IP from GetHostByName method of dns class. 
            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            Console.WriteLine("IP Address is : " + IP);
        }
    }
}