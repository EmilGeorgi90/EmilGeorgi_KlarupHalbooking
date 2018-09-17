using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Client
{
    public class Client
    {
        public async Task<TcpClient> Connect()
        {
            try
            {
                System.Net.Sockets.TcpClient client = new TcpClient();
                int port = 65432;
                //gets the possible Host's
                string server = Dns.GetHostName();
                //gets IP Host info from variable server
                IPHostEntry ipHostInfo = Dns.GetHostEntry(server);
                IPAddress ipAddress = null;
                //loops into ipHostinfos IPAddress's
                for (int i = 0; i < ipHostInfo.AddressList.Length; ++i)
                {
                    //checks if the current ip addrees Family type is equel InterNetwork
                    if (ipHostInfo.AddressList[i].AddressFamily ==
                      AddressFamily.InterNetwork)
                    {
                        ipAddress = ipHostInfo.AddressList[i];
                        break;
                    }

                }
                //throw exception if none were to find
                if (ipAddress == null)
                {
                    throw new Exception("No IPv4 address for server");
                }
                await client.ConnectAsync(ipAddress, port); // Connect
                return client;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<string> SendRequest(TcpClient client, string method, string data)
        {
            try
            {
                string response = null;
                
                //create's the object that streams the data from client to server
                NetworkStream networkStream = client.GetStream();
                StreamWriter writer = new StreamWriter(networkStream);
                StreamReader reader = new StreamReader(networkStream);
                //sets autoflush to true so i will see on its own if we are trying to send something to the sever
                writer.AutoFlush = true;
                await writer.WriteLineAsync();
                //wait for the response
                response = await reader.ReadLineAsync();
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
