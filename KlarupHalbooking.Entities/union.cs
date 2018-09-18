using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class Union : IBooking
    {
        private int unionID;
        private string unionName;
        private UserData userData;
        private UnionLeader unionLeader;

        public Union()
        {
        }

        public Union(UserData userData, string unionName)
        {
            UserData = userData;
            UnionName = unionName;
        }

        public Union(UserData userData, string unionName, int unionID)
        {
            UserData = userData;
            UnionName = unionName;
            UnionID = unionID;
        }

        public Union(UnionLeader unionLeader, UserData userData, string unionName)
        {
            UnionLeader = unionLeader;
            UserData = userData;
            UnionName = unionName;
        }

        public Union(UnionLeader unionLeader, UserData userData, string unionName, int unionID)
        {
            UnionLeader = unionLeader;
            UserData = userData;
            UnionName = unionName;
            UnionID = unionID;
        }

        public UnionLeader UnionLeader
        {
            get { return unionLeader; }
            set { unionLeader = value; }
        }

        public UserData UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        public string UnionName
        {
            get { return unionName; }
            set { unionName = value; }
        }

        public int UnionID
        {
            get { return unionID; }
            set { unionID = value; }
        }
    }
}
