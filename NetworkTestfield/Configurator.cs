using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NetworkTestfield
{
    public class Configurator
    {
        public static readonly string IPPlain = "192.168.56.1";
        public static readonly IPAddress IP = IPAddress.Parse(IPPlain);
        public static readonly int Port = 13000;
        public static readonly IPEndPoint EndPoint = new(IP, Port);
    }
}
