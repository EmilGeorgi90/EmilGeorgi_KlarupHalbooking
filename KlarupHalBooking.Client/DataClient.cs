using KlarupHalbooking.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            List<HallBooking> repsonse = new List<HallBooking>();
            var dataHolder = from book in context.HallBookings join act in context.Activities on book.Activity.ActivityID equals act.ActivityID join uni in context.Unions on book.Union.UnionID equals uni.UnionID join user in context.UserData on uni.UserData.UserDataID equals user.UserDataID select new { hallboking = book, acti = act, unio = uni, users = user };
            foreach (var data in dataHolder)
            {
                data.hallboking.Activity = data.acti;
                data.hallboking.Union = data.unio;
                data.hallboking.Union.UserData = data.users;
                repsonse.Add(data.hallboking);
            }
            return repsonse.ToList();
        }
        public List<Activity> GetActivities(params IBooking[] bookings)
        {
            IQueryable<Activity> repsonse;
            repsonse = from act in context.Activities select act;

            return repsonse.ToList();
        }
        /// <summary>
        /// ment to add booking in database thru EF
        /// </summary>
        /// <param name="bookingDate"></param>
        /// <param name="bookingEndDate"></param>
        /// <param name="activity"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
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
        public int Remove(HallBooking booking)
        {
            context.HallBookings.Remove(booking);
            return context.SaveChanges();
        }
        public double CalculateCoveragePercentageByDay(DateTime date)
        {
            double nonBookedMinutes = CalculateNonBookedMinutesByDay(date);
            double openMinutes = CalculateTotalMinutesOpenByDay(date);
            double reservedMinutes = openMinutes - nonBookedMinutes;
            double reservedPercentage = reservedMinutes / openMinutes * 100;

            return reservedPercentage;
        }

        /// <summary>
        /// Method to calculate the amount of minutes within operating hours on any given day, that are not booked.
        /// </summary>
        /// <param name="date">The day to calculate nonbooked minutes on</param>
        /// <returns>The amount of minutes within operating hours on the given day, that are not booked</returns>
        public double CalculateNonBookedMinutesByDay(DateTime date)
        {
            List<HallBooking> bookings = context.HallBookings.Where(b => DbFunctions.TruncateTime(b.HallBookingTime) == DbFunctions.TruncateTime(date)).OrderBy(b => b.HallBookingTime).ToList();
            double unreservedMinutes = 0;
            TimeSpan openingTime;
            TimeSpan closingTime;
            if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                openingTime = new TimeSpan(09, 00, 00);
                closingTime = new TimeSpan(21, 00, 00);
            }
            else
            {
                openingTime = new TimeSpan(08, 00, 00);
                closingTime = new TimeSpan(22, 00, 00);
            }
            if (bookings.Count() == 0)
            {
                unreservedMinutes += (closingTime - openingTime).TotalMinutes;
            }
            else if (bookings.Count() == 1)
            {
                unreservedMinutes += (bookings[0].HallBookingTime.TimeOfDay - openingTime).TotalMinutes;
                unreservedMinutes += (closingTime - bookings[0].HallBookingEndTime.TimeOfDay).TotalMinutes;
            }
            else if (bookings.Count() > 1)
            {
                unreservedMinutes += (bookings[0].HallBookingTime.TimeOfDay - openingTime).TotalMinutes;
                for (int i = 1; i < bookings.Count(); i++)
                {
                    if (bookings[i - 1].HallBookingEndTime.TimeOfDay < bookings[i].HallBookingTime.TimeOfDay)
                    {
                        unreservedMinutes += (bookings[i].HallBookingTime.TimeOfDay - bookings[i - 1].HallBookingEndTime.TimeOfDay).TotalMinutes;
                    }
                }
                unreservedMinutes += (closingTime - bookings[bookings.Count() - 1].HallBookingEndTime.TimeOfDay).TotalMinutes;
            }
            return unreservedMinutes;
        }

        /// <summary>
        /// Method to calculate how many minutes the hall is open for on any given day
        /// </summary>
        /// <param name="date">The date to find opening hours of</param>
        /// <returns>The amount of minutes the hall is open for on the given day</returns>
        public double CalculateTotalMinutesOpenByDay(DateTime date)
        {
            TimeSpan openingTime;
            TimeSpan closingTime;
            if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                openingTime = new TimeSpan(09, 00, 00);
                closingTime = new TimeSpan(21, 00, 00);
            }
            else
            {
                openingTime = new TimeSpan(08, 00, 00);
                closingTime = new TimeSpan(22, 00, 00);
            }
            return (closingTime - openingTime).TotalMinutes;
        }

        /// <summary>
        /// Method to calculate the percentage of time between two dates that are booked
        /// </summary>
        /// <param name="startDate">The inclusive start date to calculate on</param>
        /// <param name="endDate">The inclusive end date to calculate on</param>
        /// <returns>The percentage of time between the two given dates that is booked</returns>
        public double CalculateCoveragePercentageByDateRange(DateTime startDate, DateTime endDate)
        {
            double unreservedMinutes = 0;
            double openMinutes = 0;
            List<DateTime> dates = FindDatesInDateRange(startDate, endDate);
            foreach (DateTime day in dates)
            {
                openMinutes += CalculateTotalMinutesOpenByDay(day);
                unreservedMinutes += CalculateNonBookedMinutesByDay(day);
            }
            double reservedMinutes = openMinutes - unreservedMinutes;
            double reservedPercentage = reservedMinutes / openMinutes * 100;


            return reservedPercentage;
        }

        /// <summary>
        /// Method to return a list of all days between two dates
        /// Throws an argument exception if endDate is earlier than startDate
        /// </summary>
        /// <param name="startDate">The inclusive start date to return in a list</param>
        /// <param name="endDate">The inclusive end date to return in a list</param>
        /// <returns>A list of dates including both startDate and endDate</returns>
        public List<DateTime> FindDatesInDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("Slutdato må ikke være før startdato");
            }
            List<DateTime> dateRange = new List<DateTime>();
            for (DateTime day = startDate; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                dateRange.Add(day);
            }
            return dateRange;
        }

        /// <summary>
        /// Method to find the union that has made the most reservation in any given date range
        /// </summary>
        /// <param name="startDate">The inclusive start date to count reservations from</param>
        /// <param name="endDate">The inclusive end date to count reservations from</param>
        /// <returns>The union that has made the most reservations between the two dates</returns>
        public Union CalculateMostActiveUnionByDateRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = FindDatesInDateRange(startDate, endDate);
            List<HallBooking> bookings = context.HallBookings.ToList();
            IQueryable<HallBooking> bookin = null;
            foreach (HallBooking booking in bookings)
            {
                bookin = context.HallBookings.Where(r => r.Union.UnionID == booking.Union.UnionID && dates.Contains(r.HallBookingTime) && r.HallBookingTime.TimeOfDay >= context.HallBookings.OrderByDescending(c => c.HallBookingTime).FirstOrDefault().HallBookingTime.TimeOfDay);
            }
            return bookin.FirstOrDefault().Union;
        }
    }
}

