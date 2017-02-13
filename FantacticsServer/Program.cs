using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FantacticsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.StartListening(Dns.GetHostName(), 11000);
        }
    }
}
