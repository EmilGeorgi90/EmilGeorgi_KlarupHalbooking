using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPServer tCPServer = new TCPServer(65432);
            tCPServer.Run();
            Console.ReadLine();
        }
    }
}
