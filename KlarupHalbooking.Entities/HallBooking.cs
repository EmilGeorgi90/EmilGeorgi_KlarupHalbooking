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
        private Union union;
        private Activity activity;

        public HallBooking(Activity activity, Union union, DateTime hallBookingTime)
        {
            Activity = activity;
            Union = union;
            HallBookingTime = hallBookingTime;
        }

        public HallBooking(Activity activity, Union union, DateTime hallBookingTime, int hallBookingID)
        {
            Activity = activity;
            Union = union;
            HallBookingTime = hallBookingTime;
            HallBookingID = hallBookingID;
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

        public int HallBookingID
        {
            get { return hallBookingID; }
            set { hallBookingID = value; }
        }

    }
}
