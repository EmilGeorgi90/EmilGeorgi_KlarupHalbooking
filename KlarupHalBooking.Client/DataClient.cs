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
        public List<HallBooking> GetData(params IBooking[] bookings)
        {
            IQueryable<HallBooking> repsonse;
            repsonse = from book in context.HallBookings join act in context.Activities on book.Activity.ActivityID equals act.ActivityID join uni in context.Unions on book.Union.UnionID equals uni.UnionID join user in context.UserData on uni.UserData.UserDataID equals user.UserDataID select book;

            return repsonse.ToList();
        }
        public List<Activity> GetActivities(params IBooking[] bookings)
        {
            IQueryable<Activity> repsonse;
            repsonse = from act in context.Activities select act;

            return repsonse.ToList();
        }
        public bool AddData(DateTime bookingDate, DateTime bookingEndDate, string activity, UserData userData)
        {
            try
            {
                Entities.Activity activities = context.Activities.FirstOrDefault(c => c.ActivityName == activity);
                Entities.Union union = context.Unions.FirstOrDefault(c => c.UserData.UserDataID == userData.UserDataID);
                Entities.HallBooking booking = new Entities.HallBooking { Activity = activities, Union = union, HallBookingTime = bookingDate, HallBookingEndTime = bookingEndDate, Admin = null, Confirmed = false };
                context.HallBookings.Add(booking);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int Confirm(HallBooking booking, UserData admin)
        {
            try
            {
                HallBooking book = context.HallBookings.FirstOrDefault(c => c.HallBookingID == booking.HallBookingID);
                book.Confirmed = true;
                book.Admin = context.Admins.FirstOrDefault(a => a.UserData.Username == admin.Username);
                return context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public (bool, bool, UserData) Login(Entities.UserData userdata)
        {
            bool result = false;
            UserData user = null;
            result = context.UserData.Any(u => u.Username == userdata.Username && u.Password == userdata.Password);
            userdata = user = context.UserData.FirstOrDefault(u => u.Username == userdata.Username && u.Password == userdata.Password);
            bool isAdmin = context.Admins.Any(a => a.UserData.UserDataID == userdata.UserDataID);
            return (result, isAdmin, user);
        }
        public int remove(HallBooking booking)
        {
            context.HallBookings.Remove(booking);
            return context.SaveChanges();
        }
    }
}
