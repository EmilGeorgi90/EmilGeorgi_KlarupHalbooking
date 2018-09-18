using KlarupHalbooking.Entities;
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
                if (typeof(Entities.IBooking) == typeof(T))
                {
                    repsonse = typeof(KlarupHalbooking.Entities.HallBookingContext).GetProperty(typeof(T).Name) is System.Reflection.PropertyInfo info ? (T)info.GetValue(context) : default(T);
                }
            }
            return repsonse;
        }
        public bool AddData(DateTime bookingDate, string activity, UserData)
        {
            try
            {
                using (Entities.HallBookingContext context = new Entities.HallBookingContext())
                {
                    Entities.Activity activities = new Entities.Activity(context.Activities.FirstOrDefault(a => a.ActivityName == activity).ActivityID, activity, context.Activities.FirstOrDefault(a => a.ActivityName == activity).SpaceNeeded);
                    Entities.Union union = new Entities.Union(userData, context.Unions.FirstOrDefault(u => u.UserData.Username == userData.Username));
                    Entities.HallBooking booking = new Entities.HallBooking(activities);
                    context.HallBookings.Add(booking);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Login(Entities.UserData userdata)
        {
            bool result = false;
            using (Entities.HallBookingContext context = new Entities.HallBookingContext())
            {
                result = context.UserData.Any(u => u.Username == userdata.Username && u.Password == userdata.Password);
            }
            return result;
        }
    }
}
