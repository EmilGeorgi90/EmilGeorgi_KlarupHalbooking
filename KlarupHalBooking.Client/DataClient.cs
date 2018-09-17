using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Client
{
    public class DataClient<T>
    {
        public T GetData(params string[] parameters)
        {
            T repsonse = default(T);
            using (Entities.HallBookingContext context = new Entities.HallBookingContext())
            {
                switch (typeof(T))
                {
                    default:
                        throw new NotSupportedException("type not supported");
                }
            }
        }
    }
}
