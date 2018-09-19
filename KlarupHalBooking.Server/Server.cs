using KlarupHalBooking.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KlarupHalBooking.Server
{
    public class TCPServer
    {
        #region Constants
        /// <summary>The upper limit to both <see cref="recieveBuffer"/> and <see cref="sendBuffer"/>.</summary>
        const int bufferLimit = 1024 * 1024;
        #endregion
        #region Fields
        /// <summary>The listener.</summary>
        protected TcpListener listener;

        /// <summary>The currently connected client.</summary>
        protected TcpClient connectedClient;

        // TODO: Add nescessary new repositories here:

        /// <summary>The bytes loaded from a client stream.</summary>
        protected byte[] recieveBuffer = new byte[bufferLimit];

        /// <summary>The bytes to transmit through the stream of the <see cref="connectedClient"/>.</summary>
        protected byte[] sendBuffer = new byte[bufferLimit];
        private readonly int port;
        private readonly HallBookingContext bookingContext;
        #endregion
        /// <summary>
        /// create the async service object
        /// </summary>
        /// <param name="port"></param>
        public TCPServer(int port)
        {
            this.port = port;
            bookingContext = new HallBookingContext();
        }
        /// <summary>
        /// begin running the server
        /// </summary>
        public async void Run()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
            //create the listener object with given ipAdress and port
            TcpListener listener = new TcpListener(ep);
            Console.WriteLine(listener.LocalEndpoint);
            //start the object
            listener.Start();
            Console.WriteLine(" on port " + this.port);
            Console.WriteLine("Hit <enter> to stop service\n");
            //make the server take more then one request before having to restart
            while (true)
            {
                try
                {
                    //waits for the client to connect
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    //starting the task
                    Thread t = new Thread(new ParameterizedThreadStart(Process));
                    //wait for the task to complete
                    t.Start(tcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// <summary>
        /// making the process and take the rquest from the client and the data send with it
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        public async void Process(object tcpClient)
        {
            TcpClient client = (TcpClient)tcpClient;
            //gets the clients ipAdress
            try
            {
                while (client.Connected)
                {
                    await connectedClient.Client.ReceiveAsync(new ArraySegment<byte>(recieveBuffer), SocketFlags.None);
                    ServerCommands clientRequest = Serializer<ServerCommands>.Deserialize(recieveBuffer);
                    Array.Clear(recieveBuffer, index: 0, length: recieveBuffer.Length);
                    switch (clientRequest)
                    {
                        case ServerCommands.GetNextBooking:
                            RespondToClient(bookingContext.HallBookings.OrderByDescending(c => c.HallBookingEndTime).FirstOrDefault());
                            break;
                        case ServerCommands.GetBookingFromUnionName:
                            RespondToClient(bookingContext.HallBookings.OrderBy(c => c.HallBookingTime > DateTime.Now && c.Union.UnionName == "de bedre autister").FirstOrDefault());
                            break;
                        case ServerCommands.HallTimeTaken:
                            RespondToClient(bookingContext.HallBookings.OrderBy(c => c.HallBookingTime > DateTime.Now).Last(a => a.Confirmed == true));
                            break;
                        case ServerCommands.UnionUsedHallMost:
                            RespondToClient(bookingContext.HallBookings.OrderBy(c => c.HallBookingTime > DateTime.Now && c.Union.UnionName == "de bedre autister").FirstOrDefault());
                            break;
                        default:
                            RespondToClient(null);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
        protected virtual void RespondToClient(IBooking booking)
        {
            // Do not modify this method.
            try
            {
                Array.Clear(sendBuffer, index: 0, length: sendBuffer.Length);
                sendBuffer = Serializer<IBooking>.Serialize(booking);
                connectedClient.GetStream().Write(sendBuffer, offset: 0, size: sendBuffer.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
