using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class HallBooking : IBooking
    {
        private int hallBookingID;
        private DateTime hallBookingTime;
        private DateTime hallBookingEndTime;
        private Admin admin;

        public Admin Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        private bool confirmed;
        private Union union;
        private Activity activity;

        public HallBooking()
        {
        }

        public HallBooking(Activity activity, Union union, DateTime hallBookingTime, DateTime hallBookingEndTime, Admin admin, bool confirmed)
        {
            Activity = activity;
            Union = union;
            HallBookingTime = hallBookingTime;
            HallBookingEndTime = hallBookingEndTime;
            Confirmed = confirmed;
            Admin = admin;
        }

        public HallBooking(Activity activity, Union union, DateTime hallBookingTime, DateTime hallBookingEndTime, Admin admin, bool confirmed, int hallBookingID)
        {
            Activity = activity;
            Union = union;
            HallBookingTime = hallBookingTime;
            HallBookingEndTime = hallBookingEndTime;
            HallBookingID = hallBookingID;
            Confirmed = confirmed;
            Admin = admin;
        }

        public Activity Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        public Union Union
        {
            get { return union; }
            set { union = value; }
        }

        public DateTime HallBookingTime
        {
            get { return hallBookingTime; }
            set { hallBookingTime = value; }
        }
        public DateTime HallBookingEndTime
        {
            get { return hallBookingEndTime; }
            set
            {
                if (value < HallBookingTime)
                    throw new ArgumentException("slut tid kan ikke være mindre end start tid");
                else
                    hallBookingEndTime = value;
            }
        }
        public bool Confirmed
        {
            get { return confirmed; }
            set { confirmed = value; }
        }
        public int HallBookingID
        {
            get { return hallBookingID; }
            set { hallBookingID = value; }
        }

    }
}
