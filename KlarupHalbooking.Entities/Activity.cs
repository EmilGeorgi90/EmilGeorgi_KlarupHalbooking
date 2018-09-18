using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class Activity : IBooking
    {
        private int activityID;
        private string activityName;
        private double spaceNeeded;

        public Activity()
        {
        }

        public Activity(string activityName, double spaceNeeded)
        {
            ActivityName = activityName;
            SpaceNeeded = spaceNeeded;
        }

        public Activity(int activityID, string activityName, double spaceNeeded)
        {
            ActivityID = activityID;
            ActivityName = activityName;
            SpaceNeeded = spaceNeeded;
        }

        public int ActivityID
        {
            get { return activityID; }
            set { activityID = value; }
        }
        public string ActivityName
        {
            get { return activityName; }
            set { activityName = value; }
        }
        public double SpaceNeeded
        {
            get { return spaceNeeded; }
            set
            {
                if(value / 100 > 1)
                {
                    throw new ArgumentException("kan ikke være større end 1");
                }
                spaceNeeded = value / 100;
            }
        }

        public override string ToString()
        {
            return ActivityName;
        }
    }
}
