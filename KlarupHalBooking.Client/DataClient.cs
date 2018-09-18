using KlarupHalbooking.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KlarupHalbooking.Client
{
    public class DataClient
    {
        Entities.HallBookingContext context = new Entities.HallBookingContext();
        public List<IBooking> GetData(params IBooking[] bookings)
        {
            IQueryable<IBooking> repsonse;
            repsonse = from book in context.Activities select book;

            return repsonse.ToList();
        }
        public bool AddData(DateTime bookingDate, DateTime bookingEndDate,string activity, UserData userData)
        {
            try
            {
                Entities.Activity activities = new Entities.Activity(context.Activities.FirstOrDefault(a => a.ActivityName == activity).ActivityID, activity, context.Activities.FirstOrDefault(a => a.ActivityName == activity).SpaceNeeded);
                Entities.Union union = new Entities.Union(userData, context.Unions.FirstOrDefault(u => u.UserData.Username == userData.Username).UnionName);
                Entities.HallBooking booking = new Entities.HallBooking(activities, union, bookingDate, bookingEndDate, null, false);
                context.HallBookings.Add(booking);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public (bool, UserData) Login(Entities.UserData userdata)
        {
            bool result = false;
            UserData user = null;
            result = context.UserData.Any(u => u.Username == userdata.Username && u.Password == userdata.Password);
            user = context.UserData.FirstOrDefault(u => u.Username == userdata.Username && u.Password == userdata.Password);
            return (result, user);
        }
    }
}
