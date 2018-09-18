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
        private bool confirmed;
        private Union union;
        private Activity activity;


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
        public Admin Admin
        {
            get { return admin; }
            set { admin = value; }
        }
        public DateTime HallBookingTime
        {
            get { return hallBookingTime; }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("hall booking tid kan ikke være mindre end dags dato");
                }
                else
                {
                    hallBookingTime = value;
                }
            }
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
        public override string ToString()
        {
            return $"{Activity.ToString()} {Admin.ToString()} {HallBookingTime} {HallBookingEndTime} {Union.ToString()}";
        }
    }
}
