using KlarupHalbooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Client
{
    public class ServerApiHandler
    {
        TcpClient client;
        const int bufferSize = 1024 * 1024;
        protected byte[] recieveBuffer = new byte[bufferSize];
        public ServerApiHandler()
        {
            this.client = new TcpClient(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 65432));
        }

        public IBooking SentDataRequest(ServerCommands command, params IBooking[] parameters)
        {
            if (client.Connected)
            {
                byte[] buffer = Serializer<ServerCommands>.Serialize(command);
                byte[] bufferReceiver = Transmit(buffer);
                return Serializer<IBooking>.Deserialize(bufferReceiver);
            }
            else
            {
                throw new System.Net.WebException("something went wrong");
            }
        }
        /// <summary>Transmits the provided byte array to the remote endpoint. Overridable.</summary>
        /// <param name="transmitBuffer">The byte array to transmit to the remote endpoint.</param>
        /// <returns>A byte array with the response data from the server.</returns>
        protected byte[] Transmit(byte[] transmitBuffer)
        {
            try
            {

                Array.Clear(recieveBuffer, index: 0, length: recieveBuffer.Length);
                client.GetStream().Write(transmitBuffer, offset: 0, size: transmitBuffer.Length);
                client.GetStream().Read(recieveBuffer, offset: 0, size: bufferSize);
                return recieveBuffer;
            }
            catch (Exception ex)
            {
                throw new System.Net.WebException(ex.Message, ex);
            }
        }
    }
}
