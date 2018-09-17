using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class Activity
    {
        private int activityID;
        private string activityName;

        public Activity(string activityName)
        {
            ActivityName = activityName;
        }

        public Activity(int activityID, string activityName)
        {
            ActivityID = activityID;
            ActivityName = activityName;
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

    }
}
